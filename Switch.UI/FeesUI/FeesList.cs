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
    public class FeesList:EntityUI<Fees>
    {
        private readonly IGenericService<Fees> _feesService = new GenericService<Fees>();
        public FeesList()
        {
            try
            {
                // var container = ServiceBootrapper.Initialise();
                //_channelService = container.Resolve<IChannels>();

                UseFullView();
                AddNorthSection()
                        .StretchFields(50)
                        .IsFramed()
                        .WithTitle("View Fees List")
                        .WithFields(
                        new List<IField>()
                {
                    Map(x => x.Name).AsSectionField<TextBox>().LabelTextIs("Name"),//Of<InstantIssuanceCardProfile>(CardProfiles).ListOf(x=>x.Name,x=>x.ID).LabelTextIs("Card Profile"),
                    Map(x=>x.FlatAmount).AsSectionField<TextBox>().LabelTextIs("Flat Amount"),
                    Map(x => x.AmountPercent).AsSectionField<TextBox>().LabelTextIs("Percentage Amount"),

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

                HasMany(x => x.FeesList).As<Grid>()
                    .ApplyMod<ExportMod>(x => x.ExportToCsv().ExportAllRows().SetFileName("export"))
                   .ApplyMod<ViewDetailsMod>(y => y.Popup<EditFees>("Edit Fees").PrePopulate<Fees, Fees>(x => { return x; }))
                    .Of<Fees>()
                    .WithRowNumbers()
                    .WithColumn(x => x.Name)
                    .WithColumn(x => x.FlatAmount)
                    .WithColumn(x => x.AmountPercent)
                    .WithColumn(x=>x.MinimumAmount)
                    .WithColumn(x=>x.MaximumAmount)
                    
                    .IsPaged<Fees>(20, (x, y) =>
                    {

                        Expression<Func<Fees, bool>> where = p => (
                          (string.IsNullOrEmpty(x.Name) ? true : p.Name.ToLower().Equals(x.Name.ToLower()))
                          &&
                          (x.FlatAmount == null) ? true : p.FlatAmount == x.FlatAmount
                          &&
                           (x.AmountPercent == null) ? true : p.AmountPercent == x.AmountPercent
                          );
                        //var name = x.Name;
                        var list = _feesService.FilterBy(where).ToList();
                            //z => z.FlatAmount == x.FlatAmount || z.Name == x.Name ||z.AmountPercent==x.AmountPercent).ToList();
                        x.FeesList = list;
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
