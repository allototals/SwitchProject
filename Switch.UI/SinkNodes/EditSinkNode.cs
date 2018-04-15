using AppZoneUI.Framework;
using Switch.Core.Model;
using Switch.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.UI
{
    public class EditSinkNode:EntityUI<SinkNode>
    {
        public EditSinkNode()
        {
            string message = "";
            AddSection()
            .IsFramed()
            .WithTitle("Edit Sink Node")
            .WithColumns(new List<Column> 
            { 
                new Column(  
                     new List<IField> 
                     { 
                            Map(x => x.Name)
                                .AsSectionField<TextBox>()
                                .Required()
                                .WithLength(15),
                             Map(x => x.HostName)
                                .AsSectionField<TextBox>()
                                .Required()
                                .TextFormatIs(TextFormat.alphanumeric)
                                .WithLength(15),
                            Map(x => x.IPAdress)
                                .AsSectionField<TextBox>()
                                .Required()
                                .WithLength(15),
                            Map(x => x.Port)
                                .AsSectionField<TextBox>()
                                .Required()
                                .TextFormatIs(TextFormat.numeric),
                           
                            Map(x => x.Status)
                                .AsSectionField<CheckBox>()
                                .Required()               
                            })
                         })
                .WithFields(new List<IField>{
                   AddSectionButton()
                       .SubmitTo(f=> 
                       {
                           try
                           {
                               bool result = new GenericService<SinkNode>().Update(f);
                               return true;
                           }
                           catch (Exception ex)
                           {
                               message = ex.Message; 
                               throw;
                           }
                      }) .WithPostback()
                    .ConfirmWith (s => String.Format("Update SinkNode {0} ", s.Name)).WithText("Update")
                    .OnSuccessDisplay(s => String.Format("Update SinkNode {0} has been updated ", s.Name))
                    .OnFailureDisplay(s => String.Format("Error: SinkNode{0} was not updated ", message))
                    
              });
        }
    }
}
