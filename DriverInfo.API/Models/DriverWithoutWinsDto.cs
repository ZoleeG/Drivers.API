namespace DriverInfo.API.Models
{
    public class DriverWithoutWinsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
