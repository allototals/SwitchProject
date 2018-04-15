using AppZoneUI.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switch.Core.Model;
using System.IO;
using AppZoneUI.Framework.Mods;
using Switch.Service;

namespace Switch.UI
{
    public class ChannelsUIEdit : EntityUI<Channels>
    {
        private IGenericService<Channels> _channelsService = new GenericService<Channels>();
        string message = "";
        public ChannelsUIEdit()
       {
             AddSection()
             .IsFramed()
             .WithTitle("Edit Channels")
             .WithColumns(new List<Column> 
            { 
                new Column(  
                     new List<IField> 
                     { 
                            Map(x => x.Name)
                                .AsSectionField<TextBox>()
                                .Required()
                                .WithLength(15),
                            Map(x => x.Code)
                                .AsSectionField<TextBox>()
                                .Required()
                                .TextFormatIs(TextFormat.numeric)
                                .WithLength(15),
                            Map(x => x.Description)
                                .AsSectionField<TextArea>()
                                .Required()
                                .TextFormatIs(TextFormat.alphanumeric)
                    //        Map(x => x.Maximum)
                    //            .AsSectionField<TextBox>()
                    //            .Required()
                    //            .TextFormatIs(TextFormat.numeric)
                    //            .WithLength(15),
                    //        Map(x => x.Minimum)
                    //            .AsSectionField<TextBox>()
                    //            .Required()
                    //            .TextFormatIs(TextFormat.numeric)
                    //}),  
            })
            })

            .WithFields(new List<IField>{
                   AddSectionButton()
                       .SubmitTo(f=> 
                       {
                           try
                           {
                               bool result = _channelsService.Update(f);
                               return true;
                           }
                           catch (Exception ex)
                           {
                               message = ex.Message; 
                               throw;
                           }
                      }) 
                    .ConfirmWith (s => String.Format("Update Channels {0} ", s.Name)).WithText("Update")
                    .OnSuccessDisplay(s => String.Format("Update Channels {0} has been updated ", s.Name))
                    .OnFailureDisplay(s => String.Format("Error: Channels {0} was not updated ", message))
                    
              });
          }
        
       
    }
}
