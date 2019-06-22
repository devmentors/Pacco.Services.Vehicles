namespace Pacco.Services.Vehicles.Core.Exceptions
{
    public class InvalidVehicleDescriptionException : ExceptionBase
    {
        public override string Code => "invalid_vehicle_description";

        public InvalidVehicleDescriptionException(string description)
            : base($"Vehicle description is invalid: {description}.")
        {
        }
    }
}