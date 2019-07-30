using System;
using Convey;
using Convey.CQRS.Queries;
using Convey.Discovery.Consul;
using Convey.HTTP;
using Convey.LoadBalancing.Fabio;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Metrics.AppMetrics;
using Convey.Persistence.MongoDB;
using Convey.Tracing.Jaeger;
using Convey.Tracing.Jaeger.RabbitMQ;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pacco.Services.Vehicles.Application;
using Pacco.Services.Vehicles.Application.Commands;
using Pacco.Services.Vehicles.Application.Messaging;
using Pacco.Services.Vehicles.Core.Repositories;
using Pacco.Services.Vehicles.Infrastructure.Exceptions;
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
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddHttpClient()
                .AddConsul()
                .AddFabio()
                .AddRabbitMq<CorrelationContext>(plugins: p => p.RegisterJaeger())
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                .AddMongo()
                .AddMetrics()
                .AddJaeger()
                .AddMongo()
                .AddMongoRepository<VehicleDocument, Guid>("Vehicles");
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
                .UseJaeger()
                .UseInitializers()
                .UsePublicContracts<ContractAttribute>()
                .UseConsul()
                .UseMetrics()
                .UseRabbitMq()
                .SubscribeCommand<AddVehicle>()
                .SubscribeCommand<UpdateVehicle>()
                .SubscribeCommand<DeleteVehicle>();
            
            return app;
        }
    }
}