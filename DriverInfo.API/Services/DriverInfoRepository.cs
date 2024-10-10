using DriverInfo.API.DbContexts;
using DriverInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DriverInfo.API.Services
{
    public class DriverInfoRepository : IDriverInfoRepository
    {
        private readonly DriverInfoContext _context;

        public DriverInfoRepository(DriverInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Driver>> GetDriversAsync()
        {
            return await _context.Drivers
                .OrderBy(d=>d.Name)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Driver>, PaginationMetadata)> GetDriversAsync(string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            var collection = _context.Drivers as IQueryable<Driver>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(d => d.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery) || (a.Description != null && a.Description.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection
                .OrderBy(d => d.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<Driver?> GetDriverAsync(int driverId, bool includeWins)
        {
            if(includeWins)
            {
                return await _context.Drivers
                    .Include(d => d.Wins)
                    .Where(d=>d.Id == driverId)
                    .FirstOrDefaultAsync();
            }

            return await _context.Drivers
                    .Where(d => d.Id == driverId)
                    .FirstOrDefaultAsync();
        }

        public async Task<Win?> GetWinForDriverAsync(int driverId, int winId)
        {
            return await _context.Wins
                .Where(w => w.DriverId == driverId && w.Id == winId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Win>> GetWinsForDriverAsync(int driverId)
        {
            return await _context.Wins
                .Where(w => w.DriverId == driverId)
                .ToListAsync();
        }

        public async Task<bool> DriverExistsAsync(int driverId)
        {
            return await _context.Drivers.AnyAsync(d => d.Id == driverId);
        }

        public async Task AddWinForDriverAsync(int driverId, Win win)
        {
            var driver = await GetDriverAsync(driverId, false);
            if (driver != null)
            {
                driver.Wins.Add(win);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void DeleteWin(Win win)
        {
            _context.Wins.Remove(win);
        }
    }
}
