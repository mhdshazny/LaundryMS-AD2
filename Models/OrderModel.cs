using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryMS_AD2.Models
{
    [Table("tbl_Order")]
    public class OrderModel
    {
        [Key]
        [DisplayName("Order ID")]
        public string OrderID { get; set; }

        [Required(ErrorMessage ="Privede the relevant order reference number.")]
        [DisplayName("Order Reference ID")]
        public string OrderRefNo{ get; set; }

        [Required(ErrorMessage ="Provide Customer ID")]
        [DisplayName("Customer ID")]
        public string OrderCusID { get; set; }

        [DisplayName("Confirmed User")]
        public string OrderApprvdBy { get; set; }

        [Required]
        [DisplayName("Order Total Quantity")]
        public int OrderTotQty { get; set; }

        [Required(ErrorMessage = "Provide Total Amount")]
        [DisplayName("Total Amount")]
        public decimal OrderTotAmnt { get; set; }

        [Required(ErrorMessage = "Provide Customer ID")]
        [DisplayName("Ordered Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Provide a Delivery Date")]
        [DisplayName("Delivery Date")]
        public DateTime OrderDelivery { get; set; }

        [DisplayName("Order Delivery Address")]
        [DataType(DataType.MultilineText)]
        public string OrderDeliveryAddress { get; set; }

        [DisplayName("Special Order Description")]
        [DataType(DataType.MultilineText)]
        public string OrderDescr { get; set; }

        [Required(ErrorMessage ="Provide the current order status.")]
        [DisplayName("Order Status")]
        public string OrderStatus { get; set; }

        [Required(ErrorMessage ="Provide the current order payment status.")]
        [DisplayName("Order Payment Status")]
        public string OrderPaymentStatus { get; set; }

    }
}
