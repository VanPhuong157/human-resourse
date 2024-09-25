using BusinessObjects.DTO.HomePage;
using BusinessObjects.Models;
using BusinessObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.HomePages
{
    public interface IHomePageRepository
    {
        Task<Response> CreateHomePage(HomePageCreateDTO homePageCreateDTO);
        Task<HomePageLastestDTO> GetLastestVersion();
        Task<HomePageDTO> GetHomePageActive();
        Task<Response> ActivateHomePage(int id);
        Task<Response> UpdateStatus(int id, HomePageEditDTO homePageEditDTO);
        Task<PaginatedList<HomePageDTO>> GetAllHomePages(
            int pageIndex = 1,
            int pageSize = 10,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? version = null, string? status = null);
        Task<HomePage?> GetBackgroundImageByIdAsync(int id);
        Task<byte[]> GetBackgroundFileAsync(string background);
    }
    
}
