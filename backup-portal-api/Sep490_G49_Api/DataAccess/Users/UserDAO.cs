using AutoMapper;
using AutoMapper.Execution;
using BusinessObjects.DTO.BusinessObjects.DTO;
using BusinessObjects.DTO.User;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.Emails;
using DataAccess.UserHistories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Users
{
    public class UserDAO
    {
        private readonly SEP490_G49Context _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EmailDAO _emailDAO;
        private readonly IHubContext<NotificationHub> _notificationHub;

        public UserDAO(
            IHttpContextAccessor httpContextAccessor,
            SEP490_G49Context context,
            IMapper mapper,
            IConfiguration configuration,
            EmailDAO emailDAO,
            IHubContext<NotificationHub> notificationHub)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _emailDAO = emailDAO;
            _notificationHub = notificationHub;
        }

        public async Task<Response> CreateUser(NewUserDTO newUser)
        {
            try
            {
                if (await UserExists(newUser.UserName))
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "Username is already taken" };
                }
                if (await _context.Users.Include(x => x.UserInformation).AnyAsync(u => u.UserInformation.Email == newUser.Email))
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "Email is already in use" };
                }
                var department = await _context.Departments.FindAsync(newUser.DepartmentId);
                var role = await _context.Roles.FindAsync(newUser.RoleId);
                if (role == null)
                {
                    return new Response { Code = ResponseCode.NotFound, Message = "Not found role." };
                }

                string temporaryPassword = GenerateTemporaryPassword();
                CreatePasswordHash(temporaryPassword, out byte[] passwordHash, out byte[] passwordSalt);

                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return new Response { Code = ResponseCode.NotFound, Message = "User ID claim is missing." };
                }

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Username = newUser.UserName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    CreatedAt = DateTime.UtcNow.AddHours(7),
                    IsDeleted = false,
                    UpdatedAt = DateTime.UtcNow.AddHours(7),
                    DepartmentId = newUser.DepartmentId,
                    Department = department,
                    Status = true,
                    RoleId = role.Id,
                    CreatedBy = Guid.Parse(userId),
                    UpdatedBy = Guid.Parse(userId),
                };

                string codeNewUserInformation = GenerateCodeForUserInformation(newUser.FullName);
                var userInformation = new UserInformation
                {
                    Id = Guid.NewGuid(),
                    FullName = newUser.FullName,
                    TypeOfWork = newUser.TypeOfWork,
                    Email = newUser.Email,
                    Status = "Active",
                    Code = codeNewUserInformation,
                    UserId = user.Id
                };
                var userHistory = new UserHistory
                {
                    RoleName = role.Name,
                    DepartmentName = department.Name,
                    UserId = user.Id,
                    User = user,
                    Id = Guid.NewGuid(),
                    StartTime = DateTime.UtcNow.AddHours(7),
                    EndTime = null
                };

                user.TemporaryPasswordHash = passwordHash;
                user.TemporaryPasswordSalt = passwordSalt;
                user.TemporaryPasswordExpires = DateTime.UtcNow.AddHours(9);

                var subject = "Welcome to the Company - Your Account";
                var body = $@"
<div style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
    <h2 style='color: #007BFF;'>Hello {newUser.FullName},</h2>

    <p>Welcome to the company!</p>

    <p>
        <strong>Your username:</strong> {newUser.UserName}<br>
        <strong>Your temporary password:</strong> {temporaryPassword}
    </p>

    <p style='background-color: #f8d7da; color: #721c24; padding: 10px; border-radius: 5px;'>
        Please note that this temporary password is invalid after 2 hours!
    </p>

    <p>Best regards,</p>

</div>";

                var sendEmail = _emailDAO.SendEmail(newUser.Email, subject, body);

                if (sendEmail == null)
                {
                    return new Response { Message = "There are unexpected when send email, please try again later", Code=ResponseCode.BadRequest};
                }
                _context.Users.Add(user);
                _context.UserInformations.Add(userInformation);
                _context.UserHistories.Add(userHistory);
                await _context.SaveChangesAsync();
                var message = $"Welcome '{user.UserInformation.FullName}' to Company";
                var redirectUrl = "";
                var notification = new Notification
                {
                    Id = Guid.NewGuid(),
                    Message = message,
                    CreatedAt = DateTime.UtcNow.AddHours(7),
                    IsRead = false,
                    UserId = user.Id,
                    RedirectUrl = redirectUrl,
                    User = user
                };

                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                await _notificationHub.Clients.All.SendAsync("ReceiveNotification", user.Id, new Notification
                {
                    Message = message,
                    RedirectUrl = redirectUrl
                });

                var message1 = $"Welcome '{user.Username}' to '{user.Department.Name}' Department";
                var redirectUrl1 = "";
                var notificationForDepartment = new Notification
                {
                    Id = Guid.NewGuid(),
                    Message = message1,
                    CreatedAt = DateTime.UtcNow.AddHours(7),
                    IsRead = false,
                    UserId = user.Id,
                    RedirectUrl = redirectUrl1,
                    User = user
                };
                _context.Notifications.Add(notificationForDepartment);
                await _context.SaveChangesAsync();
                await _notificationHub.Clients.All.SendAsync("ReceiveNotification", user.Id, new Notification
                {
                    CreatedAt = DateTime.UtcNow.AddHours(7),
                    Message = message1,
                    RedirectUrl = redirectUrl
                });

                return new Response { Code = ResponseCode.Success, Message = "Create user successfully!" };
            }
            catch (Exception)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Create failure!" };
            }
        }
        private async Task NotifyDepartmentMembers(Guid departmentId, string message, string redirectUrl = null)
        {
            try
            {
                var department = await _context.Departments
                    .Include(d => d.Users)
                    .FirstOrDefaultAsync(d => d.Id == departmentId);

                if (department == null)
                {
                    throw new Exception("Department not found.");
                }

                foreach (var user in department.Users)
                {
                    await _notificationHub.Clients.User(user.Id.ToString()).SendAsync("ReceiveNotification", new Notification
                    {
                        Message = message,
                        RedirectUrl = redirectUrl
                    });

                    // Add notification to database
                    var notification = new Notification
                    {
                        Id = Guid.NewGuid(),
                        Message = message,
                        CreatedAt = DateTime.UtcNow.AddHours(7),
                        IsRead = false,
                        UserId = user.Id,
                        RedirectUrl = redirectUrl,
                        User = user
                    };

                    _context.Notifications.Add(notification);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Failed to notify department members.", ex);
            }
        }
        private bool IsValidPassword(string password)
        {
            if (password.Length < 8) return false;
            if (!password.Any(char.IsUpper)) return false;
            if (!password.Any(char.IsLower)) return false;
            if (!password.Any(char.IsDigit)) return false;
            if (!password.Any(ch => !char.IsLetterOrDigit(ch))) return false;
            return true;
        }


        public async Task<Response> Login(LoginDTO login)
        {
            var users = await _context.Users
                .Include(u => u.UserInformation)
                .Include(u => u.Role)
                .ToListAsync(); // Lấy tất cả dữ liệu từ bảng Users vào bộ nhớ

            // Tìm người dùng có tên người dùng khớp phân biệt chữ hoa và chữ thường
            var user = users
                .FirstOrDefault(x => x.Username.Equals(login.Username, StringComparison.Ordinal));

            if (user == null)
            {
                return new Response
                {
                    Message = "Username is not exist",
                    Code = ResponseCode.BadRequest,
                };
            }

            if (user.UserInformation.Status.Equals("Deactive", StringComparison.OrdinalIgnoreCase))
            {
                return new Response
                {
                    Message = "Account is deactivated. Please contact support.",
                    Code = ResponseCode.BadRequest,
                };
            }

            // Kiểm tra mật khẩu tạm thời trước
            if (user.TemporaryPasswordHash != null && VerifyPasswordHash(login.Password, user.TemporaryPasswordHash, user.TemporaryPasswordSalt))
            {
                // Kiểm tra thời gian hết hạn của mật khẩu tạm thời
                if (DateTime.UtcNow.AddHours(7) > user.TemporaryPasswordExpires)
                {
                    return new Response
                    {
                        Message = "Temporary password has expired. Please request a new one.",
                        Code = ResponseCode.BadRequest,
                    };
                }

                // Đăng nhập thành công với mật khẩu tạm thời
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.Name),
            new Claim(ClaimTypes.GroupSid, user.DepartmentId.ToString())
        };

                var accessToken = GenerateAccessToken(claims);
                var refreshToken = GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpires = DateTime.Now.AddDays(7);
                user.RefreshTokenCreated = DateTime.UtcNow.AddHours(7);
                user.AccessToken = accessToken;
                user.AccessTokenCreated = DateTime.UtcNow.AddHours(7);

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                var token = new Token
                {
                    AccessToken = user.AccessToken,
                    RefreshToken = user.RefreshToken,
                    RefreshTokenExpires = user.RefreshTokenExpires
                };

                return new Response
                {
                    Data = token,
                    Code = ResponseCode.Success,
                    Message = "Login successful with temporary password. Please change your password immediately."
                };
            }

            // Kiểm tra mật khẩu thông thường nếu mật khẩu tạm thời không khớp
            if (!VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new Response
                {
                    Message = "Password is incorrect",
                    Code = ResponseCode.BadRequest,
                };
            }

            // Tạo token và cập nhật thông tin
            var claimsForRegularLogin = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role.Name),
        new Claim(ClaimTypes.GroupSid, user.DepartmentId.ToString())
    };

            var accessTokenForRegularLogin = GenerateAccessToken(claimsForRegularLogin);
            var refreshTokenForRegularLogin = GenerateRefreshToken();

            user.RefreshToken = refreshTokenForRegularLogin;
            user.RefreshTokenExpires = DateTime.Now.AddDays(7);
            user.RefreshTokenCreated = DateTime.UtcNow.AddHours(7);
            user.AccessToken = accessTokenForRegularLogin;
            user.AccessTokenCreated = DateTime.UtcNow.AddHours(7);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            var regularToken = new Token
            {
                AccessToken = user.AccessToken,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpires = user.RefreshTokenExpires
            };

            return new Response
            {
                Data = regularToken,
                Code = ResponseCode.Success,
                Message = "Login successfully."
            };
        }






        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            byte[] tokenBytes = RandomNumberGenerator.GetBytes(64);
            return BitConverter.ToString(tokenBytes).Replace("-", "").ToLower();
        }


        public async Task<Response> ChangePassword(Guid userId, ChangePasswordDTO changePasswordDto)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    return new Response { Code = ResponseCode.NotFound, Message = "User not found" };
                }

                // Kiểm tra mật khẩu cũ
                if (!VerifyPasswordHash(changePasswordDto.OldPassword, user.PasswordHash, user.PasswordSalt))
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "Old password is incorrect" };
                }

                // Kiểm tra mật khẩu mới và mật khẩu xác nhận có khớp không
                if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "New password and confirm password do not match" };
                }

                // Tạo hash cho mật khẩu mới
                CreatePasswordHash(changePasswordDto.NewPassword, out byte[] newPasswordHash, out byte[] newPasswordSalt);

                // Cập nhật hash mật khẩu và salt của mật khẩu mới
                user.PasswordHash = newPasswordHash;
                user.PasswordSalt = newPasswordSalt;

                // Sau khi thay đổi mật khẩu, hủy mật khẩu tạm thời
                user.TemporaryPasswordHash = null;
                user.TemporaryPasswordSalt = null;
                user.TemporaryPasswordExpires = null; // Hoặc bạn có thể để null nếu field này nullable

                user.UpdatedAt = DateTime.UtcNow.AddHours(7);

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return new Response { Code = ResponseCode.Success, Message = "Password changed successfully!" };
            }
            catch (Exception)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Password change failed!" };
            }
        }


        public async Task<Token> RefreshToken(string token)
        {
            var user = await _context.Users
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.RefreshToken == token);

            if (user == null || user.RefreshTokenExpires < DateTime.Now)
            {
                throw new SecurityTokenException("Invalid token");
            }

            var newAccessToken = GenerateAccessToken(GetUserClaims(user));
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenCreated = DateTime.UtcNow.AddHours(7);
            user.RefreshTokenExpires = DateTime.UtcNow.AddDays(7);
            user.AccessToken = newAccessToken;
            user.AccessTokenCreated = DateTime.UtcNow.AddHours(7);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new Token
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                RefreshTokenExpires = user.RefreshTokenExpires
            };
        }

        private IEnumerable<Claim> GetUserClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim(ClaimTypes.GroupSid, user.DepartmentId.ToString())
            };

            return claims;
        }
        public async Task<Response> UpdateRoleDepartment(UpdateRoleDTO updateRoleDTO)
        {
            var user = await _context.Users
                .Include(u => u.UserHistories)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == updateRoleDTO.UserId);

            if (user == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "User not found." };
            }
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == updateRoleDTO.DepartmentId);
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == updateRoleDTO.RoleId);

            if (department == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "Department not found." };
            }

            if (role == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "Role not found." };
            }
            // Check if the user being updated is currently an admin
            if (user.Role.Name == "Admin")
            {
                // Count the number of current admins in the system
                var adminCount = await _context.Users.CountAsync(u => u.Role.Name == "Admin");

                // If there is only one admin left, prevent the update
                if (adminCount <= 1)
                {
                    return new Response
                    {
                        Code = ResponseCode.BadRequest,
                        Message = "There must be at least one admin in the system."
                    };
                }
            }

            var previousHistory = user.UserHistories
                .Where(uh => uh.EndTime == null)
                .OrderByDescending(uh => uh.StartTime)
                .FirstOrDefault();

            if (previousHistory != null)
            {
                previousHistory.EndTime = DateTime.UtcNow.AddHours(7);
            }

            

            var newHistory = new UserHistory
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.UtcNow.AddHours(7),
                EndTime = null,
                RoleName = role.Name,
                DepartmentName = department.Name,
                UserId = updateRoleDTO.UserId,
                User = user
            };

            _context.UserHistories.Add(newHistory);

            user.DepartmentId = department.Id;
            user.RoleId = role.Id;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new Response { Code = ResponseCode.Success, Message = "Update Position Successfully!" };
        }

        public async Task ResetPassword(Guid userId, ResetPasswordDTO resetPasswordDto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
            {
                throw new Exception("New password and confirm password do not match");
            }

            CreatePasswordHash(resetPasswordDto.NewPassword, out byte[] newPasswordHash, out byte[] newPasswordSalt);

            user.PasswordHash = newPasswordHash;
            user.PasswordSalt = newPasswordSalt;
            user.UpdatedAt = DateTime.UtcNow.AddHours(7);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Response> ForgotPassword(string email)
        {
            // Validate email format
            if (!IsValidEmail(email))
            {
                return new Response {Code = ResponseCode.BadRequest, Message = "Invalid email format" };
            }

            var user = await _context.Users.Include(x => x.UserInformation).FirstOrDefaultAsync(x => x.UserInformation.Email == email);

            if (user == null)
            {
                return new Response {Code = ResponseCode.NotFound, Message = "User not found" };
            }

            string temporaryPassword = GenerateTemporaryPassword();
            CreatePasswordHash(temporaryPassword, out byte[] tempPasswordHash, out byte[] tempPasswordSalt);

            user.TemporaryPasswordHash = tempPasswordHash;
            user.TemporaryPasswordSalt = tempPasswordSalt;
            user.TemporaryPasswordExpires = DateTime.UtcNow.AddHours(7).AddMinutes(5);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            var subject = "Your Temporary Password";
            var body = $@"
<div style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
    <h2 style='color: #007BFF;'>Hello {user.UserInformation.FullName},</h2>

    <p>
        <strong>Your temporary password:</strong> 
        <span style='color: #d9534f;'>{temporaryPassword}</span>
    </p>

    <p style='background-color: #f8d7da; color: #721c24; padding: 10px; border-radius: 5px;'>
        Please note that this temporary password is invalid after 5 minutes!
    </p>

    <p>Best regards,</p>

</div>";

            var sendEmail = _emailDAO.SendEmail(email, subject, body);

            var resetPasswordDto = new ResetPasswordDTO
            {
                NewPassword = temporaryPassword,
                ConfirmPassword = temporaryPassword
            };
            await ResetPassword(user.Id, resetPasswordDto);

            if (sendEmail != null)
            {
                return new Response {Code = ResponseCode.Success, Message = "Temporary password is sent to your email!" };
            }
            return new Response {Code = ResponseCode.BadRequest, Message = "Cannot send email!" };
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }



        private string GenerateTemporaryPassword()
        {
            var upper = Enumerable.Range('A', 26).Select(c => (char)c).ToArray();
            var lower = Enumerable.Range('a', 26).Select(c => (char)c).ToArray();
            var digits = Enumerable.Range('0', 10).Select(c => (char)c).ToArray();
            var special = "!@#$%^&*()_+[]{}|;:,.<>?".ToCharArray();
            var allChars = upper.Concat(lower).Concat(digits).Concat(special).ToArray();
            var random = new Random();

            var password = new List<char>
    {
        upper[random.Next(upper.Length)],
        lower[random.Next(lower.Length)],
        digits[random.Next(digits.Length)],
        special[random.Next(special.Length)]
    };

            // Thêm các ký tự ngẫu nhiên khác để đảm bảo độ dài mật khẩu là 8
            while (password.Count < 8)
            {
                password.Add(allChars[random.Next(allChars.Length)]);
            }

            // Trộn ngẫu nhiên các ký tự trong mật khẩu
            return new string(password.OrderBy(_ => random.Next()).ToArray());
        }



        private string GenerateCodeForUserInformation(string fullName)
        {
            fullName = Utils.Utils.RemoveSign4VietnameseString(fullName);

            string[] nameParts = fullName.Split(' ');
            string newEmployeeCode = "";
            foreach (string part in nameParts.Reverse())
            {
                if (!string.IsNullOrEmpty(part))
                {
                    newEmployeeCode += part[0];
                }
            }

            newEmployeeCode = newEmployeeCode.ToUpper();


           var existingUser = _context.UserInformations
               .Where(x => x.Code.StartsWith(newEmployeeCode))
               .OrderByDescending(x => x.Code)
               .FirstOrDefault();

            int number = 1;
            if (existingUser != null)
            {
                string lastUserCode = existingUser.Code;
                string lastNumberPart = lastUserCode.Substring(newEmployeeCode.Length);

                if (int.TryParse(lastNumberPart, out int lastNumber))
                {
                    number = lastNumber + 1;
                }
            }

            newEmployeeCode = newEmployeeCode + number.ToString();

            return newEmployeeCode;
        }
        public int GetTotalUserCount(Guid? departmentId)
        {
            return _context.Users
                .Include(x => x.UserInformation)
                .Where(u => !u.IsDeleted &&
                            u.UserInformation.Status == "Active" &&
                            (!departmentId.HasValue || u.DepartmentId == departmentId.Value))
                .Count();
        }

        public double CalculateUserGrowthPercentage(Guid? departmentId)
        {
            var currentDate = DateTime.UtcNow.AddHours(7);
            var currentMonthCount = _context.Users
                .Include(x => x.UserInformation)
                .Where(u => !u.IsDeleted &&
                            u.UserInformation.Status == "Active" &&
                            (!departmentId.HasValue || u.DepartmentId == departmentId.Value) &&
                            u.CreatedAt.Month == currentDate.Month &&
                            u.CreatedAt.Year == currentDate.Year)
                .Count();

            var previousMonthDate = currentDate.AddMonths(-1);
            var previousMonthCount = _context.Users
                .Include(x => x.UserInformation)
                .Where(u => !u.IsDeleted &&
                            u.UserInformation.Status == "Active" &&
                            (!departmentId.HasValue || u.DepartmentId == departmentId.Value) &&
                            u.CreatedAt.Month == previousMonthDate.Month &&
                            u.CreatedAt.Year == previousMonthDate.Year)
                .Count();

            if (previousMonthCount == 0)
            {
                return currentMonthCount == 0 ? 0 : 100;
            }

            return ((double)(currentMonthCount - previousMonthCount) / previousMonthCount) * 100;
        }

        public async Task<PaginatedList<UserGroup_UserDTO>> GetUsers(int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.Users
                .Include(u => u.UserGroup_Users)
                    .ThenInclude(ugu => ugu.UserGroup)
                .Include(u => u.UserInformation)
                .Where(u => !u.IsDeleted)
                .AsQueryable();

            var count = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var users = await query
                .OrderBy(u => u.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var userDTOs = _mapper.Map<List<UserGroup_UserDTO>>(users);

            return new PaginatedList<UserGroup_UserDTO>(userDTOs, pageIndex, totalPages, count);
        }

        public async Task<Response> DeleteUser(Guid userId)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.UserGroup_Users)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    return new Response { Code = ResponseCode.NotFound, Message = "User not found" };
                }

                // Set isDeleted to true
                user.IsDeleted = true;

                // Remove user from all UserGroups
                _context.UserGroup_Users.RemoveRange(user.UserGroup_Users);

                // Update user
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return new Response { Code = ResponseCode.Success, Message = "User deleted successfully" };
            }
            catch (Exception ex)
            {
                return new Response { Code = ResponseCode.InternalServerError, Message = $"Failed to delete user: {ex.Message}" };
            }
        }
    }
}

