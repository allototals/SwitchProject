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
    public class EditFees:EntityUI<Fees>
    {
        private readonly IGenericService<Fees> _feesService = new GenericService<Fees>();
        string message ="";
        public EditFees()
        {
            AddSection()
              .IsFramed()
              .WithTitle("Edit Fees")
              .WithColumns(new List<Column> 
            { 
                new Column(  
                     new List<IField> 
                     { 
                            Map(x => x.Name)
                                .AsSectionField<TextBox>()
                                .Required()
                                .WithLength(15),
                            Map(x => x.FlatAmount)
                                .AsSectionField<TextBox>()
                                .Required()
                                .TextFormatIs(TextFormat.money)
                                .WithLength(15),
                            Map(x => x.AmountPercent)
                                .AsSectionField<TextArea>()
                                .Required()
                                .TextFormatIs(TextFormat.numeric),
                            Map(x => x.MaximumAmount)
                                .AsSectionField<TextBox>()
                                .Required()
                                .TextFormatIs(TextFormat.money)
                                .WithLength(15),
                            Map(x => x.MinimumAmount)
                                .AsSectionField<TextBox>()
                                .Required()
                                .TextFormatIs(TextFormat.money)
                      
                              })
                    })

             .WithFields(new List<IField>{
                   AddSectionButton()
                       .SubmitTo(f=> 
                       {
                           try
                           {
                               bool result = _feesService.Update(f);
                               return true;
                           }
                           catch (Exception ex)
                           {
                               message = ex.Message; ;
                               throw;
                           }
                      }) .WithPostback()
                    .ConfirmWith (s => String.Format("Update Fee {0} ", s.Name)).WithText("Update")
                    .OnSuccessDisplay(s => String.Format("Update Fee {0} has been updated ", s.Name))
                    .OnFailureDisplay(s => String.Format("Error: Fee{0} was not updated ", message))
                    
              });
        }
    }
    
}
