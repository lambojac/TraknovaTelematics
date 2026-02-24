using System;
using System.Collections.Generic;

public class Vehicle
{
    public int Id { get; set; }

    public ICollection<Telemetry> Telemetries { get; set; }
}