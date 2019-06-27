using System;
using Pacco.Services.Vehicles.Core.Exceptions;

namespace Pacco.Services.Vehicles.Core.Entities
{
    public class Vehicle
    {
        public AggregateId Id { get; protected set; }
        public string Brand { get; protected set; }
        public string Model { get; protected set; }
        public string Description { get; protected set; }
        public ushort PayloadCapacity { get; protected set; }
        public ushort LoadingCapacity { get; protected set; }
        public decimal PricePerHour { get; protected set; }
        public Variants Variants { get; protected set; }

        public Vehicle(AggregateId id, string brand, string model, string description, ushort payloadCapacity,
            ushort loadingCapacity, decimal pricePerHour)
        {
            Id = id;
            Brand = brand;
            Model = model;
            ChangeDescription(description);
            PayloadCapacity = payloadCapacity;
            LoadingCapacity = loadingCapacity;
            ChangePricePerHour(pricePerHour);
            AddVariants(Variants.Standard);
        }
        
        public Vehicle(AggregateId id, string brand, string model, string description, ushort payloadCapacity,
            ushort loadingCapacity, decimal pricePerHour, params Variants[] variants) 
            : this(id, brand, model, description, payloadCapacity, loadingCapacity, pricePerHour)
        {
            AddVariants(variants);
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new InvalidVehicleDescriptionException(description);
            }

            Description = description;
        }

        public void ChangePricePerHour(decimal pricePerHour)
        {
            if (pricePerHour <= 0)
            {
                throw  new InvalidVehiclePricePerHourException(pricePerHour);
            }

            PricePerHour = pricePerHour;
        }

        public void ChangeVariants(Variants variants)
            => Variants = variants;

        public void AddVariants(params Variants[] variants)
        {
            foreach (var variant in variants)
            {
                Variants |= variant;
            }
        }
        
        public void RemoveVariants(params Variants[] variants)
        {
            foreach (var variant in variants)
            {
                Variants &= ~variant;
            }
        }
    }
}