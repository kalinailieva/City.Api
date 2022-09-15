using CityInfo.Api.Entities;

namespace CityInfo.Api.Services
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAllCitiesAsync();
        Task<City?> GetCityAsync(int cityId, bool includedPointsOfInterest);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestAsync(int cityId);
        Task<PointOfInterest> GetPointOfInterestAsync(int cityId, int pointOfInterestId);
        Task<bool> CityExistsAsync(int cityId);

        Task AddPointOfInterestAsync(int cityId, PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
        Task DeletePointOfInterestAsync(int cityId, PointOfInterest pointOfInterest);
        Task<IEnumerable<City>> GetAllCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);

    }
}
