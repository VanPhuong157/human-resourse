using BusinessObjects.DTO;
using BusinessObjects.DTO.Okr;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.AspNetCore.Mvc;

namespace Repository.Objectives
{
    public interface IOkrRepository
    {
        Task<Response> GetOkrsSlowProgress();
        Task<OKRDTO> GetOkrById(Guid id);
        Task<Response> CreateOkr(OKRCreateDTO okrCreateDTO);
        Task<Response> UpdateProgressOkr(Guid okrId, int achieved);
        Task<Response> UpdateOwnerOkr(Guid okrId, UpdatePeopleDTO dto);
        Task UpdateDepartmentOkr(Guid okrId, UpdateDepartmentsDTO dto);
        //Task<Response> UpdateOkrRequest(Guid okrId, OKREditDTO okrEditDTO);
        Task<Response> UpdateOkr(Guid okrId, OKRPartialUpdateDTO dto);
        Task<PaginatedList<OKRDTO>> GetOkrsByDepartmentId(
           int pageIndex = 1,
           int pageSize = 10,
           string? title = null,
           string? type = null,
           string? scope = null,
           string? status = null,
           string? cycle = null
          );
        //Task<Response> UpdateApproveStatus(Guid id, [FromBody] ApproveStatusUpdateDTO dto);
    //    Task<PaginatedList<OKRRequestDTO>> GetOkrsRequests(
    //int pageIndex = 1,
    //int pageSize = 10,
    //string? title = null,
    //string? type = null,
    //string? scope = null,
    //string? approveStatus = null,
    //string? cycle = null,
    //Guid? departmentId = null);
        IDictionary<string, int> GetOkrStatisticsByApproveStatus(Guid? departmentId);
        IDictionary<string, int> GetOkrStatisticsByStatus(Guid? departmentId);
    }
}
