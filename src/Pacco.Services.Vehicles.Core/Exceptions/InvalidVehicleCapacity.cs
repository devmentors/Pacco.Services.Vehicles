namespace Pacco.Services.Vehicles.Core.Exceptions
{
    public class InvalidVehicleCapacity : ExceptionBase
    {
        public override string Code => "invalid_vehicle_capacity";

        public InvalidVehicleCapacity(double capacity)
            : base($"Vehicle capacity is invalid: {capacity}.")
        {
        }
    }
}