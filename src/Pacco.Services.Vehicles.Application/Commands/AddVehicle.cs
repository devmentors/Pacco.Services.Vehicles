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
        public double PayloadCapacity { get; }
        public double LoadingCapacity { get; }
        public decimal PricePerHour { get; }
        public Variants Variants { get; }

        public AddVehicle()
        {
            
        }
        
        [JsonConstructor]
        public AddVehicle(Guid id, string brand, string model, string description, double payloadCapacity, 
            double loadingCapacity, decimal pricePerHour, Variants variants)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Description = description;
            PayloadCapacity = payloadCapacity;
            LoadingCapacity = loadingCapacity;
            PricePerHour = pricePerHour;
            Variants = variants;
        }
    }
}