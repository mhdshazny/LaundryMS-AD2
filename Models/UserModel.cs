using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryMS_AD2.Models
{
    [Table("tbl_Employee")]
    public class UserModel
    {
        [Key]
        [Required]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string fName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string lName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Display(Name = "Date of Birth")]
        [Required]
        public DateTime DoB { get; set; }

        [Required]
        public string NIC { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Address Line")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "Contact")]
        [Required]
        public string Contact { get; set; }

        [Display(Name = "User Role")]
        [Required]
        public string Role { get; set; }

        [Display(Name = "Branch Name")]
        [Required]
        public string Branch { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }


        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Your password and confirm password do not match.")]
        //[Display(Name = "Confirm Password")]

        //public string ConfirmPassword { get; set; }

        [Display(Name = "User Status")]
        [Required]
        public string Status { get; set; }




        //public List<UserViewModel> AllUsers()
        //{
        //    //var userlist;


        //    return userlist;
        //}




        //    public int DeleteUser()
        //    {
        //        int result = 0;
        //        try
        //        {
        //            //sql = "DELETE FROM tbl_User WHERE userID=@uid";
        //            sql = "DELETE tbl_User WHERE userID=@uid";
        //            com = new SqlCommand(sql, con);
        //            com.Parameters.Add("@uid", SqlDbType.Char, 10, "userID").Value = this.UserID;
        //            //com.Parameters.Add("@delStat", SqlDbType.VarChar, 50, "fName").Value = "Deleted";
        //            con.Open();
        //            com.ExecuteNonQuery();
        //            result = 1;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            con.Close();

        //        }
        //        return result;
        //    }
        //}

    }
}
