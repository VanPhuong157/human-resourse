using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.DTO.Statistic;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Objectives;
using Repository.Users;

namespace Repository.Statistic
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly SEP490_G49Context _ctx;
        private readonly IOkrRepository _okrDAO;
        private readonly IUserRepository _userDAO;

        public StatisticRepository(
            SEP490_G49Context ctx,
            IOkrRepository okrDAO,
            IUserRepository userDAO)
        {
            _ctx = ctx;
            _okrDAO = okrDAO;
            _userDAO = userDAO;
        }

        public IDictionary<string, int> GetOkrStatisticsByApproveStatus(Guid? departmentId)
        {
            return _okrDAO.GetOkrStatisticsByApproveStatus(departmentId);
        }

        public IDictionary<string, int> GetOkrStatisticsByStatus(Guid? departmentId)
        {
            return _okrDAO.GetOkrStatisticsByStatus(departmentId);
        }

        public async Task<TotalStatisticDTO> UsersStatistic(Guid? departmentId)
        {
            var total = _userDAO.GetTotalUserCount(departmentId);
            var percent = _userDAO.CalculateUserGrowthPercentage(departmentId);

            return new TotalStatisticDTO
            {
                Total = total,
                Percentage = percent
            };
        }

        // WorkItem nội bộ – dùng chung cho OKR / PolicyStep
        private class WorkItem
        {
            public Guid TaskId { get; set; }          // <== ID của OKR / PolicyStep
            public Guid UserId { get; set; }
            public string UserName { get; set; } = string.Empty;
            public Guid DepartmentId { get; set; }

            public string Source { get; set; } = string.Empty;
            public string Priority { get; set; } = "Normal";
            public string Status { get; set; } = string.Empty;

            public DateTime CreatedAt { get; set; }
            public DateTime? DueDate { get; set; }
            public DateTime? CompletedAt { get; set; }
        }

        // ================== OKR DASHBOARD ==================

        // Trạng thái OKR: done, Doing, To Do, Archived, postpone
        public async Task<WorkDashboardDTO> GetOkrWorkDashboardAsync(
            Guid? departmentId,
            Guid? userId,
            DateTime? from,
            DateTime? to)
        {
            var now = DateTime.UtcNow;

            var okrQuery =
                from o in _ctx.OKRs
                join ou in _ctx.OkrUsers on o.Id equals ou.OkrId
                join u in _ctx.Users on ou.UserId equals u.Id
                join ui in _ctx.UserInformations on u.Id equals ui.UserId
                select new WorkItem
                {
                    TaskId = o.Id,
                    UserId = u.Id,
                    UserName = ui.FullName ?? u.Id.ToString(),
                    DepartmentId = u.DepartmentId,
                    Source = "OKR",
                    Priority = o.Priority ?? "Normal",
                    Status = string.IsNullOrWhiteSpace(o.Status) ? "Unknown" : o.Status,
                    CreatedAt = o.LastUpdated ?? o.DueDate ?? now,
                    DueDate = o.DueDate,
                    CompletedAt = o.LastUpdated
                };

            IQueryable<WorkItem> query = okrQuery;

            if (departmentId.HasValue)
                query = query.Where(x => x.DepartmentId == departmentId.Value);

            if (userId.HasValue)
                query = query.Where(x => x.UserId == userId.Value);

            if (from.HasValue)
                query = query.Where(x => x.CreatedAt >= from.Value);

            if (to.HasValue)
                query = query.Where(x => x.CreatedAt <= to.Value);

            var workItems = await query.ToListAsync();

            // ======= Tập task duy nhất (không nhân theo số user) =======
            var distinctTasks = workItems
                .GroupBy(x => x.TaskId)
                .Select(g => g.First())
                .ToList();

            var totalTasks = distinctTasks.Count;

            var byPriority = distinctTasks
                .GroupBy(x => x.Priority)
                .ToDictionary(g => g.Key, g => g.Count());

            var byStatus = distinctTasks
                .GroupBy(x => x.Status)
                .ToDictionary(g => g.Key, g => g.Count());

            // ======= Theo từng user – vẫn tính theo từng assignment =======
            var byUser = workItems
                .GroupBy(x => new { x.UserId, x.UserName, x.DepartmentId })
                .Select(g =>
                {
                    var total = g.Count();

                    int completed = g.Count(i =>
                    {
                        var st = (i.Status ?? string.Empty).Trim().ToLower();
                        // done + archived = hoàn thành
                        return st == "done" || st == "archived";
                    });

                    int overdue = g.Count(i =>
                        i.DueDate.HasValue &&
                        i.CompletedAt == null &&
                        i.DueDate.Value < now);

                    int onTimeCompleted = g.Count(i =>
                        i.DueDate.HasValue &&
                        i.CompletedAt.HasValue &&
                        i.CompletedAt.Value <= i.DueDate.Value);

                    double onTimeRate = completed == 0
                        ? 0
                        : (double)onTimeCompleted / completed;

                    double completionRate = total == 0
                        ? 0
                        : (double)completed / total;

                    double overdueRatio = total == 0
                        ? 0
                        : (double)overdue / total;

                    double finalScore =
                        completionRate * 0.4 +
                        onTimeRate * 0.3 +
                        (1 - overdueRatio) * 0.3;

                    return new UserTaskStatisticItemDTO
                    {
                        UserId = g.Key.UserId,
                        FullName = g.Key.UserName,
                        TotalTasks = total,
                        CompletedTasks = completed,
                        OverdueTasks = overdue,
                        OnTimeRate = onTimeRate,
                        FinalScore = finalScore
                    };
                })
                .OrderByDescending(x => x.FinalScore)
                .ToList();

            var byDate = distinctTasks
                .GroupBy(x => x.CreatedAt.Date)
                .Select(g => new DateTaskStatisticDTO
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToList();

            return new WorkDashboardDTO
            {
                TotalTasks = totalTasks,
                ByPriority = byPriority,
                ByStatus = byStatus,
                ByUser = byUser,
                ByDate = byDate
            };
        }

        // ================== POLICY STEP DASHBOARD ==================

        // PolicyStep: trạng thái suy từ ExecDate / ReviewDate / ApproveDate
        // ================== POLICY STEP DASHBOARD ==================

        // PolicyStep: trạng thái suy từ ExecDate / ReviewDate / ApproveDate
        public async Task<WorkDashboardDTO> GetPolicyStepWorkDashboardAsync(
    Guid? departmentId,
    Guid? userId,
    DateTime? from,
    DateTime? to)
        {
            var now = DateTime.UtcNow;

            var stepQuery =
                from psu in _ctx.PolicyStepUsers
                join ps in _ctx.PolicySteps on psu.PolicyStepId equals ps.Id
                join u in _ctx.Users on psu.UserId equals u.Id
                join ui in _ctx.UserInformations on u.Id equals ui.UserId into uiJoin
                from ui in uiJoin.DefaultIfEmpty()
                select new WorkItem
                {
                    TaskId = ps.Id,
                    UserId = u.Id,
                    UserName = ui.FullName ?? u.Username ?? u.Id.ToString(),
                    DepartmentId = u.DepartmentId,
                    Source = "POLICY",
                    Priority = "Normal",
                    Status = ps.ApproveDate != null
                        ? "Approved"
                        : (ps.ReviewDate != null
                            ? "Reviewing"
                            : (ps.ExecDate != null ? "Executing" : "NotStarted")),
                    CreatedAt = ps.ExecDate ?? ps.ReviewDate ?? ps.ApproveDate ?? now,
                    DueDate = ps.ReviewDate,
                    CompletedAt = ps.ApproveDate
                };

            IQueryable<WorkItem> query = stepQuery;

            if (departmentId.HasValue)
                query = query.Where(x => x.DepartmentId == departmentId.Value);

            if (userId.HasValue)
                query = query.Where(x => x.UserId == userId.Value);

            if (from.HasValue)
                query = query.Where(x => x.CreatedAt >= from.Value);

            if (to.HasValue)
                query = query.Where(x => x.CreatedAt <= to.Value);

            var workItems = await query.ToListAsync();

            // task duy nhất theo PolicyStep
            var distinctTasks = workItems
                .GroupBy(x => x.TaskId)
                .Select(g => g.First())
                .ToList();

            var totalTasks = distinctTasks.Count;

            // ====== TÌNH TRẠNG HẠN (thay cho Priority) ======
            // Quá hạn:      DueDate < hôm nay && chưa Completed
            // Sắp đến hạn:  DueDate trong 7 ngày tới && chưa Completed
            // Bình thường:  còn lại
            var byDeadline = distinctTasks
                .GroupBy(t =>
                {
                    if (t.DueDate.HasValue && t.CompletedAt == null && t.DueDate.Value.Date < now)
                        return "Quá hạn";

                    if (t.DueDate.HasValue && t.CompletedAt == null &&
                        t.DueDate.Value.Date >= now &&
                        t.DueDate.Value.Date <= now.AddDays(7))
                        return "Sắp đến hạn";

                    return "Bình thường";
                })
                .ToDictionary(g => g.Key, g => g.Count());

            // Trạng thái công việc (NotStarted / Executing / Reviewing / Approved)
            var byStatus = distinctTasks
                .GroupBy(x => x.Status)
                .ToDictionary(g => g.Key, g => g.Count());

            // Theo user vẫn tính trên assignment
            var byUser = workItems
                .GroupBy(x => new { x.UserId, x.UserName, x.DepartmentId })
                .Select(g =>
                {
                    var total = g.Count();

                    int completed = g.Count(i =>
                    {
                        var st = (i.Status ?? string.Empty).Trim().ToLower();
                        return st == "approved";
                    });

                    int overdue = g.Count(i =>
                        i.DueDate.HasValue &&
                        i.CompletedAt == null &&
                        i.DueDate.Value.Date < now);

                    int onTimeCompleted = g.Count(i =>
                        i.DueDate.HasValue &&
                        i.CompletedAt.HasValue &&
                        i.CompletedAt.Value.Date <= i.DueDate.Value.Date);

                    double onTimeRate = completed == 0
                        ? 0
                        : (double)onTimeCompleted / completed;

                    double completionRate = total == 0
                        ? 0
                        : (double)completed / total;

                    double overdueRatio = total == 0
                        ? 0
                        : (double)overdue / total;

                    double finalScore =
                        completionRate * 0.4 +
                        onTimeRate * 0.3 +
                        (1 - overdueRatio) * 0.3;

                    return new UserTaskStatisticItemDTO
                    {

                        UserId = g.Key.UserId,
                        FullName = g.Key.UserName,
                        TotalTasks = total,
                        CompletedTasks = completed,
                        OverdueTasks = overdue,
                        OnTimeRate = onTimeRate,
                        FinalScore = finalScore
                    };
                })
                .OrderByDescending(x => x.FinalScore)
                .ToList();

            var byDate = distinctTasks
                .GroupBy(x => x.CreatedAt.Date)
                .Select(g => new DateTaskStatisticDTO
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToList();

            return new WorkDashboardDTO
            {
                TotalTasks = totalTasks,
                // LƯU Ý: ByPriority giờ đang là "Quá hạn / Sắp đến hạn / Bình thường"
                ByPriority = byDeadline,
                ByStatus = byStatus,
                ByUser = byUser,
                ByDate = byDate
            };
        }

    }
}
