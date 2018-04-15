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
    public  class SinkNode:Entity
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[ScaffoldColumn(false)]
       // public virtual Guid Id { get; set; }
        //[Required]
        //[DataType(DataType.Text)]
        //[MaxLength(40, ErrorMessage="Allowed character length is 40.")]
        //[RegularExpression("[^A-Za-z]+")]
        public virtual string Name { get; set; }
        //[DataType(DataType.Text)]
        //[Required]
        public virtual string HostName { get; set; }
        //[Required]
        //[DataType(DataType.Text)]
        public virtual string IPAdress { get; set; }
        //[Required]
        //[RegularExpression("[^0-9]+")]
        //[MaxLength(5,ErrorMessage="Port cannot be more than 5 digits.")]
        public virtual int Port { get; set; }
        public virtual bool Status { get; set; }
       // [ScaffoldColumn(false)]
        public  virtual bool IsDeleted { get; set; }
        public virtual string Err_Msg { get; set; }
        public virtual IList<SinkNode> SinkNodeList { get; set; }
        public virtual NodeType NodeType
        {
            get
            {
                return NodeType.Client;
            }
        }
    }
    public class SinkNodeMap : ClassMap<SinkNode>
    {
        public SinkNodeMap()
        {

            Table("SinkNode");
            Id(x => x.Id).GeneratedBy.GuidNative();
            Map(x => x.Name);
            Map(x => x.HostName);
            Map(x => x.IPAdress);
            Map(x => x.Port);
            Map(x => x.Status);
            Map(x => x.IsDeleted);
           // Map(x => x.Description);
        }
    }
}
