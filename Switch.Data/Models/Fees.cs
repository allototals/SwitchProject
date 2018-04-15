using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Data.Models
{
    public class Fees
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^([A-Za-z]+)$")]
        [Display(Name="Fee Name")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal FlatAmount { get; set; }
        [Range(0,100)]
        public int Percent { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Maximum { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Minimum { get; set; }
        [ScaffoldColumn(false)]
        public bool IsDeleted { get; set; }
    }
}
