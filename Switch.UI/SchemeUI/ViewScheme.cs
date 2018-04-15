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
    public class ViewScheme:EntityUI<Schemes>
    {
        public ViewScheme()
        {
            UseFullView();
            WithTitle("Scheme Details");
            AddSection()
                .IsFormGroup()
                .WithColumns(new List<Column>()
                    {
                        new Column( new List<IField>()
                        {
                             Map(x => x.Name).AsSectionField<TextLabel>().LabelTextIs("Name"),
                             Map(x => x.Route.Name).AsSectionField<TextLabel>().LabelTextIs("Route"),
                             Map(x => x.Description).AsSectionField<TextLabel>().LabelTextIs("Description"),
                             AddSectionButton().WithText("Edit").ApplyMod<ButtonPopupMod>(x => x.Popup<EditScheme>("Edit").PrePopulate<Schemes, Schemes>(y => y)),

                             
                           
                        }),
                    });
            AddSection().IsFramed().IsCollapsible()
           .WithColumns(new List<Column>()
            {
                new Column(new List<IField>()
                {
                         HasMany(x => x.SchemesList)
                        .AsSectionField<Grid>() 
                        .Of<Schemes>()
                        //.WithColumn(x => x.Channel.Name)
                        .WithColumn(x => x.Channel.Name,"Channel")
                        .WithColumn(x => x.TransType.Name,"Type")
                        .WithColumn(x => x.Fees.Name,"Fee")
                        .WithRowNumbers()
                        .IsPaged<Schemes>(10, (x, e) =>
                        {
                            x.SchemesList = new GenericService<Schemes>().SelectAll();
                            return x;
                         })
                         //.ApplyMod<ViewDetailsMod>(y => y.Popup<SchemeComboDetails>("View Details"))
                })
            });
        }
    }
}
