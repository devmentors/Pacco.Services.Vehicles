namespace Pacco.Services.Vehicles.Core.Exceptions
{
    public class InvalidVehiclePricePerServiceException : DomainException
    {
        public override string Code => "invalid_vehicle_price_per_service";

        public InvalidVehiclePricePerServiceException(decimal pricePerService)
            : base($"Vehicle price per service is invalid: {pricePerService}.")
        {
        }
    }
}