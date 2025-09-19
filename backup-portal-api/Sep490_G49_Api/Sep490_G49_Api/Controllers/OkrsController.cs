using BusinessObjects.DTO;
using BusinessObjects.DTO.Okr;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Mvc;
using Repository.Objectives;

[Route("api/[controller]")]
[ApiController]
public class OkrsController : ControllerBase
{
    private readonly IOkrRepository okrRepository;

    public OkrsController(IOkrRepository okrRepository)
    {
        this.okrRepository = okrRepository;
    }

    [HttpGet("{okrId}")]
    public async Task<OKRDetailsDTO> GetOkrById(Guid okrId)
    {
        return await okrRepository.GetOkrById(okrId);
    }
    [HttpPost]
    public async Task<Response> CreateOkr([FromForm] OKRCreateDTO okrCreateDTO)
    {
        if (okrCreateDTO == null)
        {
            return new Response { Code = ResponseCode.BadRequest, Message = "Request is null." };
        }

        return await okrRepository.CreateOkr(okrCreateDTO);
    }

    [HttpGet("by-department")]
    public async Task<PaginatedList<OKRDTO>> GetOkrsByDepartmentId(
           int pageIndex = 1,
           int pageSize = 10,
           string? title = null,
           string? type = null,
           string? scope = null,
           string? status = null,
           string? cycle = null,
           Guid? departmentId = null)
    {
        return await okrRepository.GetOkrsByDepartmentId(pageIndex, pageSize, title, type, scope, status,cycle, departmentId);
    }
    [HttpPut("{okrId}/UpdateProgressOkr")]
    public async Task<Response> UpdateProgressOkr(Guid okrId,[FromBody] int achieved)
    {
        return await okrRepository.UpdateProgressOkr(okrId, achieved);
    }

    [HttpPut("{okrId}/UpdateOwnerOkr")]
    public async Task<Response> UpdateOwnerOkr(Guid okrId, [FromBody] Guid owner)
    {
        return await okrRepository.UpdateOwnerOkr(okrId, owner);
    }


    [HttpPut("{okrId}/UpdateOkrRequest")]
    public async Task<Response> UpdateOkrRequest(Guid okrId, [FromForm] OKREditDTO okrEditDTO)
    {
        return await okrRepository.UpdateOkrRequest(okrId, okrEditDTO);
    }

    //[HttpGet("requests")]
    //public async Task<PaginatedList<OKRRequestDTO>> GetOkrsRequests(
    //int pageIndex = 1,
    //int pageSize = 10,
    //string? title = null,
    //string? type = null,
    //string? scope = null,
    //string? approveStatus = null,
    //string? cycle = null,
    //Guid? departmentId = null)
    //{
    //    return await okrRepository.GetOkrsRequests(pageIndex, pageSize, title, type, scope, approveStatus,cycle, departmentId);
    //}
    [HttpPut("{id}/approveStatus")]
    public async Task<IActionResult> UpdateApproveStatus(Guid id, [FromBody] ApproveStatusUpdateDTO dto)
    {
        var response = await okrRepository.UpdateApproveStatus(id, dto);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}

