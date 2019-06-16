using System;
using System.Collections.Generic;

namespace Pacco.Services.Vehicles.Application.DTO
{
    public class VehicleDto
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public ushort PayloadCapacity { get; set; }
        public decimal PricePerHour { get; set; }
        public IEnumerable<string> Variants { get; set; }
    }
}