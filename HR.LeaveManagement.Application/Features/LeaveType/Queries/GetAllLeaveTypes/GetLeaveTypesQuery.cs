using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    // Here we using records because records are immutable(not change)
    public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;
}
