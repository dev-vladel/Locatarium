using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locatarium.DAL.Entities
{
    [Table("Residences")]
    public class Residence
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public ICollection<ResidenceImage> ResidenceImages { get; set; }

        public User User { get; set; }

        public Address Address { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public ICollection<ResidenceCategory> ResidenceCategories { get; set; }
    }
}
