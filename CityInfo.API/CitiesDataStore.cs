using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Denver",
                    Description = "The city that is a mile high",
                    PointsOfIntrest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Mile High Stadium",
                            Description = "Home of the Broncos"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Colorado Mint",
                            Description = "Where they make money"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Parker,",
                    Description = "City that is really a town",
                    PointsOfIntrest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "O'Brian Water Park",
                            Description = "The only water park in town"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "Some Park",
                            Description = "There is probably a park in Parker"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Longmont",
                    Description = "This place is north of Denver but south of the North Pole",
                    PointsOfIntrest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "Longmont Village",
                            Description = "Some made up place"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 6,
                            Name = "City Center",
                            Description = "Not even sure there is a place called \"City Center\" but it could be in the center of the city"
                        }
                    }
                }
            };
        }
    }
}
