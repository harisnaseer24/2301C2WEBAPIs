using System;
using System.Collections.Generic;

namespace _2301C2WebAPIs.Models;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? City { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
