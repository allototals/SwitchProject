using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Core.Model
{
    public class External:Entity
    {
        public virtual string Name { get; set; }
    }
    public class ExternalMap:ClassMap<External>
    {
        public ExternalMap()
        {
            Id(x => x.Id).GeneratedBy.GuidNative();
            Map(x=>x.Name);
        }
    }
}
