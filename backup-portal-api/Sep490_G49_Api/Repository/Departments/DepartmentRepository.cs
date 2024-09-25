using BusinessObjects.DTO.Department;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.Departments;

namespace Repository.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DepartmentDAO _dao;
        public DepartmentRepository(DepartmentDAO dao)
        {
            _dao = dao;
        }
        public async Task<Response> CreateDepartment(CreateDepartmentDTO departmentDTO)
        {
            return await _dao.CreateDepartment(departmentDTO);
        }

        public async Task<PaginatedList<DepartmentDTO>> GetAllDepartments(int pageIndex, int pageSize)
        {
            return await _dao.GetAllDepartments(pageIndex, pageSize);
        }

        public async Task<Response> UpdateDepartment(Guid id, CreateDepartmentDTO departmentDTO)
        {
            return await _dao.UpdateDepartment(id, departmentDTO);
        }
    }
}
