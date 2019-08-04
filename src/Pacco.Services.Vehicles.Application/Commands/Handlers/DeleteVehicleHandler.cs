using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;
using Pacco.Services.Vehicles.Application.Events;
using Pacco.Services.Vehicles.Application.Messaging;
using Pacco.Services.Vehicles.Core.Exceptions;
using Pacco.Services.Vehicles.Core.Repositories;

namespace Pacco.Services.Vehicles.Application.Commands.Handlers
{
    internal class DeleteVehicleHandler : ICommandHandler<DeleteVehicle>
    {
        private readonly IVehiclesRepository _repository;
        private readonly IMessageBroker _broker;
        private readonly ILogger<DeleteVehicleHandler> _logger;

        public DeleteVehicleHandler(IVehiclesRepository repository, IMessageBroker broker,
            ILogger<DeleteVehicleHandler> logger)
        {
            _repository = repository;
            _broker = broker;
            _logger = logger;
        }
        
        public async Task HandleAsync(DeleteVehicle command)
        {
            var vehicle = await _repository.GetAsync(command.VehicleId);

            if (vehicle is null)
            {
                throw new VehicleNotFoundException(command.VehicleId);
            }
            
            await _repository.DeleteAsync(vehicle);
            await _broker.PublishAsync(new VehicleDeleted(command.VehicleId));
            _logger.LogInformation($"Deleted a vehicle with id: {command.VehicleId}.");
        }
    }
}