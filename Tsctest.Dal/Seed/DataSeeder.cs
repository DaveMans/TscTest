using System.Collections.Generic;
using System.Linq;
using Tsctest.Core.Models;

namespace Tsctest.Dal.Seed
{
    public class DataSeeder
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Countries.Any())
            {
                var itemsToDelete = context.Set<Country>();
                context.Countries.RemoveRange(itemsToDelete);
                context.SaveChanges();
            }

            var users = new List<Country>() {
                    new Country {
                        Name = "Afghanistan",
                        AlphaCode2 = "AF",
                        AlphaCode3 = "AFG",
                        NumericCode = "004",
                        Independent = true,
                        SubDivisions = new List < SubDivision > {
                            new SubDivision {
                                Name = "Berat",
                                Code = "AL-01"
                            },
                            new SubDivision {
                                Name = "Dibër",
                                Code = "AL-09"
                            },
                            new SubDivision {
                                Name = "Durrës",
                                Code = "AL-02"
                            },
                            new SubDivision {
                                Name = "Elbasan",
                                Code = "AL-03"
                            },
                            new SubDivision {
                                Name = "Fier",
                                Code = "AL-04"
                            },
                            new SubDivision {
                                Name = "Gjirokastër",
                                Code = "AL-05"
                            },
                            new SubDivision {
                                Name = "Korçë",
                                Code = "AL-06"
                            },
                            new SubDivision {
                                Name = "Kukës",
                                Code = "AL-07"
                            },
                            new SubDivision {
                                Name = "Lezhë",
                                Code = "AL-08"
                            },
                            new SubDivision {
                                Name = "Shkodër",
                                Code = "AL-10"
                            },
                            new SubDivision {
                                Name = "Tiranë",
                                Code = "AL-11"
                            },
                            new SubDivision {
                                Name = "Vlorë",
                                Code = "AL-12"
                            },
                        }
                    },
                    new Country {
                        Name = "Aland Islands",
                        AlphaCode2 = "AX",
                        AlphaCode3 = "ALA",
                        NumericCode = "248",
                        Independent = false
                    },
                    new Country {
                        Name = "Albania",
                        AlphaCode2 = "AL",
                        AlphaCode3 = "008",
                        NumericCode = "004",
                        Independent = true,
                        SubDivisions = new List < SubDivision > {
                            new SubDivision {
                                Name = "Kuçovë",
                                Code = "AL-KC"
                            },
                            new SubDivision {
                                Name = "Kukës",
                                Code = "AL-KU"
                            },
                            new SubDivision {
                                Name = "Kurbin",
                                Code = "AL-KB"
                            },
                            new SubDivision {
                                Name = "Lezhë",
                                Code = "AL-LE"
                            },
                            new SubDivision {
                                Name = "Librazhd",
                                Code = "AL-LB"
                            },
                            new SubDivision {
                                Name = "Lushnjë",
                                Code = "AL-LU"
                            },
                            new SubDivision {
                                Name = "Malësi e Madhe",
                                Code = "AL-MM"
                            },
                            new SubDivision {
                                Name = "Mallakastër",
                                Code = "AL-MK"
                            },
                            new SubDivision {
                                Name = "Mat",
                                Code = "AL-MT"
                            },
                            new SubDivision {
                                Name = "Mirditë",
                                Code = "AL-MR"
                            },
                            new SubDivision {
                                Name = "Peqin",
                                Code = "AL-PQ"
                            },
                            new SubDivision {
                                Name = "Përmet",
                                Code = "AL-PR"
                            },
                        }
                    },
                };
            context.Countries.AddRange(users);
            context.SaveChanges();
        }
    }
}
