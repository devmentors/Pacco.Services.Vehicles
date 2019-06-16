using System;
using Pacco.Services.Vehicles.Core.Exceptions;

namespace Pacco.Services.Vehicles.Core.Entities
{
    public class Vehicle
    {
        public Guid Id { get; protected set; }
        public string Brand { get; protected set; }
        public string Model { get; protected set; }
        public string Description { get; protected set; }
        public ushort PayloadCapacity { get; protected set; }
        public decimal PricePerHour { get; protected set; }
        public Variants Variants { get; protected set; }

        public Vehicle(Guid id, string brand, string model, string description, ushort payloadCapacity,
            decimal pricePerHour)
        {
            Id = id;
            Brand = brand;
            Model = model;
            ChangeDescription(description);
            PayloadCapacity = payloadCapacity;
            ChangePricePerHour(pricePerHour);
            AddVariants(Variants.Standard);
        }
        
        public Vehicle(Guid id, string brand, string model, string description, ushort payloadCapacity,
            decimal pricePerHour, params Variants[] variants) 
            : this(id, brand, model, description, payloadCapacity, pricePerHour)
        {
            AddVariants(variants);
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new DomainException("Vehicle's description cannot be empty.");
            }

            Description = description;
        }

        public void ChangePricePerHour(decimal pricePerHour)
        {
            if (pricePerHour <= 0)
            {
                throw  new DomainException("Vehicle's price per hour cannot be less/equal zero.");
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