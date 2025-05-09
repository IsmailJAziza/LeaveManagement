using AutoMapper;
using LeaveManagement.Data.DataModel;
using LeaveManagement.Models.LeaveAllocations;
using LeaveManagement.Models.LeaveRequest;
using LeaveManagement.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Services
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IPeriodRepository _periodRepository;

        public LeaveRequestRepository(IMapper mapper, IUserRepository userRepository, ApplicationDbContext context, ILeaveAllocationRepository leaveAllocationRepository, IPeriodRepository periodRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _context = context;
            _leaveAllocationRepository = leaveAllocationRepository;
            _periodRepository = periodRepository;
        }

        public async Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequest()
        {
            //get all leave Requests
            var leaveRequestList = await _context.LeaveRequests
                .Include(q => q.LeaveType)
                .ToListAsync();

            var LeaveRequestReadOnlyVM = leaveRequestList.Select(p => new LeaveRequestReadOnlyVM
            {
                Id = p.Id,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                NumberOfDays = p.EndDate.DayNumber - p.StartDate.DayNumber,
                LeaveType = p.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)p.LeaveRequestStatusId
            }).ToList();
            //start do the calculations (model)
            var model = new EmployeeLeaveRequestListVM
            {
                TotalRequests = leaveRequestList.Count,
                ApprovedRequests = leaveRequestList.Count(p => p.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Approved),
                DeclinedRequests = leaveRequestList.Count(p => p.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Declined),
                PendingRequests = leaveRequestList.Count(p => p.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Pending),
                leaveRequests = LeaveRequestReadOnlyVM
            };
            //
            return model;
        }

        public async Task CancelLeaveRequest(int LeaveRequestId)
        {
            //get leave reqest
            var leaveRequest = await _context.LeaveRequests.FirstOrDefaultAsync(p => p.Id == LeaveRequestId);
            //update leave request status
            leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Cancelled;

            //restor dedacted days
            await UpdateAllocationDays(leaveRequest, false); 
            //save changes
            await _context.SaveChangesAsync();
        }

        public async Task CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            var leaveRequest = _mapper.Map<LeaveRequest>(model);
            //get the user
            var user = await _userRepository.GetLoogedInUserAsync();
            leaveRequest.EmployeeID = user.Id;
            //set the request status
            leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Pending;
            //save the request
            _context.LeaveRequests.Add(leaveRequest);
            //leave Allocation Deducttion 
            await UpdateAllocationDays(leaveRequest, true);

            await _context.SaveChangesAsync();

        }

        public async Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequest()
        {
            var user = await _userRepository.GetLoogedInUserAsync();
            var leaveRequestList = await _context.LeaveRequests
                .Include(q => q.LeaveType)
                .Where(p => p.EmployeeID == user.Id)
                .ToListAsync();
            var model = leaveRequestList.Select(p => new LeaveRequestReadOnlyVM
            {
                Id = p.Id,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                NumberOfDays = p.EndDate.DayNumber - p.StartDate.DayNumber,
                LeaveType = p.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)p.LeaveRequestStatusId
            }).ToList();

            return model;
        }

        public async Task<LeaveRequestReviewVM> GetLeaveRequestForReview(int id)
        {
            //get leave Request include the leavetype
            var leaveRequest = await _context.LeaveRequests
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(p => p.Id == id);
            //get user
            var user = await _userRepository.GetUserById(leaveRequest.EmployeeID);
            //maping

            var model = new LeaveRequestReviewVM
            {
                Id = leaveRequest.Id,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                LeaveRequestStatus = (LeaveRequestStatusEnum)leaveRequest.LeaveRequestStatusId,
                NumberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber,
                LeaveType = leaveRequest.LeaveType.Name,
                RequestComments = leaveRequest.RequestComment,
                Employee = new EmployeeListVM
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,

                },
            };
            return model;
        }

        public async Task<bool> RequstedDaysExceedAllocation(LeaveRequestCreateVM model)
        {
            var user = await _userRepository.GetLoogedInUserAsync();
            var currentDate = DateTime.Now;
            var period = await _context.Periods.FirstOrDefaultAsync(q => q.EndDate.Year == currentDate.Year);
            var leaveAllocation = await _context.LeaveAllocations.FirstOrDefaultAsync(p => p.LeaveTypeId == model.LeaveTypeId && p.EmployeeId == user.Id && p.Period.Id == period.Id);
            var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
            return leaveAllocation.Days < numberOfDays;

        }

        public async Task ReviewLeaveRequest(int leaveRequestId, bool approved)
        {
            //get leaverequest id 

            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
            //get user
            var user = await _userRepository.GetLoogedInUserAsync();
            leaveRequest.ReviewerId = user.Id;
            //retreive the date if declined 
            if (approved)
            {
                leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Approved;
            }
            else
            {
                leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Declined;
                await UpdateAllocationDays(leaveRequest, false);

            }
            await _context.SaveChangesAsync();
        }

        private async Task UpdateAllocationDays(LeaveRequest leaveRequest, bool deductDays)
        {
            var allocation = await _leaveAllocationRepository.GetCurreAllocationAsync(leaveRequest.EmployeeID, leaveRequest.LeaveTypeId);
            var numberOfDays = CalculateDays(leaveRequest.StartDate, leaveRequest.EndDate);
            if (deductDays)
            {
                allocation.Days -= numberOfDays;
            }
            else
            {
                allocation.Days += numberOfDays;
            }
            _context.Entry(allocation).State = EntityState.Modified;
        }
        private int CalculateDays(DateOnly start, DateOnly end)
        {
            return end.DayNumber - start.DayNumber;
        }
    }
}
