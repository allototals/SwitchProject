using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using System.IO;
using AppZoneUI.Framework.Mods;
using Switch.Core.Model;
using Switch.Service;

namespace Switch.UI
{
    public class AddFees:EntityUI<Fees>
    {
    
        private readonly IGenericService<Fees> _feesService = new GenericService<Fees>();
        public AddFees()
        {
     
            UseFullView();
            AddNorthSection()
                    .StretchFields(50)
                    .IsFramed()
                    .WithTitle("Add Fees")
                    .WithFields(
                    new List<IField>()
                {
                    Map(x => x.Name).AsSectionField<TextBox>().LabelTextIs("Name"),//Of<InstantIssuanceCardProfile>(CardProfiles).ListOf(x=>x.Name,x=>x.ID).LabelTextIs("Card Profile"),
                    Map(x=>x.FlatAmount).AsSectionField<TextBox>().LabelTextIs("Flat Amount"),
                    Map(x=>x.AmountPercent).AsSectionField<TextBox>().LabelTextIs("Percent"),
                    Map(x=>x.MinimumAmount).AsSectionField<TextBox>().LabelTextIs("Minimum"),

                    Map(x => x.MaximumAmount).AsSectionField<TextBox>().LabelTextIs("Maximum"),

                   // Map(x=> "Upload Format In CSV").AsSectionField<TextLabel>(),
                    // Map(x=> x.UploadHeader.Replace(",","<br/>")).AsSectionField<TextLabel>().LabelTextIs("Upload Header"),
                    //Map(x=> x.UploadFormat.Replace(",","<br/>")).AsSectionField<TextLabel>().LabelTextIs("Upload Format"),
                 AddSectionButton()
                .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.PackageGo))
                .WithText("Create")
                .SubmitTo(x =>
                {
                    //if (user == null)
                    //{
                    //    Ext.Net.ExtNet.AddScript("alert('{0}');", string.Format("Sorry your session has expired, please refresh"));
                    //    return false;
                    //}

                    bool result = true;
                    try
                    {
                         _feesService.Create(new Fees(){MaximumAmount =x.MaximumAmount, MinimumAmount =x.MinimumAmount , FlatAmount =x.FlatAmount ,  AmountPercent =x.AmountPercent , Name =x.Name  });
                        //Stream fs = x.TheFile.InputStream;
                        
                        //UploadDownloadLimit upd = new UploadDownloadLimitSystem().GetSingle();
                        //bool isExceeded = new BatchIssuanceSystem().IsMaxUpLoadExceeded(fs, upd);
                        //if (isExceeded == false)
                        //{                            
                        //    Ext.Net.ExtNet.AddScript("alert('{0}');", string.Format("Sorry you have exceeded the batch upload limit of {0}, Try again",
                        //        upd != null ? upd.UploadLimit.ToString() : System.Configuration.ConfigurationManager.AppSettings["MaxRecordsinFileUpload"]));

                        //    result = false;
                        //}
                        //else
                        //{
                        //    //bool IsAffiliate = false;
                        //    //fs.Position = 0;
                        //    //if (user1.Role.UserCategory == PANE.Framework.Functions.DTO.UserCategory.Mfb)
                        //    //{
                        //    //    IsAffiliate = true;
                        //    //}
                        //    //new BatchIssuanceSystem().LinkCardsToAccounts(fs, x.CardProfile, user, IsAffiliate);
                        //}
                    }
                    catch (Exception ex)
                    {
                       // PANE.ERRORLOG.ErrorLogger.Log(ex); 
                        x.Err_Msg = string.Format("An error occured while saving fees: {0}", ex.Message);
                        result = false;
                    }
                    return result;
                })
                .OnFailureDisplay(x => x.Err_Msg)//"Batch Upload Failed! Please check the file and try again.")
                .OnSuccessRedirectTo("FeesEdit.aspx",x=>x.Id)
                .OnSuccessDisplay("Fees successfully created")
             });
        }
    }
}
