using Locatarium.Models;
using System.ComponentModel.DataAnnotations;

namespace Locatarium.Web.ViewModels
{
    public class UserViewModel
    {
        [MaxLength(32, ErrorMessage = "The first name is too long")]
        public string FirstNameVM { get; set; }

        [MaxLength(32, ErrorMessage = "The last name is too long")]
        public string LastNameVM { get; set; }

        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$", ErrorMessage = "Password must contain at least 8 characters, one letter and one number")]
        [MaxLength(16, ErrorMessage = "Password cannot be longer than 16 characters")]
        [DataType(DataType.Password)]
        public string PasswordVM { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPasswordVM { get; set; }

        [RegularExpression("(^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$)", ErrorMessage = "This is not a valid email adress!")]
        [MaxLength(255, ErrorMessage = "The email is too long")]
        [DataType(DataType.EmailAddress)]
        public string EmailVM { get; set; }

        [RegularExpression("^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\\s\\./0-9]*$", ErrorMessage = "This is not a valid phone number!")]
        [MaxLength(15, ErrorMessage = "This is not a valid phone number!")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneVM { get; set; }
    }
}
