using System;
using Convey.CQRS.Commands;

namespace Pacco.Services.Vehicles.Application.Commands
{
    public class DeleteVehicle : ICommand
    {
        public Guid Id { get; }

        public DeleteVehicle()
        {
        }
        
        public DeleteVehicle(Guid id)
            => Id = id;
    }
}