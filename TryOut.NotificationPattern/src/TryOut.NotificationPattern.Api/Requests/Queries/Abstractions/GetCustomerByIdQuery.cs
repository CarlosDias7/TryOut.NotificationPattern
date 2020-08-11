using MediatR;

namespace TryOut.NotificationPattern.Api.Requests.Queries.Abstractions
{
    public abstract class GetCustomerByIdQuery : IRequest<string>
    {
        public long Id { get; set; }
    }
}