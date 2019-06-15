using System.Threading.Tasks;

namespace Pacco.Services.Vehicles.Infrastructure.Documents
{
    internal static class Extensions
    {
        public static Core.Entities.Vehicle AsEntity(this Documents.Vehicle document)
            => new Core.Entities.Vehicle(
                document.Id,
                document.Brand,
                document.Model,
                document.Description,
                document.PayloadCapacity,
                document.PricePerHour,
                document.Variants);

        public static async Task<Core.Entities.Vehicle> AsEntityAsync(this Task<Documents.Vehicle> task)
            => (await task).AsEntity();

        public static Documents.Vehicle AsDocument(this Core.Entities.Vehicle entity)
            => new Documents.Vehicle
            {
                Id = entity.Id,
                Brand = entity.Brand,
                Model = entity.Model,
                Description = entity.Description,
                PayloadCapacity = entity.PayloadCapacity,
                PricePerHour = entity.PricePerHour,
                Variants = entity.Variants
            };
        
        public static async Task<Documents.Vehicle> AsDocumentAsync(this Task<Core.Entities.Vehicle> task)
            => (await task).AsDocument();
    }
}