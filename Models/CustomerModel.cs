using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryMS_AD2.Models
{
    [Table("tbl_Customer")]
    public class CustomerModel
    {
        [Key]
        [DisplayName("Customer ID")]
        public string CusID { get; set; }

        [Required(ErrorMessage ="Please provide a proper Name.")]
        [DisplayName("First Name")]
        public string CusfName { get; set; }

        [Required(ErrorMessage = "Please provide a proper Name.")]
        [DisplayName("Last Name")]
        public string CuslName { get; set; }

        [Required(ErrorMessage = "Please provide a proper NIC Number.")]
        [DisplayName("Customer NIC")]
        public string CusNIC { get; set; }

        [Required(ErrorMessage = "Please provide a proper Gender.")]
        [DisplayName("Customer Gender")]
        public string CusGender { get; set; }

        [Required(ErrorMessage = "Please provide a proper Address.")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Customer Address")]
        public string CusAddress { get; set; }

        [Required(ErrorMessage = "Please provide a proper Contact No.")]
        [DisplayName("Contact No.")]
        public string CusContact { get; set; }

        [Required(ErrorMessage = "Please provide a proper Email")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Customer Email")]
        public string CusEmail { get; set; }

        [Required(ErrorMessage = "Please provide a proper Password")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string CusPassw { get; set; }

        [Required(ErrorMessage = "Please provide a proper Customer Status")]
        [DisplayName("Status")]
        public string CusStatus { get; set; }
    }
}
