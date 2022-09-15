using AutoMapper;

namespace CityInfo.Api.Profiles
{
    public class PointOfInterestProfile: Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestCreateDto>();
            CreateMap<Models.PointOfInterestCreateDto, Entities.PointOfInterest>();
            CreateMap<Models.PointOfInterestUpdateDto, Entities.PointOfInterest>();
        }
    }
}
