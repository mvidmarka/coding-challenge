using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.DAL.Models
{
    public class BaseDBModel
    {
        [Required]
        public DateTime DateEdited { get; set; }
    }
}
