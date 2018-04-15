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
    public class Channels:Entity
    {
        
       // public virtual Guid Id { get; set; }
       
        public virtual string Name { get; set; }
        //[Required]
        //[DataType(DataType.Text)]
        //[RegularExpression("^([0-9]+)$")]
        public virtual string Code { get; set; }
       // [RegularExpression("^([A-Za-z]+)$")]
        public virtual string Description { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual string Err_Msg { get; set; }
        public  virtual IList<Channels> ChannelList { get; set; }
    }
    public class ChannelsMap : ClassMap<Channels>
    {
        public ChannelsMap()
        {
            //Schema("DataBaseObject");
            Table("Channels");
            Id(x => x.Id).GeneratedBy.GuidNative();
            Map(x => x.Name);
            Map(x => x.Code);
            Map(x => x.Description);
        }
    }
   
    

}
