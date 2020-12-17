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

        public DbSet<CustomerModel> CustomerData { get; set; }
        public DbSet<ProductModel> ProductData { get; set; }
        public DbSet<PrTypeModel> PrTypeData { get; set; }
        public DbSet<OrderModel> OrderData { get; set; }
        public DbSet<OrderListModel> OrderListData { get; set; }
        public DbSet<OrderListDataLibraryModel> OrdListDL { get; set; }
        public DbSet<PackageModel> PackageData { get; set; }
        public DbSet<EmpRoleModel> EmpRoleData { get; set; }

    }
}
