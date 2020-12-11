using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryMS_AD2.Models
{
    [Table("tbl_EmpRole")]
    public class EmpRoleModel
    {
        [Key]
        public int EmpRoleID { get; set; }

        [DisplayName("Employee Role")]
        public string EmpRole { get; set; }
    }
}
