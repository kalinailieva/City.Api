using CityInfo.Api.DbContexts;
using CityInfo.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Api.Services
{
    public class CityRepository : ICityRepository
    {
        private readonly CityContext _context;
        public CityRepository(CityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await _context.Cities.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId, bool includedPointsOfInterest)
        {
            if (includedPointsOfInterest)
            {
                return await _context.Cities.Include(x => x.PointsOfInterest).Where(x => x.Id == cityId).FirstOrDefaultAsync();
            }
            return await _context.Cities.Where(x => x.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointsOfInterest.Where(x => x.CityId == cityId && x.Id == pointOfInterestId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestAsync(int cityId)
        {
            return await _context.PointsOfInterest.Where(x => x.CityId == cityId).ToListAsync();
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(x => x.Id == cityId);
        }

        public async Task AddPointOfInterestAsync(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);
            if (city != null)
            {
                city.PointsOfInterest.Add(pointOfInterest);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task UpdatePointOfInterestAsync(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);
            if (city != null)
            {
                 _context.PointsOfInterest.Update(pointOfInterest);
            }            
        }

        public async Task DeletePointOfInterestAsync(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);
            if (city != null)
            {
                _context.PointsOfInterest.Remove(pointOfInterest);
            }
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize)
        {     
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(searchQuery))
            {
                return await GetAllCitiesAsync();
            }

            var cities = await GetAllCitiesAsync();

            if (!string.IsNullOrEmpty(name))
            {
                cities = cities.Where(x => x.Name == name);
            }
            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                cities = cities.Where(x=> x.Description != null && x.Description.Contains(searchQuery));
            }
            return  cities.OrderBy(x => x.Name)
                .Skip(pageSize * (pageNumber -1))
                .Take(pageSize)
                .ToList();
        }

        //return data + pagination meta data in tupple

        public async Task<(IEnumerable<City>, PaginationMetaData)> GetAllCitiesAsyncWithPageMatadata(
            string? name, string? searchQuery, int pageNumber, int pageSize)
        {
 

            var cities = await GetAllCitiesAsync();

            if (!string.IsNullOrEmpty(name))
            {
                cities = cities.Where(x => x.Name == name);
            }
            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                cities = cities.Where(x => x.Description != null && x.Description.Contains(searchQuery));
            }

            var totalItemsCount = cities.Count();

            var paginationMetaData = new PaginationMetaData
                (totalItemsCount, pageSize, pageNumber);

            var collectionToReturn =  cities.OrderBy(x => x.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return (collectionToReturn, paginationMetaData);
        }
    }
}
