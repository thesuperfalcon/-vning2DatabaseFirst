using System;
using System.Collections.Generic;

namespace Övning2DatabaseFirst.Models;

public partial class Car
{
    public int Id { get; set; }

    public string? Plate { get; set; }

    public string? Make { get; set; }

    public string? Color { get; set; }

    public int? ParkingSlotsId { get; set; }

    public virtual ParkingSlot? ParkingSlots { get; set; }
}
