using System;
using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Queries;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Pacco.Services.Vehicles.Application;
using Pacco.Services.Vehicles.Application.Commands;
using Pacco.Services.Vehicles.Application.DTO;
using Pacco.Services.Vehicles.Application.Queries;
using Pacco.Services.Vehicles.Infrastructure;

namespace Pacco.Services.Vehicles
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddConvey()
                    .AddApplication()
                    .AddInfrastructure()
                    .AddWebApi())
                .Configure(app => app
                    .UseInitializers()
                    .UseInfrastructure()
                    .UseErrorHandler()
                    .UsePublicContracts()
                    .UseEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync("Welcome to Pacco Vehicles Service!")))
                    .UseDispatcherEndpoints(endpoints =>
                    {
                        endpoints.Get<GetVehicles,PagedResult<VehicleDto>>("vehicles");

                        endpoints.Post<AddVehicle>("vehicles",
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"vehicles/{cmd.Id}"));
                        
                        endpoints.Put<UpdateVehicle>("vehicles/{id}",
                            beforeDispatch: (cmd, ctx) =>
                            {
                                cmd.Bind(c => c.Id, ctx.Args<Guid>("id"));
                                return Task.CompletedTask;
                            },
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"vehicles/{cmd.Id}"));
                        
                        endpoints.Delete("vehicles/{id}",
                            ctx => ctx.SendAsync(new DeleteVehicle(ctx.Args<Guid>("id"))));
                    }))
                .Build()
                .RunAsync();
    }
}
