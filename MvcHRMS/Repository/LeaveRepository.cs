using Microsoft.EntityFrameworkCore;
using MvcHRMS.Data;
using MvcHRMS.Models;

namespace MvcHRMS.Repository
{
    public class LeaveRepository:ILeaveRepository
    {       
        private readonly ApplicationDbContext _context;
        public LeaveRepository(ApplicationDbContext db) 
        {
            this._context = db;
        
        }
       

        public async Task<LeaveRequest> ApplyLeaveAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Add(leaveRequest);
            await _context.SaveChangesAsync();
            return leaveRequest;
        }

        public async Task<List<LeaveRequest>> GetPendingLeaveRequestsAsync()
        {
            return await _context.LeaveRequests
                .Where(l => l.Status == "Pending")
                .ToListAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestByIdAsync(int id)
        {
            return await _context.LeaveRequests.FindAsync(id);
        }

        public async Task UpdateLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            _context.Entry(leaveRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
