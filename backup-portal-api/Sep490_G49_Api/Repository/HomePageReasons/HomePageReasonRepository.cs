using BusinessObjects.DTO.HomePageReasonDTO;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.HomePageReasons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.HomePageReasons
{
    public class HomePageReasonRepository : IHomePageReasonRepository
    {
        private readonly HomePageReasonDAO _homePageReasonDAO;

        public HomePageReasonRepository(HomePageReasonDAO homePageReasonDAO)
        {
            _homePageReasonDAO = homePageReasonDAO;
        }

        public async Task<Response> CreateReason(ReasonCreateDTO reasonCreateDTO)
        {
            return await _homePageReasonDAO.CreateReason(reasonCreateDTO);
        }

        public async Task<PaginatedList<ReasonDTO>> GetAllReasons(int pageIndex = 1, int pageSize = 10)
        {
            return await _homePageReasonDAO.GetAllReasons(pageIndex, pageSize);
        }

        public async Task<Response> UpdateReason(int id, ReasonCreateDTO reasonCreateDTO)
        {
            return await _homePageReasonDAO.UpdateReason(id, reasonCreateDTO);
        }

        public async Task<Response> DeleteReason(int id)
        {
            return await _homePageReasonDAO.DeleteReason(id);
        }
        }
}
