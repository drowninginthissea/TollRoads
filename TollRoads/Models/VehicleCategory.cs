﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace TollRoads.Models;

public partial class VehicleCategory
{
    public int Id { get; set; }

    public string CategoryName { get; set; }

    public decimal FaceCoefficient { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}