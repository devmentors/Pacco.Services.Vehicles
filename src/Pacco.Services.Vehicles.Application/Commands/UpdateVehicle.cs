using System;
using Convey.CQRS.Commands;
using Newtonsoft.Json;
using Pacco.Services.Vehicles.Core.Entities;

namespace Pacco.Services.Vehicles.Application.Commands
{
    public class UpdateVehicle : ICommand
    {
        public Guid Id { get; }
        public string Description { get; }
        public decimal PricePerHour { get; }
        public Variants Variants { get; }

        public UpdateVehicle()
        {
        }
        
        [JsonConstructor]
        public UpdateVehicle(Guid id,string description, decimal pricePerHour, Variants variants)
        {
            Id = id;
            Description = description;
            PricePerHour = pricePerHour;
            Variants = variants;
        }
    }
}