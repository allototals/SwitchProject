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
    public class ListTransType:EntityUI<TransactionType>
    {
        public ListTransType()
        {
            try
            {

                UseFullView();
                AddNorthSection()
                        .StretchFields(50)
                        .IsFramed()
                        .WithTitle("Transaction Type List")
                        .WithFields(
                        new List<IField>()
                  {
                    Map(x => x.Name).AsSectionField<TextBox>().LabelTextIs("Name"),//Of<InstantIssuanceCardProfile>(CardProfiles).ListOf(x=>x.Name,x=>x.ID).LabelTextIs("Card Profile"),
                    Map(x=>x.Code).AsSectionField<TextBox>().LabelTextIs("Code"),
                    Map(x => x.Description).AsSectionField<TextBox>().LabelTextIs("Description"),

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

                HasMany(x => x.TransactionTpeList).As<Grid>()
                    .ApplyMod<ExportMod>(x => x.ExportToCsv().ExportAllRows().SetFileName("export"))
                   .ApplyMod<ViewDetailsMod>(y => y.Popup<Switch.UI.EditTransType>("Edit Transaction Type").PrePopulate<TransactionType, TransactionType>(x => { return x; }))
                    .Of<TransactionType>()
                    .WithRowNumbers()
                    .WithColumn(x => x.Name)
                    .WithColumn(x => x.Code)
                    .WithColumn(x => x.Description)
                   // .WithColumn(x => x.Port)
                    //.WithColumn(x => x.Status)

                    .IsPaged<TransactionType>(20, (x, y) =>
                    {
                        Expression<Func<TransactionType, bool>> where = p => (
                          (string.IsNullOrEmpty(x.Name) ? true : p.Name.ToLower().Equals(x.Name.ToLower()))
                          &&
                          (string.IsNullOrEmpty(x.Code)) ? true : p.Code.ToLower().Equals(x.Code.ToLower())
                          &&
                           (string.IsNullOrEmpty(x.Description)) ? true : p.Description.ToLower().Equals(x.Description.ToLower())
                          );
                       // var name = x.Name;
                        x.TransactionTpeList = new GenericService<TransactionType>().FilterBy(where).ToList();
                            //z => z.Code == x.Code|| z.Name == x.Name ).ToList();

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
