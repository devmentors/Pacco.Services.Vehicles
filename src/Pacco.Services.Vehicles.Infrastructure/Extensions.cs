using System;
using Convey;
using Convey.CQRS.Queries;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pacco.Services.Vehicles.Application.Commands;
using Pacco.Services.Vehicles.Application.Messaging;
using Pacco.Services.Vehicles.Core.Repositories;
using Pacco.Services.Vehicles.Infrastructure.Documents;
using Pacco.Services.Vehicles.Infrastructure.Messaging;
using Pacco.Services.Vehicles.Infrastructure.Repositories;

namespace Pacco.Services.Vehicles.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<IVehiclesRepository, VehiclesMongoRepository>();
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();

            builder.AddInitializer<IMongoDbInitializer>();

            return builder
                .AddMongo()
                .AddMongoRepository<VehicleDocument, Guid>("Vehicles")
                .AddRabbitMq()
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher();
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseRabbitMq()
                .SubscribeCommand<AddVehicle>()
                .SubscribeCommand<UpdateVehicle>()
                .SubscribeCommand<DeleteVehicle>();
            
            return app;
        }
    }
}