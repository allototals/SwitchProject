using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using Switch.Core.Model;
using Switch.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.UI
{
    public class ViewFees : EntityUI<Fees>
    {
        private readonly IGenericService<Fees> _feesService = new GenericService<Fees>();
         public ViewFees()
        {
            UseFullView();
            WithTitle("View Fee Details");
            AddSection()
                .IsFormGroup()
                .WithColumns(new List<Column>()
                    {
                        new Column( new List<IField>()
                        {
                             Map(x => x.Name).AsSectionField<TextLabel>(),
                             Map(x => x.FlatAmount).AsSectionField<TextLabel>().LabelTextIs("Flat Amount"),
                             Map(x => x.AmountPercent).AsSectionField<TextLabel>().LabelTextIs("Percent of Transaction"),
                             Map(x => x.MaximumAmount).AsSectionField<TextLabel>(),
                             Map(x => x.MinimumAmount).AsSectionField<TextLabel>(),
                        }
                        ),
                    });
            AddButton().WithText("Edit Fee")
            .ApplyMod<ButtonPopupMod>(x => x
            .Popup<EditFees>("Edit Fee").PrePopulate<EditFees,EditFees>(z=>z));
           
        }
    }
}
