using MvcHRMS.Models;

namespace MvcHRMS.Services
{
    public interface ILeaveService
    {
        Task ApplyLeaveAsync(LeaveRequest leaveRequest);
        Task<List<LeaveRequest>> GetPendingLeaveRequestsAsync();
        Task ApproveLeaveRequestAsync(int leaveRequestId);
        Task RejectLeaveRequestAsync(int leaveRequestId);
    }
}
