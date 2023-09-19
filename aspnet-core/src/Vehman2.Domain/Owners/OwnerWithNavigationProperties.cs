using Vehman2.Companies;

using System;
using System.Collections.Generic;

namespace Vehman2.Owners
{
    public class OwnerWithNavigationProperties
    {
        public Owner Owner { get; set; }

        public Company Company { get; set; }
        

        
    }
}