using System;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Pacco.Services.Vehicles.Application.DTO;
using Pacco.Services.Vehicles.Application.Queries;
using Pacco.Services.Vehicles.Infrastructure.Mongo.Documents;

namespace Pacco.Services.Vehicles.Infrastructure.Mongo.Queries.Handlers
{
    internal class GetVehiclesHandler : IQueryHandler<GetVehicles, PagedResult<VehicleDto>>
    {
        private readonly IMongoRepository<VehicleDocument, Guid> _repository;

        public GetVehiclesHandler(IMongoRepository<VehicleDocument, Guid> repository)
        {
            _repository = repository;
        }
        
        public async Task<PagedResult<VehicleDto>> HandleAsync(GetVehicles query)
        {
            var pagedResult = await  _repository.BrowseAsync(v => v.PricePerHour >= query.PriceFrom  
                                                              &&  v.PricePerHour <= query.PriceTo && v.Variants == query.Variants, query);

            return pagedResult?.Map(v => new VehicleDto
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