using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Pacco.Services.Vehicles.Application.DTO;
using Pacco.Services.Vehicles.Application.Queries;
using Pacco.Services.Vehicles.Core.Repositories;

namespace Pacco.Services.Vehicles.Application.Handlers
{
    public class GetVehiclesHandler : IQueryHandler<GetVehicles, IEnumerable<VehicleDto>>
    {
        private readonly IVehiclesRepository _repository;

        public GetVehiclesHandler(IVehiclesRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<VehicleDto>> HandleAsync(GetVehicles query)
        {
            var vehicles = await _repository.GetAsync(query.PriceFrom, query.PriceTo, query.Variants);

            return vehicles?.Select(v => new VehicleDto
            {
                Id = v.Id,
                Brand = v.Brand,
                Model = v.Model,
                Description = v.Description,
                PayloadCapacity = v.PayloadCapacity,
                PricePerHour = v.PricePerHour,
                Variants = v.Variants.ToString().Split(',')
            });
        }
    }
}