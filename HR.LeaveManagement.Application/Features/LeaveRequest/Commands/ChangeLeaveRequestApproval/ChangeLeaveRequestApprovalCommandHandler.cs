using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<ChangeLeaveRequestApprovalCommandHandler> _appLogger;
        public ChangeLeaveRequestApprovalCommandHandler(ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender, IAppLogger<ChangeLeaveRequestApprovalCommandHandler> appLogger)
        {
            this._leaveRequestRepository = leaveRequestRepository;
            this._emailSender = emailSender;
            this._appLogger = appLogger;
        }
        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest is null)
                throw new NotFoundException(nameof(leaveRequest), request.Id);

            leaveRequest.Approved = request.Approved;
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            // if request is approved, get and update the employee's allocations

            try
            {
                // Send confirmation email
                var email = new EmailMessage
                {
                    To = String.Empty, /* Get email from employee record */
                    Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                                $"has been updated",
                    Subject = "Leave Request Approval Status Updated"
                };
                await _emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
            }

            return Unit.Value;


        }
    }
}
