using BusinessObjects.DTO;
using BusinessObjects.DTO.Okr;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.Okrs;

namespace Repository.Objectives
{
    public class OkrRepository : IOkrRepository
    {
        private readonly OkrDAO okrDAO;
        public OkrRepository(OkrDAO okrDAO)
        {
            this.okrDAO = okrDAO;
        }

        public Task<Response> GetOkrsSlowProgress() {
            return okrDAO.GetOkrsSlowProgress();
        }
        public async Task<OKRDetailsDTO> GetOkrById(Guid id)
        {
            return await okrDAO.GetOkrById(id);

        }
        public async Task<Response> UpdateProgressOkr(Guid okrId, int achieved)
        {
            return await okrDAO.UpdateProgressOkr(okrId, achieved);
        }

        public async Task<Response> UpdateOwnerOkr(Guid okrId, Guid Owner)
        {
            return await okrDAO.UpdateOwnerOkr(okrId, Owner);
        }
        public async Task<Response> UpdateOkrRequest(Guid okrId, OKREditDTO okrEditDTO)
        {
            return await okrDAO.UpdateOkrRequest(okrId, okrEditDTO);
        }

            public async Task<Response> CreateOkr(OKRCreateDTO okrCreateDTO)
        {
            return await okrDAO.CreateOkr(okrCreateDTO);
        }
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
            return await okrDAO.GetAllOkrs(pageIndex, pageSize, title, type, scope, status,cycle, departmentId);
        }

        public async Task<Response> UpdateApproveStatus(Guid OkrId, ApproveStatusUpdateDTO dto)
        {
            return await okrDAO.UpdateApproveStatus(OkrId, dto);
        }

        public async Task<PaginatedList<OKRRequestDTO>> GetOkrsRequests(
            int pageIndex = 1,
            int pageSize = 10,
            string? title = null,
            string? type = null,
            string? scope = null,
            string? approveStatus = null,
            string? cycle = null,
            Guid? departmentId = null)
        {
            return await okrDAO.GetOkrsRequests(pageIndex, pageSize, title, type, scope, approveStatus, cycle, departmentId);
        }
    }
}