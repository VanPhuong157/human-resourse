using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.DTO.HomePage;
using BusinessObjects.Models;
using BusinessObjects.Response;
using DataAccess.Okrs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.HomePages
{
    public class HomePageDAO
    {
        private readonly SEP490_G49Context _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;


        public HomePageDAO(SEP490_G49Context context, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        public async Task<PaginatedList<HomePageDTO>> GetAllHomePages(
    int pageIndex = 1,
    int pageSize = 10,
    DateTime? startTime = null,
    DateTime? endTime = null,
    int? version = null,
    string? status = null)
        {
            var query = _context.HomePages.AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(j => j.Status == status);
            }
            if (version.HasValue)
            {
                query = query.Where(j => j.Id == version);
            }
            if (startTime.HasValue)
            {
                query = query.Where(j => j.CreateAt >= startTime.Value);
            }

            if (endTime.HasValue)
            {
                query = query.Where(j => j.CreateAt <= endTime.Value);
            }

            var okrs = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var homePages = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new HomePageDTO
                {
                    Id = o.Id,
                    TitleBody = o.TitleBody,
                    CreateAt = o.CreateAt,
                    Title = o.Title,
                    Address = o.Address,
                    Email = o.Email,
                    PhoneNumber = o.PhoneNumber,
                    ImageBackgroundDetail = o.ImageBackgroundDetail,
                    Status = o.Status,
                    StatusJobPost = o.StatusJobPost
                })
                .ToListAsync(); ;

            return new PaginatedList<HomePageDTO>(homePages, pageIndex, totalPages, count);
        }

        public async Task<HomePageLastestDTO> GetLastestVersion()
        {
            var latestHomePage = await _context.HomePages.OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            return _mapper.Map<HomePageLastestDTO>(latestHomePage);
        }
        public async Task<HomePageDTO> GetHomePageActive()
        {
            var latestHomePage = await _context.HomePages.Where(h => h.Status == "Active").FirstOrDefaultAsync();
            return _mapper.Map<HomePageDTO>(latestHomePage);
        }
        public async Task<Response> CreateHomePage(HomePageCreateDTO homePageCreateDTO)
        {
            string? uniqueFileName = null;
            if (homePageCreateDTO.ImageBackground != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg" };
                var fileExtension = Path.GetExtension(homePageCreateDTO.ImageBackground.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "Only JPG files are allowed." };
                }

                var allowedMimeTypes = new[] { "image/jpeg", "image/jpg" };
                if (!allowedMimeTypes.Contains(homePageCreateDTO.ImageBackground.ContentType))
                {
                    return new Response { Code = ResponseCode.BadRequest, Message = "Only JPG files are allowed." };
                }

                var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", "HomePages", homePageCreateDTO.TitleBody);
                Directory.CreateDirectory(uploadsFolder);

                uniqueFileName = homePageCreateDTO.ImageBackground.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await homePageCreateDTO.ImageBackground.CopyToAsync(fileStream);
                }
            }
            var newHomePage = new HomePage
            {
                TitleBody = homePageCreateDTO.TitleBody,
                Address = homePageCreateDTO.Address,
                Title = homePageCreateDTO.Title,
                PhoneNumber = homePageCreateDTO.PhoneNumber,
                Email = homePageCreateDTO.Email,
                StatusJobPost = homePageCreateDTO.StatusJobPost,
                Status = "Deactive",
                CreateAt = DateTime.UtcNow.AddHours(7),
                ImageBackgroundPath = uniqueFileName != null ? $"/Uploads/HomePages/{homePageCreateDTO.TitleBody}/{uniqueFileName}" : null,
                ImageBackgroundDetail = homePageCreateDTO.ImageBackground?.FileName
            };
            _context.Add(newHomePage);
            await _context.SaveChangesAsync();
            return new Response { Code = ResponseCode.Success, Message = "Create successfully!" };
        }

        public async Task<Response> ActivateHomePage(int id)
        {
            // Start a transaction to ensure atomicity of the operation
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Deactivate all currently active homepages
                    var activeHomePages = await _context.HomePages.Where(h => h.Status == "Active").ToListAsync();
                    foreach (var homePage in activeHomePages)
                    {
                        homePage.Status = "Deactive";
                    }

                    // Activate the selected homepage
                    var selectedHomePage = await _context.HomePages.FindAsync(id);
                    if (selectedHomePage == null)
                    {
                        return new Response { Code = ResponseCode.NotFound, Message = "HomePage not found." };
                    }

                    selectedHomePage.Status = "Active";

                    // Save changes
                    await _context.SaveChangesAsync();

                    // Commit the transaction
                    await transaction.CommitAsync();

                    return new Response { Code = ResponseCode.Success, Message = "HomePage activated successfully!" };
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of an error
                    await transaction.RollbackAsync();
                    return new Response { Code = ResponseCode.BadRequest, Message = $"An error occurred: {ex.Message}" };
                }
            }
        }

        public async Task<Response> UpdateStatus(int id, HomePageEditDTO homePageEditDTO)
        {
            var homePage = await _context.HomePages.FindAsync(id);
            if (homePage == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "HomePage not found." };
            }

            homePage.Status = homePageEditDTO.Status;
            homePage.StatusJobPost = homePageEditDTO.StatusJobPost;

            try
            {
                _context.HomePages.Update(homePage);
                await _context.SaveChangesAsync();
                return new Response { Code = ResponseCode.Success, Message = "HomePage status updated successfully!" };
            }
            catch (Exception ex)
            {
                return new Response { Code = ResponseCode.BadRequest, Message = $"An error occurred: {ex.Message}" };
            }
        }
        public async Task<HomePage?> GetBackgroundImageByIdAsync(int id)
        {
            return await _context.HomePages
                .FirstOrDefaultAsync(hp => hp.Id == id);
        }
    }
}
