using BusinessObjects.DTO.Department;
using BusinessObjects.Models;
using BusinessObjects.Response;

namespace Repository.Departments
{
    public interface IDepartmentRepository
    {
        Task<PaginatedList<DepartmentDTO>> GetAllDepartments(int pageIndex, int pageSize);
        Task<Response> CreateDepartment(CreateDepartmentDTO departmentDTO);
        Task<Response> UpdateDepartment(Guid id, CreateDepartmentDTO departmentDTO);
    }
}
