using AutoMapper;
using BusinessObjects.DTO.Department;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DataAccess.Departments
{
    public class DepartmentDAO
    {
        private readonly SEP490_G49Context _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DepartmentDAO(SEP490_G49Context context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<DepartmentDTO>> GetAllDepartments(int pageIndex, int pageSize)
        {
            var totalCount = await _context.Departments.CountAsync();

            var departments = await _context.Departments
                .Include(d => d.Users)
                .OrderBy(d => d.Name) // Sắp xếp theo Name hoặc bất kỳ tiêu chí nào bạn muốn
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var users = await _context.Users.ToListAsync();

            var departmentDTOs = departments.Select(d => new DepartmentDTO
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                CreatedAt = d.CreatedAt,
                CreatedBy = users.FirstOrDefault(x => x.Id == d.CreatedBy)?.Username ?? null
            }).ToList();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PaginatedList<DepartmentDTO>(departmentDTOs, pageIndex, totalPages, totalCount);
        }


        public async Task<Response> CreateDepartment(CreateDepartmentDTO departmentDTO)
        {
            if (departmentDTO == null)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Department DTO is null." };
            }

            if (string.IsNullOrEmpty(departmentDTO.DepartmentName))
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "DepartmentName is required." };
            }

            var existingDepartment = await _context.Departments.FirstOrDefaultAsync(d => d.Name == departmentDTO.DepartmentName);
            if (existingDepartment != null)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "DepartmentName already exists." };
            }
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "User not found", Data = null };
            }
            var department = _mapper.Map<Department>(departmentDTO);
            department.Id = Guid.NewGuid();
            department.Name = departmentDTO.DepartmentName;
            department.Description = departmentDTO.Description;
            department.CreatedAt = DateTime.UtcNow.AddHours(7);
            department.CreatedBy = Guid.Parse(userId);
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return new Response { Code = ResponseCode.Success, Message = "Department created successfully." };
        }


        public async Task<Response> UpdateDepartment(Guid id, CreateDepartmentDTO departmentDTO)
        {
            if (departmentDTO == null)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Department DTO is null." };
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "Department not found." };
            }

            if (string.IsNullOrEmpty(departmentDTO.DepartmentName))
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Department name is required." };
            }

            var existingDepartment = await _context.Departments.FirstOrDefaultAsync(d => d.Name == departmentDTO.DepartmentName && d.Id != id);
            if (existingDepartment != null)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Department name already exists." };
            }
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "User not found", Data = null };
            }
            department.Name = departmentDTO.DepartmentName;
            department.Description = departmentDTO.Description;
            department.UpdatedAt = DateTime.UtcNow.AddHours(7);
            department.UpdatedBy = Guid.Parse(userId);
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();

            return new Response { Code = ResponseCode.Success, Message = "Department edited successfully." };
        }

    }
}
