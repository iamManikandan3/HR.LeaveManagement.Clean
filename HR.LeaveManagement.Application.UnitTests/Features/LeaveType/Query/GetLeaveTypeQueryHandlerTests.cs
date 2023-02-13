using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveType.Query
{
    public class GetLeaveTypeQueryHandlerTests
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private IMapper _mapper;
        private Mock<IAppLogger<GetLeaveTypesQueryHandler>> _appLogger;

        public GetLeaveTypeQueryHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
            
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _appLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();

        }

        [Fact]
        public async Task GetLeaveTypeQueryHandler_GetAsync_should_return_all_leave_types()
        {
            var handler = new GetLeaveTypesQueryHandler(_mapper, _mockRepo.Object, _appLogger.Object);

            var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<LeaveTypeDto>>();
            result.Count.ShouldBe(3);
        }
    }
}
