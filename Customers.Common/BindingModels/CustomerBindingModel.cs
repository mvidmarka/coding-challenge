using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Common.BindingModels
{
    public class CustomerBindingModel : BaseBindingModel
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }

        //to distinct company from person
        [Required]
        public bool IsCompany { get; set; }

    }
}
