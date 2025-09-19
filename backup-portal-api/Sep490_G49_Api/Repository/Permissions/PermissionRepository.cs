using AutoMapper;
using BusinessObjects.DTO.Permission;
using BusinessObjects.Models;
using BusinessObjects.Response;

using Microsoft.EntityFrameworkCore;

namespace Repository.Permissions
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly SEP490_G49Context _context;
        private readonly IMapper _mapper;

        public PermissionRepository(SEP490_G49Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<PermissionResponseDTO>> GetAllPermissions(
    string? name = null,
    int pageIndex = 1,
    int pageSize = 10)
        {
            // Truy vấn tất cả các quyền và áp dụng lọc nếu có
            var query = _context.Permissions.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            // Lấy các quyền trong trang hiện tại
            var permissions = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Ánh xạ thành DTO
            var permissionDtos = _mapper.Map<IEnumerable<PermissionResponseDTO>>(permissions);

            // Tính số trang tổng cộng
            var count = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            // Tạo đối tượng PaginatedList và trả về
            return new PaginatedList<PermissionResponseDTO>(
                permissionDtos.ToList(),
                pageIndex,
                totalPages,
                count
            );
        }

        public async Task<PaginatedList<PermissionResponseDTO>> GetUserPermissions(
    Guid userId,
    string? name = null,
    int pageIndex = 1,
    int pageSize = 10)
        {
            try
            {
                // Lấy permissions từ roles của user
                var rolePermissionsQuery = _context.Users
                    .Where(u => u.Id == userId)
                    .Include(u => u.Role)
                    .ThenInclude(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                    .SelectMany(u => u.Role.RolePermissions.Where(rp => rp.IsEnabled == true).Select(rp => rp.Permission));

                // Lấy permissions từ user groups của user
                var userGroupPermissionsQuery = _context.Users
            .Where(u => u.Id == userId)
            .Include(u => u.UserGroup_Users)
                .ThenInclude(ugu => ugu.UserGroup)
                .ThenInclude(ug => ug.UserGroup_Roles)
                .ThenInclude(ugr => ugr.Role)
                .ThenInclude(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
            .Where(u => u.UserGroup_Users
                .SelectMany(ugu => ugu.UserGroup.UserGroup_Roles)
                .Any(ugr => ugr.Role.RolePermissions.Any(rp => rp.IsEnabled == true)))
            .SelectMany(u => u.UserGroup_Users
                .SelectMany(ugu => ugu.UserGroup.UserGroup_Roles
                    .SelectMany(ugr => ugr.Role.RolePermissions
                        .Where(rp => rp.IsEnabled == true)
                        .Select(rp => rp.Permission))));

                // Hợp nhất hai danh sách permissions
                var userPermissionsQuery = rolePermissionsQuery
                    .Union(userGroupPermissionsQuery);

                // Áp dụng bộ lọc nếu có
                if (!string.IsNullOrEmpty(name))
                {
                    userPermissionsQuery = userPermissionsQuery.Where(p => p.Name == name);
                }

                // Tính tổng số quyền
                var count = await userPermissionsQuery.CountAsync();

                // Lấy các quyền trong trang hiện tại
                var permissions = await userPermissionsQuery
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                // Ánh xạ thành DTO
                var permissionDtos = _mapper.Map<IEnumerable<PermissionResponseDTO>>(permissions);

                // Tính số trang tổng cộng
                var totalPages = (int)Math.Ceiling(count / (double)pageSize);

                // Tạo đối tượng PaginatedList và trả về
                return new PaginatedList<PermissionResponseDTO>(
                    permissionDtos.ToList(),
                    pageIndex,
                    totalPages,
                    count
                );
            }
            catch (AutoMapperMappingException ex)
            {
                // Ghi lại thông tin lỗi chi tiết
                Console.WriteLine($"Mapping error: {ex.Message}");
                Console.WriteLine($"Inner exception: {ex.InnerException?.Message}");
                throw;
            }
        }

        public async Task<Response> EditUserPermissions(Guid userId, List<Guid> permissionIds)
        {
            // Lấy tất cả các quyền hiện tại của người dùng từ cơ sở dữ liệu
            var userPermissions = await _context.UserPermissions.Where(up => up.UserId == userId).ToListAsync();

            // Đặt tất cả các quyền hiện tại của người dùng thành không kích hoạt (IsEnabled = false)
            foreach (var up in userPermissions)
            {
                up.IsEnabled = false;
            }

            // Duyệt qua danh sách các quyền mới
            foreach (var permissionId in permissionIds)
            {
                // Tìm quyền hiện tại của người dùng tương ứng với permissionId
                var userPermission = userPermissions.FirstOrDefault(up => up.PermissionId == permissionId);

                // Nếu quyền đã tồn tại, đặt lại IsEnabled = true
                if (userPermission != null)
                {
                    userPermission.IsEnabled = true;
                }
                // Nếu quyền không tồn tại, thêm quyền mới vào danh sách quyền của người dùng
                else
                {
                    _context.UserPermissions.Add(new UserPermission
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        PermissionId = permissionId,
                        IsEnabled = true
                    });
                }
            }

            // Lưu các thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();
            return new Response { Code = ResponseCode.Success, Message = "Edit permission successfully!" };
        }

        public async Task<PaginatedList<PermissionResponseDTO>> GetRolePermissions(
    Guid roleId,
    string? name = null,
    int pageIndex = 1,
    int pageSize = 10)
        {
            // Truy vấn tất cả quyền của vai trò và lọc theo trạng thái kích hoạt
            var query = from rp in _context.RolePermissions
                        join p in _context.Permissions on rp.PermissionId equals p.Id
                        where rp.RoleId == roleId && rp.IsEnabled
                        select p;

            // Áp dụng lọc theo tên nếu có
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            // Tính tổng số quyền
            var count = await query.CountAsync();

            // Lấy các quyền trong trang hiện tại
            var permissions = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Ánh xạ thành DTO
            var permissionDtos = _mapper.Map<IEnumerable<PermissionResponseDTO>>(permissions);

            // Tính số trang tổng cộng
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            // Tạo đối tượng PaginatedList và trả về
            return new PaginatedList<PermissionResponseDTO>(
                permissionDtos.ToList(),
                pageIndex,
                totalPages,
                count
            );
        }

        public async Task<Response> EditRolePermissions(Guid roleId, List<Guid> permissionIds)
        {
            // Lấy tất cả các quyền hiện tại của vai trò từ cơ sở dữ liệu
            var rolePermissions = await _context.RolePermissions.Where(rp => rp.RoleId == roleId).ToListAsync();

            // Đặt tất cả các quyền hiện tại của vai trò thành không kích hoạt (IsEnabled = false)
            foreach (var rp in rolePermissions)
            {
                rp.IsEnabled = false;
            }

            // Duyệt qua danh sách các quyền mới
            foreach (var permissionId in permissionIds)
            {
                // Tìm quyền hiện tại của vai trò tương ứng với permissionId
                var rolePermission = rolePermissions.FirstOrDefault(rp => rp.PermissionId == permissionId);

                // Nếu quyền đã tồn tại, đặt lại IsEnabled = true
                if (rolePermission != null)
                {
                    rolePermission.IsEnabled = true;
                }
                // Nếu quyền không tồn tại, thêm quyền mới vào danh sách quyền của vai trò
                else
                {
                    _context.RolePermissions.Add(new RolePermission
                    {
                        Id = Guid.NewGuid(),
                        RoleId = roleId,
                        PermissionId = permissionId,
                        IsEnabled = true
                    });
                }
            }

            // Lưu các thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();
            return new Response { Code = ResponseCode.Success, Message = "Edit role permissions successfully!" };
        }


    }
}
