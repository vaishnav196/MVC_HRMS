using MvcHRMS.Models;

namespace MvcHRMS.Repository
{
    public interface ILeaveRepository
    {
        Task<LeaveRequest> ApplyLeaveAsync(LeaveRequest leaveRequest);
        Task<List<LeaveRequest>> GetPendingLeaveRequestsAsync();
        Task<LeaveRequest> GetLeaveRequestByIdAsync(int id);
        Task UpdateLeaveRequestAsync(LeaveRequest leaveRequest);
    }
}
