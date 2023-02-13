using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Domain;
using Moq;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    public class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
        {
            // Setup model
            var leaveTypes = new List<LeaveType>
            {
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Test Vaccation"
                },
                new LeaveType
                {
                    Id = 2,
                    DefaultDays = 15,
                    Name = "Test Sick"
                },
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 15,
                    Name = "Test Maternity"
                }
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();
            
            // Setup functionality
            mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveTypes);
            mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
                .Returns((LeaveType leaveType) =>
                {
                    leaveTypes.Add(leaveType);
                    return Task.CompletedTask;
                });

            
            return mockRepo;
        }
    }
}
