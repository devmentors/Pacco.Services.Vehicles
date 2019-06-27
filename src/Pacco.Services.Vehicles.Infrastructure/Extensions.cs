using System;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Convey.MessageBrokers;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pacco.Services.Vehicles.Application.Commands;
using Pacco.Services.Vehicles.Application.Events;
using Pacco.Services.Vehicles.Application.Messaging;
using Pacco.Services.Vehicles.Core.Entities;
using Pacco.Services.Vehicles.Core.Repositories;
using Pacco.Services.Vehicles.Infrastructure.IoC;
using Pacco.Services.Vehicles.Infrastructure.Messaging;
using Pacco.Services.Vehicles.Infrastructure.Mongo.Documents;
using Pacco.Services.Vehicles.Infrastructure.Mongo.Repositories;

namespace Pacco.Services.Vehicles.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<IVehiclesRepository, VehiclesMongoRepository>();
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();

            return builder
                .AddMongo()
                .AddMongoRepository<VehicleDocument, Guid>("Vehicles")
                .AddRabbitMq()
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .RegisterExceptionDecorators();
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseRabbitMq()
                .SubscribeCommand<AddVehicle>((cmd, ex) => new AddVehicleRejected(cmd.Id, ex.Message, ex.Code))
                .SubscribeCommand<UpdateVehicle>((cmd, ex) => new UpdateVehicleRejected(cmd.Id, ex.Message, ex.Code))
                .SubscribeCommand<DeleteVehicle>((cmd, ex) => new DeletedVehicleRejected(cmd.Id, ex.Message, ex.Code));
            
            return app;
        }
    }
}