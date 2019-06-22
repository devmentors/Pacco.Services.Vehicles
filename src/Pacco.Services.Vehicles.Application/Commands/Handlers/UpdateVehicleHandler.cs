using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Pacco.Services.Vehicles.Application.Events;
using Pacco.Services.Vehicles.Application.Messaging;
using Pacco.Services.Vehicles.Core.Exceptions;
using Pacco.Services.Vehicles.Core.Repositories;

namespace Pacco.Services.Vehicles.Application.Commands.Handlers
{
    internal class UpdateVehicleHandler : ICommandHandler<UpdateVehicle>
    {
        private readonly IVehiclesRepository _repository;
        private readonly IMessageBroker _broker;

        public UpdateVehicleHandler(IVehiclesRepository repository, IMessageBroker broker)
        {
            _repository = repository;
            _broker = broker;
        }
        
        public async Task HandleAsync(UpdateVehicle command)
        {
            var vehicle = await _repository.GetAsync(command.Id);

            if (vehicle is null)
            {
                throw new VehicleNotFoundException(command.Id);
            }
            
            vehicle.ChangeDescription(command.Description);
            vehicle.ChangePricePerHour(command.PricePerHour);
            vehicle.ChangeVariants(command.Variants);

            await _repository.UpdateAsync(vehicle);
            await _broker.PublishAsync(new VehicleUpdated(command.Id));

        }
    }
}