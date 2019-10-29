using System;
using Pacco.Services.Vehicles.Core.Exceptions;

namespace Pacco.Services.Vehicles.Application.Exceptions
{
    public class VehicleNotFoundException : ExceptionBase
    {
        public override string Code => "vehicle_not_found";

        public VehicleNotFoundException(Guid id)
            : base($"Vehicle not found: {id}.")
        {
        }
    }
}