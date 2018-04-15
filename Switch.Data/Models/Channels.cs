using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Data.Models
{
    public class Channels
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression("^([A-Za-z]+)$")]
        [Display(Name = "Channel Name")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression("^([0-9]+)$")]
        public string Code { get; set; }
       // [RegularExpression("^([A-Za-z]+)$")]
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public IList<Channels> ChannelList { get; set; }
    }
   
    

}
