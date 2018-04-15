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
    public class Fees:Entity
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public virtual Guid Id { get; set; }
       
        public virtual string Name { get; set; }
        
        public virtual float FlatAmount { get; set; }
        
        public virtual int AmountPercent { get; set; }
      
        public virtual float MaximumAmount { get; set; }
       
        public virtual float MinimumAmount { get; set; }
      
        public virtual bool IsDeleted { get; set; }
        public virtual string Err_Msg { get; set; }
        public virtual IList<Fees> FeesList { get; set; }
    }
    public class FeesMap : ClassMap<Fees>
    {
        public FeesMap()
        {
            //Schema("DataBaseObject");
            Table("Fees");
            Id(x => x.Id).GeneratedBy.GuidNative();
            Map(x => x.Name);
            Map(x => x.FlatAmount);
            Map(x => x.MaximumAmount);
            Map(x => x.MinimumAmount);
            Map(x => x.AmountPercent);
            Map(x => x.IsDeleted);
        }
    }
}
