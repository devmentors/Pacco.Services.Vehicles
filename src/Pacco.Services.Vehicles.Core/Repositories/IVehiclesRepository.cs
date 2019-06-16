using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pacco.Services.Vehicles.Core.Entities;

namespace Pacco.Services.Vehicles.Core.Repositories
{
    public interface IVehiclesRepository
    {
        Task<Vehicle> GetAsync(Guid id);
        Task<IEnumerable<Vehicle>> GetAsync(decimal priceFrom, decimal priceTo, Variants variants);
        Task AddAsync(Vehicle vehicle);
        Task UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(Vehicle vehicle);
    }
}