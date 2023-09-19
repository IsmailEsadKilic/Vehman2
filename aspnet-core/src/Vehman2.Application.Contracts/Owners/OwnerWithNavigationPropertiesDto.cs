using Vehman2.Companies;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace Vehman2.Owners
{
    public class OwnerWithNavigationPropertiesDto
    {
        public OwnerDto Owner { get; set; }

        public CompanyDto Company { get; set; }

    }
}