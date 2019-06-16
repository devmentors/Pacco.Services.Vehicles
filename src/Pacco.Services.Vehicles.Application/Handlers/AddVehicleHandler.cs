using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Pacco.Services.Vehicles.Application.Commands;
using Pacco.Services.Vehicles.Core.Entities;
using Pacco.Services.Vehicles.Core.Repositories;

namespace Pacco.Services.Vehicles.Application.Handlers
{
    internal class AddVehicleHandler : ICommandHandler<AddVehicle>
    {
        private readonly IVehiclesRepository _repository;

        public AddVehicleHandler(IVehiclesRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(AddVehicle command)
        {
            var vehicle = new Vehicle(
                command.Id,
                command.Brand,
                command.Model,
                command.Description,
                command.PayloadCapacity,
                command.PricePerHour,
                command.Variants);

            await _repository.AddAsync(vehicle);
        }
    }
}