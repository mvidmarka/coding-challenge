using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Common.BindingModels
{
    public class PersonBindingModel : BaseBindingModel
    {
       
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Firstname")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Surname")]
        public string SurName { get; set; }

        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100)]
        [Phone]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        [Display(Name = "Position")]
        public string Position { get; set; }

        [Display(Name = "Company")]
        public Guid? CompanyId { get; set; }

        public string CompanyName { get; set; }

    }
}
