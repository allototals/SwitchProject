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
    public class AddScheme:EntityUI<Schemes>
    {
        private readonly IGenericService<Schemes> _schemeService = new GenericService<Schemes>();
        private readonly IGenericService<Channels> _channelsService = new GenericService<Channels>();
        private readonly IGenericService<Fees> _feesService = new GenericService<Fees>();
        private readonly IGenericService<TransactionType> _transService = new GenericService<TransactionType>();
        private readonly IGenericService<Routes> _routeService = new GenericService<Routes>();
        string message = "";
        public AddScheme()
        {
            UseFullView();
            AddNorthSection()
                    .StretchFields(50)
                    .IsFramed()
                    .WithTitle("Add Scheme")
                    .WithFields(
                    new List<IField>()
                {
                    Map(x => x.Name).AsSectionField<TextBox>().LabelTextIs("Name"),//Of<InstantIssuanceCardProfile>(CardProfiles).ListOf(x=>x.Name,x=>x.ID).LabelTextIs("Card Profile"),
                    Map(x=>x.Description).AsSectionField<TextArea>().LabelTextIs("Description"),
                     Map(x => x.Channel).AsSectionField<DropDownList>().Required().Of(_channelsService.SelectAll()).ListOf(x=>x.Name,x=>x.Id),
                    Map(x=>x.Fees).AsSectionField<DropDownList>().Required().Of(_feesService.SelectAll()).ListOf(x=>x.Name,x=>x.Id).LabelTextIs("Fees"),
                    Map(x=>x.TransType).AsSectionField<DropDownList>().Required().Of(_transService.SelectAll()).ListOf(x=>x.Name,x=>x.Id).LabelTextIs("Transaction type"),   
                    Map(x=>x.Route).AsSectionField<DropDownList>().Required().Of(_routeService.SelectAll()).ListOf(x=>x.Name,x=>x.Id).LabelTextIs("Route"),

                   // Map(x => x.).AsSectionField<CheckBox>().LabelTextIs("Status"),

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
                        _schemeService.Create(x);
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
                        x.Err_Msg = string.Format("An error occured while saving SourceNode: {0}", ex.Message);
                        result = false;
                    }
                    return result;
                })
                .OnFailureDisplay(x => x.Err_Msg)//"Batch Upload Failed! Please check the file and try again.")
                .OnSuccessRedirectTo("SourceNodeList.aspx",x=>x.Id)
                .OnSuccessDisplay("SourceNode successfully created")
             });
        }
    }
}
