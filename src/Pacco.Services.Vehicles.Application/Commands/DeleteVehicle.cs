using System;
using Convey.CQRS.Commands;
using Newtonsoft.Json;

namespace Pacco.Services.Vehicles.Application.Commands
{
    public class DeleteVehicle : ICommand
    {
        public Guid Id { get; }

        [JsonConstructor]
        public DeleteVehicle(Guid id)
            => Id = id;
    }
}