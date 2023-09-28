﻿using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Common.Classes;

public class Motorcycle : IVehicle
{   
    public string RegNo { get; init; }
    public string Make { get; init; }
    public int Odometer { get; set; }
    public double CostKm { get; set; }
    public VehicleTypes VehicleType { get; init; } = VehicleTypes.Motorcycle;
    public double CostPerDay { get; init; }
    public VehicleStatuses Status { get; set; }

    public Motorcycle(string regNo, string make, int odometer, double costKm,
     double costPerDay, VehicleStatuses status)
    => (RegNo, Make, Odometer, CostKm, CostPerDay, Status)
     = (regNo, make, odometer, costKm, costPerDay, status);
}
