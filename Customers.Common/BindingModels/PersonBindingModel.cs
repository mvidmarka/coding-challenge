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
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string SurName { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100)]
        [Phone]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Position { get; set; }

    }
}
