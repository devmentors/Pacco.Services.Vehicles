using System;
using Convey.CQRS.Commands;
using Newtonsoft.Json;
using Pacco.Services.Vehicles.Core.Entities;

namespace Pacco.Services.Vehicles.Application.Commands
{
    public class AddVehicle : ICommand
    {
        public Guid Id { get; }
        public string Brand { get; }
        public string Model { get; }
        public string Description { get; }
        public ushort PayloadCapacity { get; }
        public decimal PricePerHour { get; }
        public Variants Variants { get; }

        [JsonConstructor]
        public AddVehicle(Guid id, string brand, string model, string description, ushort payloadCapacity, 
            decimal pricePerHour, Variants variants)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Description = description;
            PayloadCapacity = payloadCapacity;
            PricePerHour = pricePerHour;
            Variants = variants;
        }
    }
}