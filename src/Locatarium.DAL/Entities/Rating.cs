using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locatarium.DAL.Entities
{
    [Table("Ratings")]
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double Value { get; set; }

        [ForeignKey("Residence")]
        public int ResidenceId { get; set; }

        public Residence Residence { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}
