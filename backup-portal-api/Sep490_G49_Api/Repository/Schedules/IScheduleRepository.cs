using BusinessObjects.DTO.Schedule;
using BusinessObjects.Models;
using BusinessObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Schedules
{
    public interface IScheduleRepository
    {
        Task<PaginatedList<ScheduleDTO>> GetAll(int pageIndex = 1, int pageSize = 10);
        Task<Response> Create(CreateScheduleDTO createSchedule);
        Task<Response> Approve(Guid id, ApproveScheduleDTO approve);
        Task<Response> DownloadFile(string fileName);
    }
}
