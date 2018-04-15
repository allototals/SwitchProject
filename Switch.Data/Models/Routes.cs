using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Switch.Data.Models
{
    public class Routes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression("[^A-Za-z]+")]
        [Display(Name = "Route Name")]
        public string Name { get; set; }
        [Required]
        [MaxLength(19)]
        [RegularExpression("[^0-9]+")]
        public string CardPAN { get; set; }

        [RegularExpression("[^A-Za-z]+")]
        public string Description { get; set; }
        [Required]
        public virtual SinkNode SinkNode { get; set; }
        [ScaffoldColumn(false)]
        public bool IsDeleted { get; set; }
    }
    //public static class ClassOptionExtension
    //{
    //    public static Switch.Data.Models.Routes ConvertToEntityModel(this rou oView)
    //    {
    //        return new ClassRoom.Data.Models.ClassOption
    //        {

    //            Id = oView.Id,
    //            schoolclassid = oView.schoolclassid,
    //            OptionName = oView.OptionName,

    //        };
    //    }
    //    public static ClassOptionViewModel ConvertToViewModel(this ClassRoom.Data.Models.ClassOption obj)
    //    {
    //        return new ClassOptionViewModel
    //        {
    //            Id = obj.Id,
    //            schoolclassid = obj.schoolclassid,
    //            OptionName = obj.OptionName,
    //            Name = obj.SchoolClass.Name
    //        };

    //    }

    //}
}
