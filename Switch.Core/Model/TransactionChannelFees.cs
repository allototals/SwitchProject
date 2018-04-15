using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Data.Models
{
   public class TransactionChannelFees
    {
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public Guid Id { get; set; }
       public TransactionType TransType { get; set; }
       public Channels Channel { get; set; }
       public Fees Fee { get; set; }
       public string Name
       {
           get
           {
               return TransType.Name + " |" + Channel.Name + "| " + Fee.Name;
           }
       }
    }
}
