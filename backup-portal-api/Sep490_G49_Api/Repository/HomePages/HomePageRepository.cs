using BusinessObjects.DTO.HomePage;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.Departments;
using DataAccess.HomePages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.HomePages
{
    public class HomePageRepository : IHomePageRepository
    {
        private readonly HomePageDAO _homePageDAO;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomePageRepository(HomePageDAO homePageDAO, IWebHostEnvironment webHostEnvironment)
        {
            _homePageDAO = homePageDAO;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<PaginatedList<HomePageDTO>> GetAllHomePages(
   int pageIndex = 1,
   int pageSize = 10,
   DateTime? startTime = null,
   DateTime? endTime = null,
   int? version = null,
   string? status =null)
        {
            return await _homePageDAO.GetAllHomePages(pageIndex, pageSize, startTime, endTime, version,status);
        }

        public async Task<Response> CreateHomePage(HomePageCreateDTO homePageCreateDTO)
        {
            return await _homePageDAO.CreateHomePage(homePageCreateDTO);
        }

        public async Task<HomePageLastestDTO> GetLastestVersion()
        {
           
            return await _homePageDAO.GetLastestVersion();
        }
        public async Task<HomePageDTO> GetHomePageActive()
        {
            
            return await _homePageDAO.GetHomePageActive();
        }

        public async Task<Response> ActivateHomePage(int id)
        {
            return await _homePageDAO.ActivateHomePage(id);
        }
        public async Task<Response> UpdateStatus(int id, HomePageEditDTO homePageEditDTO)
        {
             return await _homePageDAO.UpdateStatus(id, homePageEditDTO);
        }

        public async Task<HomePage?> GetBackgroundImageByIdAsync(int id)
        {
            return await _homePageDAO.GetBackgroundImageByIdAsync(id);
        }
        public async Task<byte[]> GetBackgroundFileAsync(string background)
        {
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, background.TrimStart('/'));

            if (System.IO.File.Exists(filePath))
            {
                return await System.IO.File.ReadAllBytesAsync(filePath);
            }
            else
            {
                throw new FileNotFoundException("Background Không có trên hệ thống.");
            }
        }
    }
}
