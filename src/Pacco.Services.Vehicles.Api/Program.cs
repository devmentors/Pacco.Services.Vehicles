using System;
using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Queries;
using Convey.Logging;
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

namespace Pacco.Services.Vehicles.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddConvey()
                    .AddWebApi()
                    .AddApplication()
                    .AddInfrastructure()
                    .Build())
                .Configure(app => app
                    .UseInfrastructure()
                    .UseDispatcherEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync("Welcome to Pacco Vehicles Service!"))
                        .Get<GetVehicle, VehicleDto>("vehicles/{id}")
                        .Get<SearchVehicles, PagedResult<VehicleDto>>("vehicles")
                        .Post<AddVehicle>("vehicles",
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"vehicles/{cmd.Id}"))
                        .Put<UpdateVehicle>("vehicles/{id}",
                            (cmd, ctx) =>
                            {
                                cmd.Bind(c => c.Id, ctx.Args<Guid>("id"));
                                return Task.CompletedTask;
                            },
                            (cmd, ctx) => ctx.Response.Created($"vehicles/{cmd.Id}"))
                        .Delete<DeleteVehicle>("vehicles/{id}")
                    ))
                .UseLogging()
                .Build()
                .RunAsync();
    }
}
