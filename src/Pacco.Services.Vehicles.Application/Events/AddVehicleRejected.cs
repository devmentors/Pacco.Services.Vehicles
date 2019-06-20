using System;
using Convey.CQRS.Events;

namespace Pacco.Services.Vehicles.Application.Events
{
    public class AddVehicleRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public string Reason { get; }
        public string Code { get; }

        public AddVehicleRejected(Guid id, string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}