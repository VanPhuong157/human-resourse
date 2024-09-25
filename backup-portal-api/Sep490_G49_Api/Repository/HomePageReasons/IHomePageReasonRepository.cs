using BusinessObjects.DTO.HomePageReasonDTO;
using BusinessObjects.Models;
using BusinessObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.HomePageReasons
{
    public interface IHomePageReasonRepository
    {
        Task<Response> CreateReason(ReasonCreateDTO reasonCreateDTO);
        Task<PaginatedList<ReasonDTO>> GetAllReasons(int pageIndex = 1, int pageSize = 10);
        Task<Response> UpdateReason(int id, ReasonCreateDTO reasonCreateDTO);
        Task<Response> DeleteReason(int id);
    }
}
