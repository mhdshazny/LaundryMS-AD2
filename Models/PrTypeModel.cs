using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryMS_AD2.Models
{
    [Table("tbl_PrType")]
    public class PrTypeModel
    {
        [Key]
        [DisplayName("Product Type ID")]
        public int PrTypeID { get; set; }

        [Required(ErrorMessage ="Provide a Type.")]
        [DisplayName("Product Type")]
        public string PrType { get; set; }

        [Required(ErrorMessage = "Provide a Description.")]
        [DisplayName("Product Type Description")]
        [DataType(DataType.MultilineText)]
        public string PrTypeDescr { get; set; }

        [Required(ErrorMessage = "Provide Product Type Status.")]
        [DisplayName("Product Type Status")]
        public string PrTypeStatus { get; set; }
    }
}
