using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GlassLewis.Company.API.Application.Events
{
    public class CompanyEventHandler : INotificationHandler<RegisteredCompanyEvent>
    {
        public Task Handle(RegisteredCompanyEvent notification, CancellationToken cancellationToken)
        {
            // Used to work with confirmation event
            return Task.CompletedTask;
        }
    }
}
