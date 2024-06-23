using MvcHRMS.Models;
using MvcHRMS.Repository;

namespace MvcHRMS.Services
{
    public class LeaveService:ILeaveService
    {
        private readonly ILeaveRepository _leaveRepository;

        public LeaveService(ILeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }

        public async Task ApplyLeaveAsync(LeaveRequest leaveRequest)
        {
            // Calculate TotalDays and AbsentDays based on business rules
            leaveRequest.TotalDays = (int)(leaveRequest.ToDate - leaveRequest.FromDate).TotalDays + 1;
            leaveRequest.AbsentDays = leaveRequest.TotalDays > 2 ? leaveRequest.TotalDays - 2 : 0;

            // Apply leave only if TotalDays <= 2 per month
            if (leaveRequest.AbsentDays <= 2)
            {
                await _leaveRepository.ApplyLeaveAsync(leaveRequest);
            }
            else
            {
                throw new ApplicationException("Cannot apply more than 2 leaves per month.");
            }
        }

        public async Task<List<LeaveRequest>> GetPendingLeaveRequestsAsync()
        {
            return await _leaveRepository.GetPendingLeaveRequestsAsync();
        }

        public async Task ApproveLeaveRequestAsync(int leaveRequestId)
        {
            var leaveRequest = await _leaveRepository.GetLeaveRequestByIdAsync(leaveRequestId);
            leaveRequest.Status = "Approved";
            await _leaveRepository.UpdateLeaveRequestAsync(leaveRequest);
        }

        public async Task RejectLeaveRequestAsync(int leaveRequestId)
        {
            var leaveRequest = await _leaveRepository.GetLeaveRequestByIdAsync(leaveRequestId);
            leaveRequest.Status = "Rejected";
            await _leaveRepository.UpdateLeaveRequestAsync(leaveRequest);
        }
    }
}
