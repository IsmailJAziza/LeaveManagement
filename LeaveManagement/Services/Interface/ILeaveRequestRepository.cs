using LeaveManagement.Models.LeaveRequest;

namespace LeaveManagement.Services.Interface
{
    public interface ILeaveRequestRepository
    {
        Task CreateLeaveRequest(LeaveRequestCreateVM leaveRequest);
        Task<List<LeaveRequestReadOnlyVM> > GetEmployeeLeaveRequest(); 
        Task CancelLeaveRequest(int LeaveRequestId);
        Task ReviewLeaveRequest(int leaveRequestId, bool approved);
        Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequest();
        Task<bool> RequstedDaysExceedAllocation(LeaveRequestCreateVM model);
        Task<LeaveRequestReviewVM> GetLeaveRequestForReview(int id);
    }
}