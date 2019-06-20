using System;
using Convey.CQRS.Events;

namespace Pacco.Services.Vehicles.Application.Events
{
    public class VehicleUpdated : IEvent
    {
        public Guid Id { get; }

        public VehicleUpdated(Guid id)
            => Id = id;
    }
}