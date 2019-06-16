using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Pacco.Services.Vehicles.Application.Commands;
using Pacco.Services.Vehicles.Core.Exceptions;
using Pacco.Services.Vehicles.Core.Repositories;

namespace Pacco.Services.Vehicles.Application.Handlers
{
    internal class UpdateVehicleHandler : ICommandHandler<UpdateVehicle>
    {
        private readonly IVehiclesRepository _repository;

        public UpdateVehicleHandler(IVehiclesRepository repository)
        {
            _repository = repository;
        }
        
        public async Task HandleAsync(UpdateVehicle command)
        {
            var vehicle = await _repository.GetAsync(command.Id);

            if (vehicle is null)
            {
                throw new DomainException($"Vehicle with {command.Id} Id not found");
            }
            
            vehicle.ChangeDescription(command.Description);
            vehicle.ChangePricePerHour(command.PricePerHour);
            vehicle.ChangeVariants(command.Variants);

            await _repository.UpdateAsync(vehicle);
        }
    }
}