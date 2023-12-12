using System;
using System.Collections.Generic;

namespace Övning2DatabaseFirst.Models;

public partial class ParkingSlot
{
    public int Id { get; set; }

    public int? SlotNumber { get; set; }

    public bool? ElectricOutlet { get; set; }

    public int? ParkingHouseId { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual ParkingHouse? ParkingHouse { get; set; }
}
