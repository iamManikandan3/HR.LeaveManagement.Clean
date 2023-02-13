using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._leaveTypeRepository = leaveTypeRepository;
            this._mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationCommandValidator(_leaveTypeRepository, _leaveAllocationRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid leave allocation request", validationResult);

            var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);
            if (leaveAllocation is null)
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);

            _mapper.Map(request, leaveAllocation); // right way to do when update. Bcz it won't affect if the properties changes. Ref: video-42(18:00)
            await _leaveAllocationRepository.UpdateAsync(leaveAllocation);
            return Unit.Value;
        }
    }
}
