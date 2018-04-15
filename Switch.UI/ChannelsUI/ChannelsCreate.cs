using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using System.IO;
using AppZoneUI.Framework.Mods;
using Switch.Core.Model;
using Switch.Service.Services;
using Switch.Service.Interface;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;
using Switch.Service;


namespace Switch.UI
{
    public  class ChannelsCreate : EntityUI<Channels>
    {
        //private Switch.Service.Interface.IChannels _channelService;
        private readonly IGenericService<Channels> _channelService = new GenericService<Channels>();
        public  ChannelsCreate()
        {
     
            UseFullView();
            AddNorthSection()
                    .StretchFields(50)
                    .IsFramed()
                    .WithTitle("Create a channel")
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
                         _channelService.Create(new Channels() { Code=x.Code, Name=x.Name,Description=x.Description });
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
                       // x.Err_Msg = string.Format("An error occured while reading the file: {0}", ex.Message);
                        result = false;
                    }
                    return result;
                })
               // .OnFailureDisplay(x => x.Err_Msg)//"Batch Upload Failed! Please check the file and try again.")
                .OnSuccessRedirectTo("channelList.aspx")
                .OnSuccessDisplay("Channels successfully created")
             });
        }
    }
}
