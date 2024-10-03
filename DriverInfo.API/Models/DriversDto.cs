namespace DriverInfo.API.Models
{
    public class DriversDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<WinDto> Wins { get; set; } = new List<WinDto>();
        public int NumberOfWins
        {
            get
            {
                return Wins.Count;
            }
        }
    }
}
