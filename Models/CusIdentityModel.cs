using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryMS_AD2.Models
{
    public class CusIdentityModel
    {
        [Required(ErrorMessage = "Please provide a proper Email")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email ID")]
        public string CusEmail { get; set; }

        [Required(ErrorMessage = "Please provide a proper Password")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string CusPassw { get; set; }

        [DisplayName("Status")]
        public string CusStatus { get; set; }
    }
}
