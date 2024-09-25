using BusinessObjects.DTO.HomePageReasonDTO;
using BusinessObjects.Models;
using BusinessObjects.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.HomePageReasons
{
    public class HomePageReasonDAO
    {
        private readonly SEP490_G49Context _context;

        public HomePageReasonDAO(SEP490_G49Context context)
        {
            _context = context;
        }


        public async Task<Response> CreateReason(ReasonCreateDTO reasonCreateDTO)
        {
            var newReason = new HomePageReason
            {
                Title = reasonCreateDTO.Title,
                SubTitle = reasonCreateDTO.SubTitle,
                Color = reasonCreateDTO.Color,
                Content = reasonCreateDTO.Content
            };

            _context.HomePageReasons.Add(newReason);
            await _context.SaveChangesAsync();

            return new Response { Code = ResponseCode.Success, Message = "Create successfully!" };
        }

        public async Task<PaginatedList<ReasonDTO>> GetAllReasons(int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.HomePageReasons.AsQueryable();

            var count = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var reasons = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new ReasonDTO
                {
                    Id = r.Id,
                    Title = r.Title,
                    SubTitle = r.SubTitle,
                    Color = r.Color,
                    Content = r.Content
                })
                .ToListAsync();

            return new PaginatedList<ReasonDTO>(reasons, pageIndex, totalPages, count);
        }
        public async Task<Response> UpdateReason(int id, ReasonCreateDTO reasonCreateDTO)
        {
            var reason = await _context.HomePageReasons.FindAsync(id);
            if (reason == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "Reason not found." };
            }

            reason.Title = reasonCreateDTO.Title;
            reason.SubTitle = reasonCreateDTO.SubTitle;
            reason.Color = reasonCreateDTO.Color;
            reason.Content = reasonCreateDTO.Content;

            _context.HomePageReasons.Update(reason);
            await _context.SaveChangesAsync();
            return new Response { Code = ResponseCode.Success, Message = "Reason updated successfully!" };
        }

        public async Task<Response> DeleteReason(int id)
        {
            var reason = await _context.HomePageReasons.FindAsync(id);
            if (reason == null)
            {
                return new Response { Code = ResponseCode.NotFound, Message = "Reason not found." };
            }

            _context.HomePageReasons.Remove(reason);
            await _context.SaveChangesAsync();
            return new Response { Code = ResponseCode.Success, Message = "Reason deleted successfully!" };
        }
    }
}
