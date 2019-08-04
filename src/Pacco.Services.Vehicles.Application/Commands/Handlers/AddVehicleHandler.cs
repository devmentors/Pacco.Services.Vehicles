using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;
using Pacco.Services.Vehicles.Application.Events;
using Pacco.Services.Vehicles.Application.Messaging;
using Pacco.Services.Vehicles.Core.Entities;
using Pacco.Services.Vehicles.Core.Repositories;

namespace Pacco.Services.Vehicles.Application.Commands.Handlers
{
    internal class AddVehicleHandler : ICommandHandler<AddVehicle>
    {
        private readonly IVehiclesRepository _repository;
        private readonly IMessageBroker _broker;
        private readonly ILogger<AddVehicleHandler> _logger;

        public AddVehicleHandler(IVehiclesRepository repository, IMessageBroker broker,
            ILogger<AddVehicleHandler> logger)
        {
            _repository = repository;
            _broker = broker;
            _logger = logger;
        }

        public async Task HandleAsync(AddVehicle command)
        {
            var vehicle = new Vehicle(
                command.VehicleId,
                command.Brand,
                command.Model,
                command.Description,
                command.PayloadCapacity,
                command.LoadingCapacity,
                command.PricePerService,
                command.Variants);

            await _repository.AddAsync(vehicle);
            await _broker.PublishAsync(new VehicleAdded(command.VehicleId));
            _logger.LogInformation($"Added a vehicle with id: {command.VehicleId}.");
        }
    }
}