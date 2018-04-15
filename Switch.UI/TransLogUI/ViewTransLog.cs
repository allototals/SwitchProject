using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using Switch.Core.Model;
using Switch.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.UI.TransLogUI
{
    public class ViewTransLog:EntityUI<TransLogs>
    {
        public ViewTransLog()
        {
            UseFullView();
            WithTitle("Scheme Details");
            AddSection()
                .IsFormGroup()
                .WithColumns(new List<Column>()
                    {
                        new Column( new List<IField>()
                        {
                             Map(x => x.Amount).AsSectionField<TextLabel>().LabelTextIs("Amount"),
                             Map(x => x.CardPAN).AsSectionField<TextLabel>().LabelTextIs("Card PAN"),
                            // Map(x => x.Description).AsSectionField<TextLabel>().LabelTextIs("Description"),
                           //  AddSectionButton().WithText("Edit").ApplyMod<ButtonPopupMod>(x => x.Popup<EditScheme>("Edit").PrePopulate<Schemes, Schemes>(y => y)),

                             
                           
                        }),
                    });
            AddSection().IsFramed().IsCollapsible()
           .WithColumns(new List<Column>()
            {
                new Column(new List<IField>()
                {
                         HasMany(x => x.TransLogList)
                        .AsSectionField<Grid>() 
                        .Of<TransLogs>()
                        //.WithColumn(x => x.Channel.Name)
                        .WithColumn(x => x.Amount,"Amount")
                        .WithColumn(x => x.CardPAN,"Card PAN")
                        //.WithColumn(x => x.,"Fee")
                        .WithRowNumbers()
                        .IsPaged<TransLogs>(10, (x, e) =>
                        {
                            x.TransLogList = new GenericService<TransLogs>().SelectAll();
                            return x;
                         })
                         //.ApplyMod<ViewDetailsMod>(y => y.Popup<SchemeComboDetails>("View Details"))
                })
            });
        }
    }
}
