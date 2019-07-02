using System;

namespace Pacco.Services.Vehicles.Core.Exceptions
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