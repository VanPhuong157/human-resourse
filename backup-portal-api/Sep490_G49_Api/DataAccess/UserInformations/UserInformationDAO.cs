using AutoMapper;
using BusinessObjects.DTO.UserInformation;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.UserInformations
{
    public class UserInformationDAO
    {
        private SEP490_G49Context _context;
        private IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHubContext<NotificationHub> _notificationHub;

        public UserInformationDAO(SEP490_G49Context context, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHubContext<NotificationHub> notificationHub)
        {
            _mapper = mapper;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _notificationHub = notificationHub;
        }
        public async Task<PaginatedList<UserDetailsWithoutFamilyDTO>> GetUserInformations(
            string? name = null,
            string? department = null,
            string? role = null,
            string? typeOfWork = null,
            string? status = null,
            int pageIndex = 1,
            int pageSize = 10)
        {
            var query = _context.UserInformations
                .Include(ui => ui.UserFiles)
                .Include(ui => ui.User)
                    .ThenInclude(u => u.Department)
                .Include(ui => ui.User)
                    .ThenInclude(ur => ur.Role)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                var lowerName = name.ToLower();
                query = query.Where(ui => ui.FullName.ToLower().Contains(lowerName));
            }

            if (!string.IsNullOrEmpty(department))
            {
                query = query.Where(ui => ui.User.Department.Name == department);
            }

            if (!string.IsNullOrEmpty(role))
            {
                query = query.Where(ui => ui.User.Role.Name == role);
            }

            if (!string.IsNullOrEmpty(typeOfWork))
            {
                query = query.Where(ui => ui.TypeOfWork == typeOfWork);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(ui => ui.Status == status);
            }
            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var userInformations = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var userDetailsList = new List<UserDetailsWithoutFamilyDTO>();

            foreach (var userInformation in userInformations)
            {
                var userDetailsDto = _mapper.Map<UserDetailsWithoutFamilyDTO>(userInformation);
                userDetailsDto.DepartmentName = userInformation.User?.Department?.Name;
                userDetailsDto.RoleNames = userInformation.User?.Role.Name;
                userDetailsList.Add(userDetailsDto);
            }

            return new PaginatedList<UserDetailsWithoutFamilyDTO>(userDetailsList, pageIndex, totalPages, totalRecords);
        }

        public async Task<UserDetailsWithoutFamilyDTO> GetByUserId(Guid userId)
        {
            var userInformation = await _context.UserInformations
                .Include(ui => ui.UserFiles)
                .Include(ui => ui.User)
                    .ThenInclude(u => u.Department)
                .Include(ui => ui.User)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(ui => ui.UserId == userId);
            var userDetails = _mapper.Map<UserDetailsWithoutFamilyDTO>(userInformation);
            userDetails.DepartmentName = userInformation.User?.Department?.Name;
            userDetails.RoleNames = userInformation.User?.Role.Name;
            return userDetails;
        }

        public async Task<PersonalDetailDTO> GetPersonalByUserId(Guid userId)
        {
            var userInformation = await _context.UserInformations
                .Include(ui => ui.UserFiles)
                .Include(u => u.User).ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(ui => ui.UserId == userId);

            if (userInformation == null)
            {
                return null;
            }

            var personalDetailDTO = _mapper.Map<PersonalDetailDTO>(userInformation);
            if (userInformation.User.Role != null)
            {
                personalDetailDTO.RoleName = userInformation.User.Role.Name;
            }

            return personalDetailDTO;
        }

        public async Task<UserDetailsWithoutFamilyDTO> GetById(Guid id)
        {
            var userInformation = await _context.UserInformations
                .Include(ui => ui.UserFiles)
                .FirstOrDefaultAsync(ui => ui.Id == id);
            return _mapper.Map<UserDetailsWithoutFamilyDTO>(userInformation);
        }

        public async Task<UserFamilyDTO> GetUserFamily(Guid userId)
        {
            var userInformation = await _context.UserInformations
                .Include(u => u.FamilyInformation)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (userInformation == null)
            {
                throw new Exception("Not found");
            }

            var userFamilyDto = _mapper.Map<UserFamilyDTO>(userInformation);
            return userFamilyDto;
        }

        public async Task<Response> EditPersonalProfile(Guid userId, UpdatePersonalProfileDTO request)
        {
            try
            {
                var user = await _context.UserInformations
                    .Include(x => x.User).ThenInclude(x => x.Role)
                    .FirstOrDefaultAsync(u => u.UserId == userId);
                _mapper.Map(request, user);
                user.UpdatedAt = DateTime.UtcNow.AddHours(7);
                if (request.ImageFile != null)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(request.ImageFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return new Response { Code = ResponseCode.BadRequest, Message = "Only image files are allowed." };
                    }

                    var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                    if (!allowedMimeTypes.Contains(request.ImageFile.ContentType))
                    {
                        return new Response { Code = ResponseCode.BadRequest, Message = "Only image files are allowed." };
                    }

                    var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", "Images", userId.ToString());
                    Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = request.ImageFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.ImageFile.CopyToAsync(fileStream);
                    }

                    user.Avatar = $"/Uploads/Images/{userId}/{uniqueFileName}";
                }
               
                _context.UserInformations.Update(user);
                await _context.SaveChangesAsync();

                var message = "Your account has been Updated";
                var redirectUrl = "";
                var notification = new Notification
                {
                    Id = Guid.NewGuid(),
                    Message = message,
                    CreatedAt = DateTime.UtcNow.AddHours(7),
                    IsRead = false,
                    UserId = userId,
                    RedirectUrl = redirectUrl,
                    User = user.User
                };

                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                await _notificationHub.Clients.All.SendAsync("ReceiveNotification", userId, new Notification
                {
                    CreatedAt = DateTime.UtcNow.AddHours(7),
                    Message = message,
                    RedirectUrl = redirectUrl
                });
                return new Response { Code = ResponseCode.Success, Message = "Edit successfully!" };
            }
            catch(Exception ex)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Edit failure" };
            }
        }
        public async Task<Response> SaveUserDetails(UserDetailsDTO userDetailsDto)
        {
            try
            {
                var userInformation = await _context.UserInformations
                .FirstOrDefaultAsync(ui => ui.UserId == userDetailsDto.UserId);

                if (userInformation == null)
                {
                    userInformation = _mapper.Map<UserInformation>(userDetailsDto);
                    userInformation.Id = Guid.NewGuid();
                    userInformation.CreatedAt = DateTime.UtcNow.AddHours(7);
                    userInformation.UpdatedAt = DateTime.UtcNow.AddHours(7);
                    _context.UserInformations.Add(userInformation);
                }
                else
                {
                    _mapper.Map(userDetailsDto, userInformation);
                    userInformation.UpdatedAt = DateTime.UtcNow.AddHours(7);
                    _context.UserInformations.Update(userInformation);
                }
                if (userDetailsDto.ImageFile != null)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(userDetailsDto.ImageFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return new Response { Code = ResponseCode.BadRequest, Message = "Only image files are allowed." };
                    }

                    var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                    if (!allowedMimeTypes.Contains(userDetailsDto.ImageFile.ContentType))
                    {
                        return new Response { Code = ResponseCode.BadRequest, Message = "Only image files are allowed." };
                    }

                    var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", "Images", userDetailsDto.UserId.ToString());
                    Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = userDetailsDto.ImageFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await userDetailsDto.ImageFile.CopyToAsync(fileStream);
                    }

                    userInformation.Avatar = $"/Uploads/Images/{userDetailsDto.UserId}/{uniqueFileName}";
                }


                if (userDetailsDto.AdditionalFiles != null && userDetailsDto.AdditionalFiles.Any())
                {
                    var allowedExtensions = new[] { ".docx", ".pdf" };

                    foreach (var file in userDetailsDto.AdditionalFiles)
                    {
                        var fileExtension = Path.GetExtension(file.FileName).ToLower();
                        if (!allowedExtensions.Contains(fileExtension))
                        {
                            return new Response { Code = ResponseCode.BadRequest, Message = "Only .docx and .pdf files are allowed." };
                        }

                        var allowedMimeTypes = new[] { "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/pdf" };
                        if (!allowedMimeTypes.Contains(file.ContentType))
                        {
                            return new Response { Code = ResponseCode.BadRequest, Message = "Only .docx and .pdf files are allowed." };
                        }

                        var userUploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", "Documents", userDetailsDto.UserId.ToString());
                        Directory.CreateDirectory(userUploadsFolder);

                        var uniqueFileName = file.FileName;
                        var filePath = Path.Combine(userUploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        var userFile = new UserFile
                        {
                            Id = Guid.NewGuid(),
                            UserId = userInformation.Id,
                            FileName = file.FileName,
                            FilePath = $"/Uploads/Documents/{userDetailsDto.UserId}/{uniqueFileName}",
                            UploadedAt = DateTime.UtcNow.AddHours(7),
                        };

                        _context.UserFiles.Add(userFile);
                    }
                }

                await _context.SaveChangesAsync();

                return new Response { Code = ResponseCode.Success, Message = "Save successfully!" };
            }
            catch(Exception ex)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Save failure!" };
            }
        }

        public async Task<Response> CreateFamilyMember(Guid userId, CreateUpdateFamilyDTO familyDto)
        {
            try
            {
                var userInformation = await _context.UserInformations
                .Include(u => u.FamilyInformation)
                .FirstOrDefaultAsync(u => u.UserId == userId);

                if (userInformation == null)
                {
                    return new Response { Code = ResponseCode.NotFound, Message = "Not found information user." };
                }

                var familyMember = new Family
                {
                    Id = Guid.NewGuid(),
                    FullName = familyDto.FullName,
                    Relationship = familyDto.Relationship,
                    DateOfBirth = familyDto.DateOfBirth,
                    Job = familyDto.Job,
                    PhoneNumber = familyDto.PhoneNumber,
                    UserInformationId = userInformation.Id,
                    UserInformation = userInformation
                };

                _context.Families.Add(familyMember);
                await _context.SaveChangesAsync();
                var userFamilyDto = _mapper.Map<UserFamilyDTO>(userInformation);
                return new Response { Code = ResponseCode.Success, Message = "Create successfully" };
            }
            catch(Exception ex)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Create failure!" };
            }
        }


        public async Task<Response> EditFamilyMember(Guid userId, Guid familyId, CreateUpdateFamilyDTO familyDto)
        {
            try
            {
                var userInformation = await _context.UserInformations
                .Include(u => u.FamilyInformation)
                .FirstOrDefaultAsync(u => u.UserId == userId);

                if (userInformation == null)
                {
                    return new Response { Code = ResponseCode.NotFound, Message = "Not found user" };
                }

                var familyMember = userInformation.FamilyInformation.FirstOrDefault(f => f.Id == familyId);
                if (familyMember == null)
                {
                    return new Response { Code = ResponseCode.NotFound, Message = "Not found family" };
                }

                familyMember.FullName = familyDto.FullName;
                familyMember.Relationship = familyDto.Relationship;
                familyMember.DateOfBirth = familyDto.DateOfBirth;
                familyMember.Job = familyDto.Job;
                familyMember.PhoneNumber = familyDto.PhoneNumber;

                await _context.SaveChangesAsync();
                return new Response { Code = ResponseCode.Success, Message = "Edit successfully!" };
            }
            catch(Exception ex)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Edit failure!" };
            }
        }

        public async Task<Response> EditStatus(Guid userId, EditStatusDTO editStatusDTO)
        {
            try
            {
                var userInformation = await _context.UserInformations.FirstOrDefaultAsync(u => u.UserId == userId);
                if (userInformation == null)
                {
                    return new Response { Code = ResponseCode.NotFound, Message = "User not found." };
                }

                userInformation.Status = editStatusDTO.NewStatus;
                userInformation.UpdatedAt = DateTime.UtcNow.AddHours(7);

                _context.UserInformations.Update(userInformation);
                await _context.SaveChangesAsync();

                return new Response { Code = ResponseCode.Success, Message = "Status updated successfully!" };
            }
            catch (Exception ex)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Status update failed." };
            }
        }

        public async Task<Response> DeleteFamily(Guid id)
        {
            try
            {
                var family = await _context.Families.FindAsync(id);
                if (family == null)
                {
                    return new Response { Code = ResponseCode.NotFound, Message = "Not found member in family." };
                }

                _context.Families.Remove(family);
                await _context.SaveChangesAsync();
                return new Response { Code = ResponseCode.Success, Message = "Delete member successfully!" };
            }
            catch
            {
                return new Response { Code = ResponseCode.InternalServerError, Message = "Delete member failed!" };
            }
        }

        public async Task<PaginatedList<UserDetailsWithoutFamilyDTO>> GetUserInformationsByDepartment(
            string? name = null,
            Guid? departmentId = null,
            int pageIndex = 1,
            int pageSize = 10)
        {
            var query = _context.UserInformations
                .Include(ui => ui.UserFiles)
                .Include(ui => ui.User)
                    .ThenInclude(u => u.Department)
                .Include(ui => ui.User)
                    .ThenInclude(ur => ur.Role)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                var lowerName = name.ToLower();
                query = query.Where(ui => ui.FullName.ToLower().Contains(lowerName));
            }

            if (departmentId != null)
            {
                query = query.Where(ui => ui.User.DepartmentId == departmentId);
            }

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var userInformations = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var userDetailsList = new List<UserDetailsWithoutFamilyDTO>();

            foreach (var userInformation in userInformations)
            {
                var userDetailsDto = _mapper.Map<UserDetailsWithoutFamilyDTO>(userInformation);
                userDetailsDto.DepartmentName = userInformation.User?.Department?.Name;
                userDetailsDto.RoleNames = userInformation.User?.Role.Name;
                userDetailsList.Add(userDetailsDto);
            }

            return new PaginatedList<UserDetailsWithoutFamilyDTO>(userDetailsList, pageIndex, totalPages, totalRecords);
        }

        public async Task<UserInformation?> GetAvatarImageByIdAsync(Guid id)
        {
            return await _context.UserInformations
                .FirstOrDefaultAsync(u => u.UserId == id);
        }
    }
}
