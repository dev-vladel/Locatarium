using System.ComponentModel.DataAnnotations.Schema;

namespace Locatarium.DAL.Entities
{
    [Table("ResidenceCategories")]
    public class ResidenceCategory
    {
        [ForeignKey("Residence")]
        public int ResidenceId { get; set; }

        public Residence Residence { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
