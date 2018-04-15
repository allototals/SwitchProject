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
    public class EditRoute:EntityUI<Routes>
    {
        public IGenericService<Routes> _routesService = new GenericService<Routes>();
        string message = "";
        public EditRoute()
        {
            AddSection()
            .IsFramed()
            .WithTitle("Edit Routes")
            .WithColumns(new List<Column> 
            { 
                new Column(  
                     new List<IField> 
                     { 
                            Map(x => x.Name)
                                .AsSectionField<TextBox>()
                                .Required()
                                .WithLength(15),
                            Map(x => x.CardPAN)
                                .AsSectionField<TextBox>()
                                .Required()
                                .TextFormatIs(TextFormat.numeric)
                                .WithLength(19),
                            Map(x => x.Description)
                                .AsSectionField<TextArea>()
                                .Required()
                                .TextFormatIs(TextFormat.alphanumeric)
                            //Map(x => x.)
                            //    .AsSectionField<TextBox>()
                            //    .Required()
                            //    .TextFormatIs(TextFormat.money)
                            //    .WithLength(15),
                            //Map(x => x.MinimumAmount)
                            //    .AsSectionField<TextBox>()
                            //    .Required()
                            //    .TextFormatIs(TextFormat.money)
                      
            })
            })

           .WithFields(new List<IField>{
                   AddSectionButton()
                       .SubmitTo(f=> 
                       {
                           try
                           {
                               bool result = _routesService.Update(f);
                               return true;
                           }
                           catch (Exception ex)
                           {
                               message = ex.Message; ;
                               throw;
                           }
                      }) .WithPostback()
                    .ConfirmWith (s => String.Format("Update Route {0} ", s.Name)).WithText("Update")
                    .OnSuccessDisplay(s => String.Format("Update Route {0} has been updated ", s.Name))
                    .OnFailureDisplay(s => String.Format("Error: Fee{0} was not updated ", message))
                    
              });
        }
    }
}
