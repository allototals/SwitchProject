using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Core.Model
{
    public class TransactionType:Entity
    {
        
       // public virtual Guid Id { get; set; }
       
        public virtual string Name { get; set; }
      
        public virtual string Code { get; set; }
       
        public virtual string Description { get; set; }
       
        public virtual bool IsDeleted { get; set; }
        public virtual string Err_Msg { get; set; }
        public virtual IList<TransactionType> TransactionTpeList { get; set; }
         
    }
    public class TransactionTypeMap:ClassMap<TransactionType>
    {
        TransactionTypeMap()
        {
            Table("TransType");
            Id(x => x.Id).GeneratedBy.GuidNative();
            Map(x => x.Name);
            Map(x => x.Code);
            Map(x => x.Description);
            Map(x => x.IsDeleted);
        }
    }
}
