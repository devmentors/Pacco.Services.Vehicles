using System;
using Convey.CQRS.Commands;
using Pacco.Services.Vehicles.Core.Entities;

namespace Pacco.Services.Vehicles.Application.Commands
{
    [Contract]
    public class UpdateVehicle : ICommand
    {
        public Guid Id { get; }
        public string Description { get; }
        public decimal PricePerService { get; }
        public Variants Variants { get; }

        public UpdateVehicle(Guid id,string description, decimal pricePerService, Variants variants)
        {
            Id = id;
            Description = description;
            PricePerService = pricePerService;
            Variants = variants;
        }
    }
}