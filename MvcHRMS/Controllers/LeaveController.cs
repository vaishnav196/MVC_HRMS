using Microsoft.AspNetCore.Mvc;
using MvcHRMS.Services;
using MvcHRMS.Models;

namespace MvcHRMS.Controllers
{
    public class LeaveController : Controller
    {
        private readonly ILeaveService _leaveService;

        public LeaveController(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        public IActionResult ApplyLeave()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyLeave(ApplyLeaveViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var leaveRequest = new LeaveRequest
                    {
                        EmpID = model.EmpID,
                        Name = model.Name,
                        Email = model.Email,
                        FromDate = model.FromDate,
                        ToDate = model.ToDate,
                        Reason = model.Reason
                    };

                    await _leaveService.ApplyLeaveAsync(leaveRequest);

                    TempData["SuccessMessage"] = "Leave applied successfully. Waiting for HR approval.";

                    return RedirectToAction("Index", "Home");
                }
                catch (ApplicationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to apply leave. Please try again later.");
                    // Log the exception
                }
            }

            return View(model);
        }

        public async Task<IActionResult> PendingRequests()
        {
            var pendingRequests = await _leaveService.GetPendingLeaveRequestsAsync();
            return View(pendingRequests);
        }

        public async Task<IActionResult> ApproveLeave(int id)
        {
            await _leaveService.ApproveLeaveRequestAsync(id);
            return RedirectToAction(nameof(PendingRequests));
        }

        public async Task<IActionResult> RejectLeave(int id)
        {
            await _leaveService.RejectLeaveRequestAsync(id);
            return RedirectToAction(nameof(PendingRequests));
        }
    }
}

