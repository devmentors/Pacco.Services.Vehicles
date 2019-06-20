using System.Threading.Tasks;
using Pacco.Services.Vehicles.Core.Entities;

namespace Pacco.Services.Vehicles.Infrastructure.Documents
{
    internal static class Extensions
    {
        public static Vehicle AsEntity(this VehicleDocument document)
            => document is null? null : new Vehicle(
                document.Id,
                document.Brand,
                document.Model,
                document.Description,
                document.PayloadCapacity,
                document.PricePerHour,
                document.Variants);

        public static async Task<Vehicle> AsEntityAsync(this Task<VehicleDocument> task)
            => (await task).AsEntity();

        public static VehicleDocument AsDocument(this Vehicle entity)
            => new VehicleDocument
            {
                Id = entity.Id,
                Brand = entity.Brand,
                Model = entity.Model,
                Description = entity.Description,
                PayloadCapacity = entity.PayloadCapacity,
                PricePerHour = entity.PricePerHour,
                Variants = entity.Variants
            };
        
        public static async Task<VehicleDocument> AsDocumentAsync(this Task<Vehicle> task)
            => (await task).AsDocument();
    }
}