using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationsDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<List<LeaveAllocationDto>> Get(bool isLoggedInUser = false)
        {
            var LeaveAllocations = await _mediator.Send(new GetLeaveAllocationsQuery());
            return LeaveAllocations;
        }

        // GET api/<LeaveAllocationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationsDetailsDto>> Get(int id)
        {
            var LeaveAllocation = await _mediator.Send(new GetLeaveAllocationsDetailsQuery { Id = id });
            return Ok(LeaveAllocation);
        }

        // POST api/<LeaveAllocationController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Post(CreateLeaveAllocationCommand LeaveAllocation)
        {
            var response = await _mediator.Send(LeaveAllocation);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<LeaveAllocationController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateLeaveAllocationCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<LeaveAllocationController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveAllocationCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
