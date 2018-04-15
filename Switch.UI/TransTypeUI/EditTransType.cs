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
    public class EditTransType:EntityUI<TransactionType>
    {
        public EditTransType()
        {
            string message = "";
            AddSection()
            .IsFramed()
            .WithTitle("Edit Transaction Type")
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
                                .WithLength(5),
                            Map(x => x.Description)
                                .AsSectionField<TextArea>()
                                .Required()
                                .TextFormatIs(TextFormat.alphanumeric)
                               // .WithLength(15),
                            //Map(x => x.Port)
                            //    .AsSectionField<TextBox>()
                            //    .Required()
                            //    .TextFormatIs(TextFormat.numeric),
                           
                            //Map(x => x.Status)
                            //    .AsSectionField<CheckBox>()
                            //    .Required()               
                            })
                         })
                .WithFields(new List<IField>{
                   AddSectionButton()
                       .SubmitTo(f=> 
                       {
                           try
                           {
                               bool result = new GenericService<TransactionType>().Update(f);
                               return true;
                           }
                           catch (Exception ex)
                           {
                               message = ex.Message; ;
                               throw;
                           }
                      }) .WithPostback()
                    .ConfirmWith (s => String.Format("Update Transaction Type {0} ", s.Name)).WithText("Update")
                    .OnSuccessDisplay(s => String.Format("Update Transaction Type: {0} has been updated ", s.Name))
                    .OnFailureDisplay(s => String.Format("Error: Transaction Type {0} was not updated ", message))
                    
              });
        }
    }
}
