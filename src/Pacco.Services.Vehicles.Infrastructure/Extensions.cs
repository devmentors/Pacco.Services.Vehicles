using System;
using Convey;
using Convey.Persistence.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using Pacco.Services.Vehicles.Core.Repositories;
using Pacco.Services.Vehicles.Infrastructure.Documents;
using Pacco.Services.Vehicles.Infrastructure.Repositories;

namespace Pacco.Services.Vehicles.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<IVehiclesRepository, VehiclesMongoRepository>();
            
            return builder
                .AddMongo()
                .AddMongoRepository<VehicleDocument, Guid>("Vehicles");
        }
    }
}