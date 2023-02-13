using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationsDetails
{
    public class LeaveAllocationsDetailsDto
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
 
    }
}
