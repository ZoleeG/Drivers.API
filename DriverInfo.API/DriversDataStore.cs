using DriverInfo.API.Models;

namespace DriverInfo.API
{
    public class DriversDataStore
    {
        public List<DriversDto> Drivers { get; set; }
        public static DriversDataStore Current { get; } = new DriversDataStore();
        public DriversDataStore()
        {
            Drivers = new List<DriversDto>()
            {
                new DriversDto()
                {
                    Id = 1,
                    Name = "Max Verstappen",
                    Description = "The guy who always wins.",
                    Wins = new List<WinDto>()
                    {
                        new WinDto()
                        {
                            Id = 1,
                            Name = "Spanish Grand Prix",
                            GridPosition = 4,
                            Year = 2016
                        },
                        new WinDto()
                        {
                            Id = 2,
                            Name = "Malaysian Grand Prix",
                            GridPosition = 3,
                            Year = 2017
                        },
                        new WinDto()
                        {
                            Id = 3,
                            Name = "Mexican Grand Prix",
                            GridPosition = 2,
                            Year = 2017
                        }
                    }
                },
                new DriversDto()
                {
                    Id= 2,
                    Name = "Lewis Hamilton",
                    Description = "Won F1-championship 7 times.",
                    Wins = new List<WinDto>()
                    {
                        new WinDto()
                        {
                            Id = 1,
                            Name = "Canadian Grand Prix",
                            GridPosition = 1,
                            Year = 2007
                        },
                        new WinDto()
                        {
                            Id = 2,
                            Name = "United States Grand Prix",
                            GridPosition = 1,
                            Year = 2007
                        },
                        new WinDto()
                        {
                            Id = 3,
                            Name = "Hungarian Grand Prix",
                            GridPosition = 1,
                            Year = 2007
                        },
                        new WinDto()
                        {
                            Id = 4,
                            Name = "Japanese Grand Prix",
                            GridPosition = 1,
                            Year = 2007
                        }
                    }
                }
            };
            
        }
    }
}
