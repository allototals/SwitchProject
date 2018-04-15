using Switch.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwitchProject.Models
{
    public class schemeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       // public string CardPAN { get; set; }
       // public Channels Channels {get;set;}
        public Guid ChannelId {get;set;}
        public Guid FeesId {get;set;}
       // public Fees Fees {get;set;}
       // public Routes Routes { get; set; }
        public Guid RoutesId { get; set; }
       // public TransactionType TransType { get; set; }
        public Guid TransTypeId { get; set; }

        public bool IsDeleted { get; set; }
        
    }
    public static class schemeExtension
    {
        public static Switch.Data.Models.Schemes ConvertToEntityModel(this schemeViewModel oView, Channels channel,Fees Fees, Routes Routes, TransactionType TransType )
        {
            var Schemes = new Switch.Data.Models.Schemes();
            Schemes.Channel = channel;
            Schemes.Description=oView.Description;
            Schemes.Fees = Fees;
            Schemes.Id = oView.Id;
            Schemes.Name = oView.Name;
            Schemes.Route=Routes;
            Schemes.TransType = TransType;
            Schemes.IsDeleted = oView.IsDeleted;
            return Schemes;
        }
        public static SwitchProject.Models.schemeViewModel ConvertToViewModel (this Switch.Data.Models.Schemes obj)
        {
            return new schemeViewModel
            {
                 ChannelId=obj.Channel.Id,
                  Description=obj.Description,
                   FeesId=obj.Fees.Id,
                    Name=obj.Name,
                     RoutesId=obj.Route.Id,
                      TransTypeId=obj.TransType.Id,
                      IsDeleted= obj.IsDeleted
                       
            };
        }
    }
}