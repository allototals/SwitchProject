using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Data.Models
{
    public class SourceNode
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^([A-Za-z]+)")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string HostName { get; set; }
        [Required]
        public string IPAdress { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]+)")]
        public int Port { get; set; }
        
        public bool Status { get; set; }
        [ScaffoldColumn(false)]
       
        public bool IsDeleted { get; set; }
        [ScaffoldColumn(false)]
        public NodeType NodeType
        {
            
            get
            {
                return NodeType.Server;
            }
        }

    }
    //public enum NodeType
    //{
    //    Client=1,
    //    Server=2     
    //}
   
    

    
}
