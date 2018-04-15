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
    public class EditSourceNode:EntityUI<SourceNode>
    {
        private readonly IGenericService<SourceNode> _sourceNodeService = new GenericService<SourceNode>();
        string message = "";
         public EditSourceNode()
        {
            AddSection()
            .IsFramed()
            .WithTitle("Edit SourceNode")
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
                                .TextFormatIs(TextFormat.alphanumeric)
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
                               bool result = _sourceNodeService.Update(f);
                               return true;
                           }
                           catch (Exception ex)
                           {
                               message = ex.Message; ;
                               throw;
                           }
                      }) .WithPostback()
                    .ConfirmWith (s => String.Format("Update SourceNode {0} ", s.Name)).WithText("Update")
                    .OnSuccessDisplay(s => String.Format("Update Sourcenode {0} has been updated ", s.Name))
                    .OnFailureDisplay(s => String.Format("Error: SourceNode{0} was not updated ", message))
                    
              });
        }
    }
}
