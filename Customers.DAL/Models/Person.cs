using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.DAL.Models
{
    [Table("Person")]
    public class Person : BaseDBModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string SurName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Position { get; set; }

        [ForeignKey("Company")]
        public Guid? CompanyId { get; set; }

        public virtual Company Company {get; set;}
    }
}
