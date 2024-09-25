using BusinessObjects.DTO.Department;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Mvc;
using Repository.Departments;

namespace Sep490_G49_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        [HttpGet]
        public async Task<PaginatedList<DepartmentDTO>> GetAllDepartments(int pageIndex = 1, int pageSize = 10)
        {
            return await _departmentRepository.GetAllDepartments(pageIndex, pageSize);
        }

        [HttpPost]
        public async Task<Response> CreateDepartment([FromBody] CreateDepartmentDTO departmentDTO)
        {
            if (departmentDTO == null)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Invalid request" };
            }

            return await _departmentRepository.CreateDepartment(departmentDTO);
        }

        [HttpPut("{id}")]
        public async Task<Response> EditDepartment(Guid id, [FromBody] CreateDepartmentDTO departmentDTO)
        {
            if (departmentDTO == null)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = "Invalid request" };
            }

            return await _departmentRepository.UpdateDepartment(id, departmentDTO);
        }
    }
}
