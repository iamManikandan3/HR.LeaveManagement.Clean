using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations
{
    public class GetLeaveAllocationsQueryHandler : IRequestHandler<GetLeaveAllocationsQuery, List<LeaveAllocationDto>>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public GetLeaveAllocationsQueryHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._mapper = mapper;
        }
        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationsQuery request, CancellationToken cancellationToken)
        {
            // To Add later
            // - Get records for specific user
            // - Get allocations per employee

            var leaveAllocations = await _leaveAllocationRepository.GetAsync();
            var response = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
            return response;
        }
    }
}
