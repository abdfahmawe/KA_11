﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Uitils
{
   public interface ISeedData
    {
        public Task DataSeedingAsync();
        public Task IdentityDataSeedingAsync();
    }
}
