using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Locatarium.Models
{
    public class ResidenceModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This is field is required!")]
        [MaxLength(255, ErrorMessage = "The title is too long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This is field is required!")]
        [Range(1, 9999999, ErrorMessage = "The value is either too low or too high.")]
        public int Price { get; set; }

        public AddressModel Address { get; set; }

        public List<string> Images { get; set; }

        public int UserId { get; set; }

        public UserModel UserDetails { get; set; }

        public List<int> ResidenceCategoryId { get; set; }

        public List<CategoryModel> ResidenceCategories { get; set; }

        public double RatingAverage { get; set; }

        public int RatingTotalNumber { get; set; }
    }
}
