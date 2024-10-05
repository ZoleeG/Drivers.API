using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriverInfo.API.Entities
{
    public class Win
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int GridPosition { get; set; }

        public int Year { get; set; }

        [ForeignKey("DriverId")]
        public Driver? Driver { get; set; }
        public int DriverId { get; set; }

        public Win(string name)
        {
            Name = name;
        }

    }
}
