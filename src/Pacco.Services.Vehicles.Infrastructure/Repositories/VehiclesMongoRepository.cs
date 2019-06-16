using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using Pacco.Services.Vehicles.Core.Entities;
using Pacco.Services.Vehicles.Core.Repositories;
using Pacco.Services.Vehicles.Infrastructure.Documents;

namespace Pacco.Services.Vehicles.Infrastructure.Repositories
{
    internal class VehiclesMongoRepository : IVehiclesRepository
    {
        private readonly IMongoRepository<VehicleDocument, Guid> _repository;

        public VehiclesMongoRepository(IMongoRepository<VehicleDocument, Guid> repository)
            => _repository = repository;

        public Task<Vehicle> GetAsync(Guid id)
            => _repository
                .GetAsync(id)
                .AsEntityAsync();

        public async Task<IEnumerable<Vehicle>> GetAsync(decimal priceFrom, decimal priceTo, Variants variants)
        {
            var documents = await  _repository.FindAsync(v => v.PricePerHour >= priceFrom 
                                                      && v.PricePerHour <= priceTo && v.Variants == variants);

            return documents.Select(d => d.AsEntity());
        }

        public Task AddAsync(Vehicle vehicle)
            => _repository.AddAsync(vehicle.AsDocument());

        public Task UpdateAsync(Vehicle vehicle)
            => _repository.UpdateAsync(vehicle.AsDocument());

        public Task DeleteAsync(Vehicle vehicle)
            => _repository.DeleteAsync(vehicle.Id);
    }
}