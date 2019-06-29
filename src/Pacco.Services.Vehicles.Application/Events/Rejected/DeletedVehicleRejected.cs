using System;
using Convey.CQRS.Events;

namespace Pacco.Services.Vehicles.Application.Events.Rejected
{
    public class DeletedVehicleRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public string Reason { get; }
        public string Code { get; }

        public DeletedVehicleRejected(Guid id, string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}