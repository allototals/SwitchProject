using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Data.Models
{
    public  class SinkNode
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(40, ErrorMessage="Allowed character length is 40.")]
        [RegularExpression("[^A-Za-z]+")]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        [Required]
        public string HostName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string IPAdress { get; set; }
        [Required]
        [RegularExpression("[^0-9]+")]
        //[MaxLength(5,ErrorMessage="Port cannot be more than 5 digits.")]
        public int Port { get; set; }
        public bool Status { get; set; }
        [ScaffoldColumn(false)]
        public bool IsDeleted { get; set; }
        public NodeType NodeType
        {
            get
            {
                return NodeType.Client;
            }
        }
    }
}
