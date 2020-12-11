using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryMS_AD2.Models
{
    [Table("tbl_Product")]
    public class ProductModel
    {
        [Key]
        [DisplayName("Product ID")]
        public string PrID { get; set; }

        [Required(ErrorMessage = "Provide a Product Name.")]
        [DisplayName("Product Name")]
        public string PrName { get; set; }

        [Required(ErrorMessage = "Provide a Proper Product Type.")]
        [DisplayName("Product Type")]
        public string PrType { get; set; }

        [DisplayName("Product Description")]
        public string PrDescr { get; set; }

        [Required(ErrorMessage ="Provide proper status details.")]
        public string PrStatus { get; set; }
    }
}
