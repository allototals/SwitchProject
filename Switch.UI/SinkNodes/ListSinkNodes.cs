using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using Switch.Core.Model;
using Switch.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Switch.UI
{
    public class ListSinkNodes:EntityUI<SinkNode>
    {
        public ListSinkNodes()
        {
            try
            {
                
                UseFullView();
                AddNorthSection()
                        .StretchFields(50)
                        .IsFramed()
                        .WithTitle("Sink Node List")
                        .WithFields(
                        new List<IField>()
                  {
                    Map(x => x.Name).AsSectionField<TextBox>().LabelTextIs("Name"),//Of<InstantIssuanceCardProfile>(CardProfiles).ListOf(x=>x.Name,x=>x.ID).LabelTextIs("Card Profile"),
                    Map(x=>x.HostName).AsSectionField<TextBox>().LabelTextIs("HostName"),
                    Map(x => x.IPAdress).AsSectionField<TextBox>().LabelTextIs("IP Address"),

                   // Map(x=> "Upload Format In CSV").AsSectionField<TextLabel>(),
                    // Map(x=> x.UploadHeader.Replace(",","<br/>")).AsSectionField<TextLabel>().LabelTextIs("Upload Header"),
                    //Map(x=> x.UploadFormat.Replace(",","<br/>")).AsSectionField<TextLabel>().LabelTextIs("Upload Format"),
                 AddSectionButton()
                .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.PackageGo))
                .WithText("Search")
                .UpdateWith(x =>
                {
                    return x;
                })
                });



                // Grid Section

                HasMany(x => x.SinkNodeList).As<Grid>()
                    .ApplyMod<ExportMod>(x => x.ExportToCsv().ExportAllRows().SetFileName("export"))
                   .ApplyMod<ViewDetailsMod>(y => y.Popup<Switch.UI.EditSinkNode>("Edit SourceNpde").PrePopulate<SinkNode, SinkNode>(x => { return x; }))
                    .Of<SinkNode>()
                    .WithRowNumbers()
                    .WithColumn(x => x.Name)
                    .WithColumn(x => x.HostName)
                    .WithColumn(x => x.IPAdress)
                    .WithColumn(x => x.Port)
                    .WithColumn(x => x.Status)

                    .IsPaged<SinkNode>(20, (x, y) =>
                    {
                        Expression<Func<SinkNode, bool>> where = p => (
                           (string.IsNullOrEmpty(x.Name) ? true : p.Name.ToLower().Equals(x.Name.ToLower()))
                           &&
                           (string.IsNullOrEmpty(x.HostName)) ? true : p.HostName.ToLower().Equals(x.HostName.ToLower())
                           && (x.Port == null) ? true : p.Port == x.Port
                           );

                        var name = string.IsNullOrEmpty(x.Name);
                       // var where= (x=>x.)
                        x.SinkNodeList = new GenericService<SinkNode>().FilterBy(where).ToList();
                            //z => z.HostName.ToLower().Equals(x.HostName.ToLower()) || z.Name.ToLower().Equals(x.Name.ToLower()) || z.Port== x.Port).ToList();
                        
                        //  xlist =x
                        return x;

                        //return x;
                    });


            }
            catch (Exception ex)
            {

            }
        }
    }
}
