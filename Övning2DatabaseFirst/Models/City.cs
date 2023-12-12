using System;
using System.Collections.Generic;

namespace Övning2DatabaseFirst.Models;

public partial class City
{
    public int Id { get; set; }

    public string CityName { get; set; } = null!;

    public virtual ICollection<ParkingHouse> ParkingHouses { get; set; } = new List<ParkingHouse>();
}
