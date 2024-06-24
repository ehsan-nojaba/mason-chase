using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Mc2.CrudTest.Presentation.Client.ViewModel
{
    public class CustomerUpdateViewModel
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(20, ErrorMessage = "First Name cannot be longer than 20 characters.", MinimumLength = 4)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(20, ErrorMessage = "Last Name cannot be longer than 20 characters.", MinimumLength = 4)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bank Account Number is required.")]
        [StringLength(18, ErrorMessage = "Bank Account Number cannot be longer than 18 characters.", MinimumLength = 16)]
        [Display(Name = "Bank Account Number")]
        public string BankAccountNumber { get; set; }
    }
}
