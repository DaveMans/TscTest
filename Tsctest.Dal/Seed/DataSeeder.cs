using System;
using System.Collections.Generic;
using System.Linq;
using Tsctest.Core.Models;

namespace Tsctest.Dal.Seed
{
    public class DataSeeder
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Countries.Any())
            {
                var users = new List<Country>()
         {
            new Country { Name = "Afghanistan", AlphaCode2 = "AF", AlphaCode3 = "AFG", NumericCode = "004"  },
            new Country { Name = "Aland Islands", AlphaCode2 = "AX", AlphaCode3 = "ALA", NumericCode = "248"  },
            new Country { Name = "Albania", AlphaCode2 = "AL", AlphaCode3 = "008", NumericCode = "004"  },
         };
                context.Countries.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
