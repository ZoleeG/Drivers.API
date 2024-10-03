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
                    Description = "The guy who always wins."
                },
                new DriversDto()
                {
                    Id= 2,
                    Name = "Lewis Hamilton",
                    Description = "Won F1-championship 7 times."
                }
            };
        }
    }
}
