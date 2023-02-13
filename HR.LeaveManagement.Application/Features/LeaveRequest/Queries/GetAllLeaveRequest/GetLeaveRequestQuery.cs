using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequest
{
    public record GetLeaveRequestQuery : IRequest<List<LeaveRequestDto>>;
}
