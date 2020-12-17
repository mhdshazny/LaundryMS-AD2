using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryMS_AD2.Models
{
    public class OrderListDataLibraryModel
    {
        [Key]
        [DisplayName("Order List ID")]
        public int OrdListID { get; set; }

        [Required]
        [DisplayName("Order Reference No")]
        public string OrderRefID { get; set; }

        [Required]
        [DisplayName("Order Product ID")]
        public string OrdPrID { get; set; }

        [Required]
        [DisplayName("Package Price")]
        public decimal OrdPkgUP { get; set; }

        [Required]
        [DisplayName("Order Quantity")]
        public int OrdPrQty { get; set; }

        [Required]
        [DisplayName("Total Amount")]
        public decimal OrdPrAmnt { get; set; }

        [Required]
        [DisplayName("Ordered Package")]
        public string OrdPkg { get; set; }

        [Required]
        [DisplayName("Order Status")]
        public string OrdListStatus { get; set; }
    }
}
