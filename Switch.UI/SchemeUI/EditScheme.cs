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
    public class EditScheme:EntityUI<Schemes>
    {
        public EditScheme()
        {
            WithTitle("Scheme Management");
            AddSection().WithTitle("Edit ").StretchFields(30).IsCollapsible().IsFramed()
            .WithFields(new List<IField>()
                {
                    Map(x => x.Name).AsSectionField<TextBox>().Required().TextFormatIs(TextFormat.alphanumeric).WithLength(30),
                    Map(x => x.Route).AsSectionField<DropDownList>().Of(new GenericService<Routes>().SelectAll()).ListOf(x=>x.Name,x=>x.Id).LabelTextIs("Route"),
                     Map(x => x.Channel).AsSectionField<DropDownList>().Of(new GenericService<Channels>().SelectAll()).ListOf(x=>x.Name,x=>x.Id).LabelTextIs("Channels"),
                    Map(x => x.Fees).AsSectionField<DropDownList>().Of(new GenericService<Fees>().SelectAll()).ListOf(x=>x.Name,x=>x.Id).LabelTextIs("Fees"),
                     Map(x => x.TransType).AsSectionField<DropDownList>().Of(new GenericService<TransactionType>().SelectAll()).ListOf(x=>x.Name,x=>x.Id).LabelTextIs("Transaction Type"),
                    Map(x => x.Description).AsSectionField<TextArea>().Required().TextFormatIs(TextFormat.alphanumeric),
                    AddSectionButton().WithText("Update").SubmitTo(x =>
                        {
                            var Result = false;
                            try
                            {
                                Result = new GenericService<Schemes>().Update(x);// new SourceNode {Name = x.Name,IPAddress = x.IPAddress,HostName=x.HostName,Port=x.Port});
                            }
                            catch(Exception)
                            {
                                throw;
                            }
                            return Result;
                        }).OnSuccessDisplay("Successfully Saved").OnFailureDisplay("Oops!! Unexpected Error"),
             });
        }
    }
}
