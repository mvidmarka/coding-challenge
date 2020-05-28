using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Common.BindingModels
{
    public class CompanyBindingModel : BaseBindingModel
    {
        public Guid Id { get; set; }

        [Required]
        public string CompanyName { get; set; }
    }
}
