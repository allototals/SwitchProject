using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Data.Models
{
    public class TransactionType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Text,ErrorMessage="Only character strings is allowed.")]
        [MaxLength(20, ErrorMessage = "Name field cannot be more than 20 digits.")]
        [RegularExpression(@"^([0-9]+)",ErrorMessage="The name field is invalid")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        //[MaxLength(10,ErrorMessage="Code Length cannot be more than 10 digits.")]
        [RegularExpression("^([0-9]+)",ErrorMessage="Invalid Code ")]
        public string Code { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(500, ErrorMessage = "Description Field cannot be more than 500 characters.")]
        public string Description { get; set; }
        [ScaffoldColumn(false)]
        public bool IsDeleted { get; set; }
         
    }
}
