using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Data.Models
{
    public class Schemes
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[ScaffoldColumn(false)]
        public virtual Guid Id { get; set; }
       // public TransactionChannelFees TransactionChannelFees { get; set; }
        [Required]
        public virtual TransactionType TransType { get; set; }
        [Required]
        public  virtual Channels Channel { get; set; }
        [Required]
        public virtual Fees Fees { get; set; }
        [Required]
        public virtual Routes Route { get; set; }
       
        //[DataType(DataType.Text)]
        //[MaxLength(20,ErrorMessage="The Name cannot be more than 20 characters.")]
        //[RegularExpression(@"^([A-Za-z]+)")]
        //[Display(Name = "Scheme Name")]
        //[Required]
        public virtual string Name { get; set; }
        //[DataType(DataType.Text)]
        //[MaxLength(500,ErrorMessage="The Description cannot be more than 500 characters.")]
       // [RegularExpression("[^A-Za-z]+")]
        public virtual string Description { get; set; }
        //[ScaffoldColumn(false)]
        public bool IsDeleted { get; set; }
    }
}
