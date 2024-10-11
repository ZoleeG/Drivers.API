namespace DriverInfo.API.Models
{
    /// <summary>
    /// A driver without wins
    /// </summary>
    public class DriverWithoutWinsDto
    {
        /// <summary>
        /// The id of the driver
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the driver
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The description of the driver
        /// </summary>
        public string? Description { get; set; }
    }
}
