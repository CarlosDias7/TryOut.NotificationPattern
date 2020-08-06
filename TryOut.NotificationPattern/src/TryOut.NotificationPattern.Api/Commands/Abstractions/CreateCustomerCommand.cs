using MediatR;
using System;

namespace TryOut.NotificationPattern.Api.Commands.Abstractions
{
    public abstract class CreateCustomerCommand : IRequest<long>
    {
        public DateTime Birth { get; set; }
        public decimal Credits { get; set; }
        public string Document { get; set; }
        public string Name { get; set; }
    }
}