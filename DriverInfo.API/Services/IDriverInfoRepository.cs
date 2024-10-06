﻿using DriverInfo.API.Entities;

namespace DriverInfo.API.Services
{
    public interface IDriverInfoRepository
    {
        Task<IEnumerable<Driver>> GetDriversAsync();
        Task<Driver?> GetDriverAsync(int driverId, bool includeWins);
        Task<IEnumerable<Win>> GetWinsForDriverAsync(int driverId);
        Task<Win?> GetWinForDriverAsync(int driverId, int winId);
    }
}