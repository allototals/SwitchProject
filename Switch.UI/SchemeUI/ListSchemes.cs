using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using Switch.Core.Model;
using Switch.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Switch.UI
{
    public class ListSchemes:EntityUI<Schemes>
    {
        public ListSchemes()
        {
           
                WithTitle("Scheme ");
                AddNorthSection().WithTitle("Search").IsFramed().IsCollapsible()
               .WithFields(new List<IField>()
                {
                  Map(x => x.Name).AsSectionField<TextBox>().TextFormatIs(TextFormat.alphanumeric).WithLength(80),
                  AddSectionButton().WithText("Cancel").ConfirmWith("Are you sure?").SubmitTo(x => false),
                  AddSectionButton().WithText("Search").UpdateWith(x =>
                           {                                          
                                 return x;
                          } )
                 });
                
                AddSection().WithTitle("List Of Combo").IsFramed().IsCollapsible()
               .WithColumns(new List<Column>()
               {
                new Column(new List<IField>()
                {
                  
                         HasMany(x => x.SchemesList)
                        .AsSectionField<Grid>() 
                        .Of<Schemes>()
                        .WithRowNumbers()
                        .WithColumn(x => x.Name)
                       // .WithColumn(x => x.Route.Name,"Route")
                        .WithColumn(x => x.Description)
                       // .WithColumn(x => x.SchemesList.Count,"TrasactionTypeChannelFee Count")
                        .WithRowNumbers()
                        .IsPaged<Schemes>(10, (x, e) =>
                        {
                            //Expression<Func<Schemes, bool>> where = p => (p.Id != null);
                            //if(!string.IsNullOrEmpty(x.Name))
                            //    where=p=>(p.Name.ToLower().Equals(x.Name.ToLower()));                    


                            x.SchemesList = new GenericService<Schemes>().SelectAll();//FilterBy(where).ToList();
                            return x;
        
               }).ApplyMod<ViewDetailsMod>(y => y.Popup<ViewScheme>("View Details"))    
               })
            });
           
        }
    }
}
