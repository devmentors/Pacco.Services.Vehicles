using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Pacco.Services.Vehicles.Application.Commands;
using Pacco.Services.Vehicles.Core.Exceptions;
using Pacco.Services.Vehicles.Core.Repositories;

namespace Pacco.Services.Vehicles.Application.Handlers
{
    internal class DeleteVehicleHandler : ICommandHandler<DeleteVehicle>
    {
        private readonly IVehiclesRepository _repository;

        public DeleteVehicleHandler(IVehiclesRepository repository)
        {
            _repository = repository;
        }
        
        public async Task HandleAsync(DeleteVehicle command)
        {
            var vehicle = await _repository.GetAsync(command.Id);

            if (vehicle is null)
            {
                throw new DomainException($"Vehicle with {command.Id} Id not found");
            }
            
            await _repository.DeleteAsync(vehicle);
        }
    }
}