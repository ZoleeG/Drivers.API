using DriverInfo.API.Entities;

namespace DriverInfo.API.Services
{
    public interface IDriverInfoRepository
    {
        Task<IEnumerable<Driver>> GetDriversAsync();
        Task<IEnumerable<Driver>> GetDriversAsync(string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<Driver?> GetDriverAsync(int driverId, bool includeWins);
        Task<IEnumerable<Win>> GetWinsForDriverAsync(int driverId);
        Task<Win?> GetWinForDriverAsync(int driverId, int winId);
        Task<bool> DriverExistsAsync(int driverId);
        Task AddWinForDriverAsync(int driverId, Win win);
        void DeleteWin(Win win);
        Task<bool> SaveChangesAsync();
    }
}
