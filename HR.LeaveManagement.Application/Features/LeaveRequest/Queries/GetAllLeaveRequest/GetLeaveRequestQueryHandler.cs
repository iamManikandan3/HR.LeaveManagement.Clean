using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequest
{
    public class GetLeaveRequestQueryHandler : IRequestHandler<GetLeaveRequestQuery, List<LeaveRequestDto>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public GetLeaveRequestQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            this._leaveRequestRepository = leaveRequestRepository;
            this._mapper = mapper;
        }
        public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestQuery request, CancellationToken cancellationToken)
        {
            // Check if it is logged in employee


            var leaveRequests = await _leaveRequestRepository.GetAsync();
            var response = _mapper.Map<List<LeaveRequestDto>>(leaveRequests);

            // Fill requests with employee information

            return response;
        }
    }
}
