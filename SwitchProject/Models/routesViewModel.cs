using Switch.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwitchProject.Models
{
    public class routesViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CardPAN { get; set; }
        public Guid SinkNodeId { get; set; }
    }
    public static class RoutesExtension
    {
        public static Switch.Data.Models.Routes ConvertToEntityModel(this routesViewModel oView)
        {
            var Routes= new Switch.Data.Models.Routes();
            Routes.CardPAN=oView.CardPAN;
            Routes.Description=oView.Description;
            Routes.Id= oView.Id;
            Routes.Name = oView.Name;
            Routes.Id = oView.Id;
           // Routes.SinkNode.Id=oView.SinkNodeId;
            return Routes;
        }
        public static routesViewModel ConvertToViewModel(this Switch.Data.Models.Routes obj)
        {
            return new routesViewModel
            {
                Id = obj.Id,
                CardPAN=obj.CardPAN,
                Description= obj.Description,
                SinkNodeId=obj.SinkNode.Id,    
                Name = obj.Name
            };

        }

    }
}