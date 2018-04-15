using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using Switch.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.UI
{
    public class ViewRoute:EntityUI<Routes>
    {
        public  ViewRoute()
        {
            UseFullView();
            WithTitle("View Route Details");
            AddSection()
                .IsFormGroup()
                .WithColumns(new List<Column>()
                    {
                        new Column( new List<IField>()
                        {
                             Map(x => x.Name).AsSectionField<TextLabel>().LabelTextIs("Name"),
                             Map(x => x.CardPAN).AsSectionField<TextLabel>().LabelTextIs("Card PAN"),
                             Map(x => x.Description).AsSectionField<TextLabel>().LabelTextIs("Description"),
                             Map(x => x.SinkNode.Name).AsSectionField<TextLabel>()
                            // Map(x => x.MinimumAmount).AsSectionField<TextLabel>(),
                        }),
                    });
            AddButton().WithText("Edit Routes")
            .ApplyMod<ButtonPopupMod>(x => x
            .Popup<EditRoute>("Edit Route").PrePopulate<EditRoute, EditRoute>(z => z));
        }
    }
}
