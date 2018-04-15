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
    public class SourceNode:Entity
    {
        
       // public virtual Guid Id { get; set; }
       
        public virtual string Name { get; set; }
        //[Required]
        //[DataType(DataType.Text)]
        public virtual string HostName { get; set; }
        //[Required]
        public virtual string IPAdress { get; set; }
        //[Required]
        //[RegularExpression(@"^([0-9]+)")]
        public virtual int Port { get; set; }
        
        public virtual bool Status { get; set; }
       // [ScaffoldColumn(false)]
       
        public virtual bool IsDeleted { get; set; }
        public virtual string Err_Msg { get; set; }
        public virtual IList<SourceNode> SourceNodeList { get; set; }
       // [ScaffoldColumn(false)]
        public virtual NodeType NodeType
        {
            
            get
            {
                return NodeType.Server;
            }
        }

    }
    public class SourceNodeMap : ClassMap<SourceNode>
    {
        public SourceNodeMap()
        {

            Table("SourceNode");
            Id(x => x.Id).GeneratedBy.GuidNative();
            Map(x => x.Name);
            Map(x => x.HostName);
            Map(x => x.IPAdress);
            Map(x => x.Port);
            Map(x => x.Status);
            Map(x => x.IsDeleted);
            
        }
    }
   
   
    

    
}
