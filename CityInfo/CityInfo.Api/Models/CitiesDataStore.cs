namespace CityInfo.Api.Models
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

  

        //init dummy data
        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York",
                    Description = "The one with the big park",
                    PointsOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                           Id = 1,
                           Name = "City Tower",
                           Description = "The best tower ever"
                        },
                         new PointOfInterestDto()
                        {
                           Id = 2,
                           Name = "City Centre",
                           Description = "Beautiful flowers"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Sofia",
                    Description = "The one with the nice view",
                    PointsOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                           Id = 1,
                           Name = "City Tower",
                           Description = "The best tower ever"
                        },
                         new PointOfInterestDto()
                        {
                           Id = 2,
                           Name = "City Centre",
                           Description = "Beautiful flowers"
                        }
                    }
                }
            };
        }
    }
}
