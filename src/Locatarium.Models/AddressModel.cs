using System.ComponentModel.DataAnnotations;

namespace Locatarium.Models
{
    public class AddressModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This is field is required!")]
        [MaxLength(255, ErrorMessage = "The street address is too long.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "This is field is required!")]
        [MaxLength(255, ErrorMessage = "The city name is too long.")]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Required(ErrorMessage = "This is field is required!")]
        [MaxLength(255, ErrorMessage = "The state name is too long.")]
        [DataType(DataType.Text)]
        public string State { get; set; }

        [Required(ErrorMessage = "This is field is required!")]
        [MaxLength(255, ErrorMessage = "The title is too long.")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
    }
}