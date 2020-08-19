using MediatR;

namespace TryOut.NotificationPattern.Api.Requests.Commands.Abstractions
{
    public abstract class DeleteCustomerCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }
}