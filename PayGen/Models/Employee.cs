using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Models {
    public class Employee {
        public int Id { get; set; }

        [DisplayName("First Name")]
        [StringLength(50)]
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [StringLength(50)]
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [DisplayName("Annual Salary")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage ="Please enter a number greater than 0")]
        public int AnnualSalary { get; set; }

        [DisplayName("Super Rate")]
        [Range(0, 50, ErrorMessage = "Please enter a value between 0 and 50")]
        public decimal SuperRate { get; set; }

        [DisplayName("Pay Period")]
        [Required(ErrorMessage = "Pay Period is required.")]
        [BindProperty]
        public string PayPeriod { get; set; }

        public override bool Equals(object obj) {
            return Equals(obj as Employee);
        }

        private bool Equals(Employee other) {
            return other != null &&
                   FirstName == other.FirstName &&
                   LastName == other.LastName &&
                   AnnualSalary == other.AnnualSalary &&
                   SuperRate == other.SuperRate &&
                   PayPeriod == other.PayPeriod;
        }

        public override int GetHashCode() {
            return HashCode.Combine(FirstName, LastName, AnnualSalary, SuperRate, PayPeriod);
        }
    }
}
