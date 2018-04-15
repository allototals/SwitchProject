using AppZoneUI.Framework;
using Switch.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Switch.Core.Model;
using AppZoneUI.Framework.Mods;
using Switch.Service;
using System.Linq.Expressions;

namespace Switch.UI
{
    public class ChannelsList : EntityUI<Switch.Core.Model.Channels>
    {
        //private Switch.Service.Interface.IChannels _channelService;
        private readonly IGenericService<Channels> _channelService = new GenericService<Channels>();

        public ChannelsList()
        {
            try
            {
               // var container = ServiceBootrapper.Initialise();
                //_channelService = container.Resolve<IChannels>();

                UseFullView();
                AddNorthSection()
                        .StretchFields(50)
                        .IsFramed()
                        .WithTitle("View Channel List")
                        .WithFields(
                        new List<IField>()
                {
                    Map(x => x.Name).AsSectionField<TextBox>().LabelTextIs("Name"),//Of<InstantIssuanceCardProfile>(CardProfiles).ListOf(x=>x.Name,x=>x.ID).LabelTextIs("Card Profile"),
                    Map(x=>x.Code).AsSectionField<TextBox>().LabelTextIs("Code"),
                    Map(x => x.Description).AsSectionField<TextArea>().LabelTextIs("Description"),

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

                List<Channels> xlist = null;

                HasMany(x => x.ChannelList).As<Grid>()
                    .ApplyMod<ExportMod>(x => x.ExportToCsv().ExportAllRows().SetFileName("export"))
                    .ApplyMod<ViewDetailsMod>(y => y.Popup<ChannelsUIEdit>("Edit Channels").PrePopulate<Channels, Channels>(x => { return x; }))
                    .Of<Channels>()
                    .WithRowNumbers()
                    .WithColumn(x => x.Name)
                    .WithColumn(x => x.Code)
                    .WithColumn(x => x.Description)
                    .IsPaged<Channels>(20, (x, y) =>
                    {
                        
                        Expression<Func<Channels, bool>> where = p => (
                           (string.IsNullOrEmpty(x.Name) ? true : p.Name.ToLower().Equals(x.Name.ToLower()))
                           &&
                           (string.IsNullOrEmpty(x.Code)) ? true : p.Code.ToLower().Equals(x.Code.ToLower())
                           &&
                            (string.IsNullOrEmpty(x.Description)) ? true : p.Description.ToLower().Equals(x.Description.ToLower())
                           );
                        var list = _channelService.FilterBy(where).ToList();
                            //z => z.Code.ToLower().Equals(code.ToLower()) || z.Name.ToLower().Equals(name.ToLower())).ToList();
                        x.ChannelList = list;
                       //  xlist =x
                         return x;

                        //return x;
                    });
                        

            }
            catch(Exception ex)
            {

            }



        }
    }
}
