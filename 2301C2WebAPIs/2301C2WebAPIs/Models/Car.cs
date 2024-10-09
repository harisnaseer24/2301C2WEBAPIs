using System;
using System.Collections.Generic;

namespace _2301C2WebAPIs.Models;

public partial class Car
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int ManufacturerId { get; set; }

    public string Color { get; set; } = null!;

    public string Power { get; set; } = null!;

    public long Price { get; set; }

    public virtual Manufacturer Manufacturer { get; set; } = null!;
}
