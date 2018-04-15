using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Core.Model
{
    public class TransLogs:Entity
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       // public virtual Guid Id { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual string CardPAN { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual string Err_Msg { get; set; }
        public virtual IList<TransLogs> TransLogList { get; set; }
    }
    public class TransLogMap:ClassMap<TransLogs>
    {
        public TransLogMap()
        {
            Table("TransLogs");
            Id(x => x.Id).GeneratedBy.GuidNative();
            Map(x => x.Amount);
            Map(x => x.CardPAN);
            Map(x => x.IsDeleted);

        }
    }
}
