using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LaundryMS_AD2.Models;

namespace LaundryMS_AD2.Models
{
    public class DbConnectionClass : DbContext
    {
        public DbConnectionClass(DbContextOptions<DbConnectionClass> options) : base(options)
        {

        }

        public DbSet<UserModel> UserData { get; set; }
        public DbSet<UserModel> EmpIdentityData { get; set; }

    }
}
