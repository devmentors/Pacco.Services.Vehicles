using System;
using Convey.CQRS.Events;

namespace Pacco.Services.Vehicles.Application.Events
{
    [Contract]
    public class VehicleAdded : IEvent
    {
        public Guid Id { get; }

        public VehicleAdded(Guid id)
            => Id = id;
    }
}