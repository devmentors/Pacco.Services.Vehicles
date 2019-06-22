namespace Pacco.Services.Vehicles.Core.Exceptions
{
    public class InvalidVehiclePricePerHourException : ExceptionBase
    {
        public override string Code => "invalid_vehicle_price_per_hour";

        public InvalidVehiclePricePerHourException(decimal pricePerHour)
            : base($"Vehicle price per hour is invalid: {pricePerHour}.")
        {
        }
    }
}