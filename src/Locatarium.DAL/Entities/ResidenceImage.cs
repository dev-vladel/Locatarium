using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locatarium.DAL.Entities
{
    [Table("ResidenceImages")]
    public class ResidenceImage
    {
        [Key]
        public int Id { get; set; }

        public string ImageName { get; set; }

        [ForeignKey("Residence")]
        public int ResidenceId { get; set; }
    }
}
