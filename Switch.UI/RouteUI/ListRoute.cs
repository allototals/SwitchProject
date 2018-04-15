using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
//using Ext.Net;
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
    public class ListRoute : EntityUI<Routes>
    {
        private readonly IGenericService<Routes> _routesService = new GenericService<Routes>();
        public ListRoute()
        {
            try
            {
                // var container = ServiceBootrapper.Initialise();
                //_channelService = container.Resolve<IChannels>();

                UseFullView();
                AddNorthSection()
                        .StretchFields(50)
                        .IsFramed()
                        .WithTitle("View Route List")
                        .WithFields(
                        new List<IField>()
                {
                    Map(x => x.Name).AsSectionField<TextBox>().LabelTextIs("Name"),//Of<InstantIssuanceCardProfile>(CardProfiles).ListOf(x=>x.Name,x=>x.ID).LabelTextIs("Card Profile"),
                    Map(x=>x.CardPAN).AsSectionField<TextBox>().LabelTextIs("Card PAN"),
                    //Map(x => x.SinkNode.Name).AsSectionField<TextBox>().LabelTextIs("Sink Node"),
                    Map(x=>x.Description).AsSectionField<TextBox>().LabelTextIs("Description"),

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

                // var list = _channelService.GetQueryable(x => x.Id != null).ToList();

                // List<Channels> xlist = null;

                // Grid Section

                HasMany(x => x.RoutesList).As<Grid>()
                    .ApplyMod<ExportMod>(x => x.ExportToCsv().ExportAllRows().SetFileName("export"))
                   .ApplyMod<ViewDetailsMod>(u => u.Popup<EditRoute>("Edit Routes")
                       .PrePopulate<Routes, Routes>(x => { return x; }))
                    .Of<Routes>()
                    .WithRowNumbers()
                    .WithColumn(x => x.Name)
                    .WithColumn(x => x.CardPAN)
                    .WithColumn(x => x.SinkNode.Name)
                    .WithColumn(x => x.Description)
                    

                    .IsPaged<Routes>(20, (x, y) =>
                    {
                        Expression<Func<Routes, bool>> where = p => (
                          (string.IsNullOrEmpty(x.Name) ? true : p.Name.ToLower().Equals(x.Name.ToLower()))
                          &&
                          (string.IsNullOrEmpty(x.CardPAN) ? true : p.CardPAN.ToLower().Equals(x.CardPAN.ToLower()))
                          &&
                           (string.IsNullOrEmpty(x.Description) ? true : p.Description.ToLower().Equals(x.Description.ToLower()))
                          );

                       // var name = x.Name;
                        var list = _routesService.FilterBy(where).ToList();
                            //z => z.CardPAN == x.CardPAN|| z.Name == x.Name).ToList();
                        x.RoutesList = list;
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
