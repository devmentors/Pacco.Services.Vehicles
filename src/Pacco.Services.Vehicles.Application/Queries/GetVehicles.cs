using System.Collections.Generic;
using Convey.CQRS.Queries;
using Pacco.Services.Vehicles.Application.DTO;
using Pacco.Services.Vehicles.Core.Entities;

namespace Pacco.Services.Vehicles.Application.Queries
{
    public class GetVehicles : PagedQueryBase, IQuery<PagedResult<VehicleDto>>
    {
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }
        public Variants Variants { get; set; }
    }
}