using System;
using Convey.CQRS.Events;

namespace Pacco.Services.Vehicles.Application.Events
{
    public class VehicleDeleted : IEvent
    {
        public Guid Id { get; }

        public VehicleDeleted(Guid id)
            => Id = id;
    }
}