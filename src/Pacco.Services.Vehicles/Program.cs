using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pacco.Services.Vehicles.Application;
using Pacco.Services.Vehicles.Application.Commands;
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
                    .UseErrorHandler()
                    .UsePublicContracts()
                    .UseEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync("Welcome to Pacco Parcels Service!")))
                    .UseDispatcherEndpoints(endpoints =>
                    {
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
