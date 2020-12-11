using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryMS_AD2.Models
{
    [Table("tbl_OrderList")]
    public class OrderListModel
    {
        [Key]
        [DisplayName("Order List ID")]
        public int OrdListID { get; set; }

        [DisplayName("Order List ID")]
        public string OrderID { get; set; }

        public string OrdPrID { get; set; }

        public int OrdPrQty { get; set; }

        public float OrdPrAmnt { get; set; }

        public string OrdPkg { get; set; }

        public string OrdListStatus { get; set; }
    }
}
