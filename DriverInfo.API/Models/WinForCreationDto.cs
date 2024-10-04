using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DriverInfo.API.Models
{
    public class WinForCreationDto
    {
        [Required(ErrorMessage = "You should provide a name value")]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        [Range(1, 30, ErrorMessage = "Please enter a valid grid position")]
        public int GridPosition { get; set; }

        [Range(1946, 2099, ErrorMessage = "Please enter a valid year")]
        public int Year { get; set; } = DateTime.Now.Year;
    }
}
