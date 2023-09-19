using Vehman2.CarModels;
using Vehman2.Fuels;
using Vehman2.Owners;

using System;
using System.Collections.Generic;

namespace Vehman2.Vehicles
{
    public class VehicleWithNavigationProperties
    {
        public Vehicle Vehicle { get; set; }

        public CarModel CarModel { get; set; }
        public Fuel Fuel { get; set; }
        public Owner Owner { get; set; }
        

        
    }
}