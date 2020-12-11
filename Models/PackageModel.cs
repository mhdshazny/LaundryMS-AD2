using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryMS_AD2.Models
{
    [Table("tbl_Package")]
    public class PackageModel
    {
        [Key]
        [DisplayName("Package ID")]
        public string PkgID { get; set; }

        [Required(ErrorMessage ="Package Name Required.")]
        [DisplayName("Package Name")]
        public string PkgName { get; set; }

        [DisplayName("Package Description")]
        [DataType(DataType.MultilineText)]
        public string PkgDescr { get; set; }
        
        [Required(ErrorMessage ="Package Needs a Price value.")]
        [DisplayName("Package Price")]
        public float PkgPrice { get; set; }
        
        [Required(ErrorMessage ="Package status required.")]
        [DisplayName("Package Status")]
        public string PkgStatus { get; set; }


    }
}
