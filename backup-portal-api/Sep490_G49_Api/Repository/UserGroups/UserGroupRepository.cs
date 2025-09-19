using AutoMapper;
using BusinessObjects.DTO.UserGroup;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.EntityFrameworkCore;

namespace Repository.UserGroups
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly SEP490_G49Context _context;
        private readonly IMapper _mapper;

        public UserGroupRepository(SEP490_G49Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<UserGroupDTO>> GetUserGroups(
            int pageIndex = 1,
            int pageSize = 10,
            string? name = null,
            string? role = null,
            string? user = null)
        {
            var query = _context.UserGroups
                .Include(ug => ug.UserGroup_Users)
                    .ThenInclude(ugu => ugu.User)
                .Include(ug => ug.UserGroup_Roles)
                    .ThenInclude(ugr => ugr.Role)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(ug => ug.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(role))
            {
                query = query.Where(ug => ug.UserGroup_Roles.Any(ugr => ugr.Role.Name.Contains(role)));
            }

            if (!string.IsNullOrEmpty(user))
            {
                query = query.Where(ug => ug.UserGroup_Users.Any(ugu => ugu.User.Username.Contains(user)));
            }

            var count = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var userGroups = await query
                .OrderBy(ug => ug.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var userGroupDTOs = userGroups.Select(ug => new UserGroupDTO
            {
                Id = ug.Id,
                Name = ug.Name,
                Description = ug.Description,
                CreatedAt = ug.CreatedAt,
                UpdatedAt = ug.UpdatedAt,
                Users = ug.UserGroup_Users.Select(ugu => ugu.User.Username).ToList(),
                Roles = ug.UserGroup_Roles.Select(ugr => ugr.Role.Name).ToList()
            }).ToList();

            return new PaginatedList<UserGroupDTO>(userGroupDTOs, pageIndex, totalPages, count);
        }


        public async Task<Response> EditUserGroup(Guid id, UserGroupEditDTO editDto)
        {
            try
            {
                var userGroup = await _context.UserGroups
                    .Include(ug => ug.UserGroup_Users)
                    .Include(ug => ug.UserGroup_Roles)
                    .FirstOrDefaultAsync(ug => ug.Id == id);

                if (userGroup == null)
                {
                    return new Response { Code = ResponseCode.NotFound, Message = "UserGroup not found" };
                }

                // Kiểm tra trùng tên với các UserGroup khác
                var existingUserGroup = await _context.UserGroups
                    .FirstOrDefaultAsync(ug => ug.Name.Equals(editDto.Name) && ug.Id != id);

                if (existingUserGroup != null)
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "UserGroup name already exists." };
                }

                // Lọc các Role có Type là "Custom"
                var roles = await _context.Roles
                    .Where(r => editDto.RoleIds.Contains(r.Id) && r.Type == "Custom")
                    .ToListAsync();

                // Kiểm tra nếu có bất kỳ Role nào không phải là "Custom"
                if (!roles.Any() || roles.Count != editDto.RoleIds.Count)
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "All roles must be of type 'Custom' and at least one role is required." };
                }

                // Cập nhật UserGroup
                userGroup.Name = editDto.Name;
                userGroup.Description = editDto.Description;
                userGroup.UpdatedAt = DateTime.UtcNow;

                // Cập nhật danh sách UserGroup_Users và UserGroup_Roles
                userGroup.UserGroup_Users = editDto.UserIds.Select(userId => new UserGroup_User { UserGroupId = userGroup.Id, UserId = userId }).ToList();
                userGroup.UserGroup_Roles = roles.Select(role => new UserGroup_Role { UserGroupId = userGroup.Id, RoleId = role.Id }).ToList();

                // Cập nhật UserGroup trong context và lưu thay đổi
                _context.UserGroups.Update(userGroup);
                await _context.SaveChangesAsync();

                return new Response { Code = ResponseCode.Success, Message = "UserGroup updated successfully" };
            }
            catch (Exception ex)
            {
                return new Response { Code = ResponseCode.InternalServerError, Message = "Edit user group failed." };
            }
        }


        public async Task<Response> DeleteUserGroup(Guid id)
        {
            var userGroup = await _context.UserGroups
                .Include(ug => ug.UserGroup_Users)
                .Include(ug => ug.UserGroup_Roles)
                .FirstOrDefaultAsync(ug => ug.Id == id);

            if (userGroup == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "UserGroup not found" };
            }

            _context.UserGroup_Users.RemoveRange(userGroup.UserGroup_Users);

            _context.UserGroup_Roles.RemoveRange(userGroup.UserGroup_Roles);

            _context.UserGroups.Remove(userGroup);
            await _context.SaveChangesAsync();

            return new Response { Code = ResponseCode.Success, Message = "UserGroup deleted successfully" };
        }
        public async Task<Response> CreateUserGroup(UserGroupCreateDTO createDto)
        {
            try
            {
                // Kiểm tra xem tên UserGroup có bị trùng lặp hay không
                var existingUserGroup = await _context.UserGroups
            .FirstOrDefaultAsync(ug => ug.Name.Equals(createDto.Name));

                if (existingUserGroup != null)
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "UserGroup name already exists." };
                }

                // Lọc các Role có Type là "Custom"
                var roles = await _context.Roles
                    .Where(r => createDto.RoleIds.Contains(r.Id) && r.Type == "Custom")
                    .ToListAsync();

                // Kiểm tra nếu không có role nào hoặc có role không phải là "Custom"
                if (!roles.Any() || roles.Count != createDto.RoleIds.Count)
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "All roles must be of type 'Custom' and at least one role is required." };
                }

                // Tạo UserGroup mới
                var userGroup = new UserGroup
                {
                    Id = Guid.NewGuid(),
                    Name = createDto.Name,
                    Description = createDto.Description,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserGroup_Users = createDto.UserIds.Select(userId => new UserGroup_User { UserId = userId }).ToList(),
                    UserGroup_Roles = roles.Select(role => new UserGroup_Role { RoleId = role.Id }).ToList()
                };

                // Thêm UserGroup vào context và lưu thay đổi
                _context.UserGroups.Add(userGroup);
                await _context.SaveChangesAsync();

                return new Response { Code = ResponseCode.Success, Message = "UserGroup created successfully" };
            }
            catch (Exception ex)
            {
                return new Response { Code = ResponseCode.InternalServerError, Message = "Create user group failed." };
            }
        }
        public async Task<UserGroupDTO?> GetUserGroupById(Guid id)
        {
            try
            {
                var userGroup = await _context.UserGroups
                    .Include(ug => ug.UserGroup_Roles)
                        .ThenInclude(ugr => ugr.Role)
                    .Include(ug => ug.UserGroup_Users)
                        .ThenInclude(ugu => ugu.User)
                    .FirstOrDefaultAsync(ug => ug.Id == id);

                if (userGroup == null)
                {
                    return null;
                }

                var userGroupDto = _mapper.Map<UserGroupDTO>(userGroup);

                return userGroupDto;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
