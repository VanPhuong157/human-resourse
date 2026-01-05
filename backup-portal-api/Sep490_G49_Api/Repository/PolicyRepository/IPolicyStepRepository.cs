using BusinessObjects.DTO.Policy;
using BusinessObjects.Models;
using BusinessObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PolicyRepository
{
    public interface IPolicyStepRepository
    {
        Task<PaginatedList<PolicyStepRowDTO>> GetAllSteps(int pageIndex, int pageSize, string? q);
        Task<Response> GetStepDetail(Guid id);
        Task<Response> UpdateStep(Guid id, UpdatePolicyStepDTO dto);
    }
}
