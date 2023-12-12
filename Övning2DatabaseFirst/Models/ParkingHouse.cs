using System;
using System.Collections.Generic;

namespace Övning2DatabaseFirst.Models;

public partial class ParkingHouse
{
    public int Id { get; set; }

    public string? HouseName { get; set; }

    public int? CityId { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<ParkingSlot> ParkingSlots { get; set; } = new List<ParkingSlot>();
}
