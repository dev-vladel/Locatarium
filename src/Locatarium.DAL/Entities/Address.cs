using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locatarium.DAL.Entities
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [ForeignKey("Residence")]
        public int ResidenceId { get; set; }

        public Residence Residence { get; set; }
    }
}
