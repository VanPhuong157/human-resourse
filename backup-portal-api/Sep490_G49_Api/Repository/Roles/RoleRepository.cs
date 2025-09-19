using AutoMapper;
using BusinessObjects.DTO.Role;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Repository.Roles
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SEP490_G49Context _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleRepository(SEP490_G49Context context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PaginatedList<RoleDTO>> GetRoles(
    int pageIndex = 1,
    int pageSize = 10,
    string? name = null,
    string? type = null)
        {
            var query = _context.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(r => r.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(r => r.Type == type);
            }

            var count = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var roles = await query
                .OrderByDescending(r => r.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var roleList = _mapper.Map<List<RoleDTO>>(roles);

            return new PaginatedList<RoleDTO>(roleList, pageIndex, totalPages, count);
        }

        public async Task<Response> CreateRole(CreateRoleDTO createRoleDTO)
        {
            try
            {
                if (await _context.Roles.AnyAsync(r => r.Name == createRoleDTO.Name))
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "Role name already exists", Data = null };
                }
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "User not found", Data = null };
                }
                var role = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = createRoleDTO.Name,
                    Description = createRoleDTO.Description,
                    CreatedAt = DateTime.Now,
                    CreatedBy = Guid.Parse(userId),
                    Type = createRoleDTO.Type
                };

                _context.Roles.Add(role);
                await _context.SaveChangesAsync();

                var roleDTO = _mapper.Map<RoleDTO>(role);
                return new Response { Code = ResponseCode.Success, Message = "Create role successfully", Data = roleDTO };
            }
            catch (Exception ex)
            {
                return new Response { Code = ResponseCode.InternalServerError, Message = "Create role failed", Data = null };
            }
        }

        public async Task<Response> EditRole(Guid id, CreateRoleDTO editRoleDTO)
        {
            try
            {
                var existingRole = await _context.Roles.FindAsync(id);
                if (existingRole == null)
                {
                    return new Response { Code = ResponseCode.NotFound, Message = "Role not found", Data = null };
                }
                if (await _context.Roles.AnyAsync(r => r.Name == editRoleDTO.Name && r.Id != id))
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "Role name already exists", Data = null };
                }
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "User not found", Data = null };
                }
                existingRole.Name = editRoleDTO.Name;
                existingRole.UpdatedAt = DateTime.UtcNow.AddHours(7);
                existingRole.Description = editRoleDTO.Description;
                existingRole.UpdatedBy = Guid.Parse(userId);
                existingRole.Type = editRoleDTO.Type;
                await _context.SaveChangesAsync();

                var roleDTO = _mapper.Map<RoleDTO>(existingRole);
                return new Response { Code = ResponseCode.Success, Message = "Edit role successfully", Data = roleDTO };
            }
            catch (Exception ex)
            {
                return new Response { Code = ResponseCode.InternalServerError, Message = "Edit role failed", Data = null };
            }
        }

        public async Task<Response> DeleteRole(Guid id)
        {
            try
            {
                var existingRole = await _context.Roles
                    .Include(r => r.UserGroup_Roles)
                    .Include(r => r.RolePermissions)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (existingRole == null)
                {
                    return new Response { Code = ResponseCode.NotFound, Message = "Role not found", Data = null };
                }

                if (existingRole.Type != "Custom")
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "Only roles with 'custom' type can be deleted", Data = null };
                }

                // Xóa UserGroup_Roles liên quan
                _context.UserGroup_Roles.RemoveRange(existingRole.UserGroup_Roles);

                // Xóa RolePermissions liên quan
                _context.RolePermissions.RemoveRange(existingRole.RolePermissions);

                // Xóa Role
                _context.Roles.Remove(existingRole);

                await _context.SaveChangesAsync();

                var roleDTO = _mapper.Map<RoleDTO>(existingRole);
                return new Response { Code = ResponseCode.Success, Message = "Delete role successfully", Data = roleDTO };
            }
            catch (Exception ex)
            {
                return new Response { Code = ResponseCode.InternalServerError, Message = $"Failed to delete role: {ex.Message}", Data = null };
            }
        }

    }
}
