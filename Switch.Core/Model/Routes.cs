using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;

namespace Switch.Core.Model
{
    public class Routes:Entity
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[ScaffoldColumn(false)]
       // public virtual Guid Id { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[RegularExpression("[^A-Za-z]+")]
        //[Display(Name = "Route Name")]
        public virtual string Name { get; set; }
        //[Required]
        //[MaxLength(19)]
        //[RegularExpression("[^0-9]+")]
        public virtual string CardPAN { get; set; }

       // [RegularExpression("[^A-Za-z]+")]
        public virtual string Description { get; set; }
       // [Required]
        public virtual SinkNode SinkNode { get; set; }
        //[ScaffoldColumn(false)]
        public virtual bool IsDeleted { get; set; }
        public virtual string Err_Msg { get; set; }
        public virtual IList<Routes> RoutesList { get; set; }
    }


    public class RoutesMap : ClassMap<Routes>
    {
        public RoutesMap()
        {
            
            Table("Routes");
            Id(x => x.Id).GeneratedBy.GuidNative();
            Map(x => x.Name);
            References<SinkNode>(x => x.SinkNode);
            Map(x => x.IsDeleted);
            Map(x => x.Description);
        }
    }
}
