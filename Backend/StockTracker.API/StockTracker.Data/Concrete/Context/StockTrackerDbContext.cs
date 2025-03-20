using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockTracker.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Data.Concrete.Context
{
    public class StockTrackerDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public StockTrackerDbContext(DbContextOptions<StockTrackerDbContext> options) : base(options)
        {
        }

        // Tablolar
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalItem> RentalItems { get; set; }
        public DbSet<ReturnedProduct> ReturnedProducts { get; set; }
        public DbSet<RemainingProduct> RemainingProducts { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<CustomerPayment> CustomerPayments { get; set; }


        public DbSet<IncomingTransaction> IncomingTransactions { get; set; }
        public DbSet<OutgoingTransaction> OutgoingTransactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>()
             .HasKey(c => c.Id);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Product>()
                .Property(p => p.MonthlyPrice)
                .HasColumnType("decimal(18,2)");

  
            modelBuilder.Entity<Rental>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // RentalItem Configuration
            modelBuilder.Entity<RentalItem>()
                .HasKey(ri => ri.Id);

            modelBuilder.Entity<RentalItem>()
                .HasOne(ri => ri.Rental)
                .WithMany(r => r.RentalItems)
                .HasForeignKey(ri => ri.RentalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CustomerPayment>()
                  .HasOne(cp => cp.CustomerAccount)
                  .WithMany() 
                  .HasForeignKey(cp => cp.CustomerAccountId)
                  .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<RemainingProduct>()
        .HasKey(rp => rp.Id);

            modelBuilder.Entity<RemainingProduct>()
                .HasOne(rp => rp.RentalItem)   
                .WithMany()                     
                .HasForeignKey(rp => rp.RentalItemId)  
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReturnedProduct>()
         .HasKey(rp => rp.Id);

            modelBuilder.Entity<ReturnedProduct>()
                .HasOne(rp => rp.RentalItem)    
                .WithMany()                     
                .HasForeignKey(rp => rp.RentalItemId)   
                .OnDelete(DeleteBehavior.Restrict);

     
            modelBuilder.Entity<CustomerAccount>()
                .HasKey(ca => ca.Id);

            modelBuilder.Entity<CustomerAccount>()
                .HasOne(ca => ca.Customer)
                .WithMany()
                .HasForeignKey(ca => ca.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

     
      

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<ApplicationRole>().HasData(
               new ApplicationRole { Id = "115c7796-cfac-44de-91b5-916eaae125b5", Name = "AdminUser", NormalizedName = "ADMINUSER", Description = "Administrator role" },
               new ApplicationRole { Id = "811f466c-9d06-43f8-a054-24aedbb4161b", Name = "NormalUser", NormalizedName = "NORMALUSER", Description = "Regular user role" }
           );

            // Default users
            var hasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser
            {
                Id = "c0b7fef7-df2b-4857-9b3d-bc8967ad19ac",
                UserName = "mehmet@gmail.com",
                NormalizedUserName = "mehmet@GMAIL.COM",
                Email = "adminuser@gmail.com",
                NormalizedEmail = "ADMINUSER@GMAIL.COM",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "User",
                PhoneNumber = "5255",
                PasswordHash = hasher.HashPassword(null, "Qwe123.,")
            };
            var adminUser2 = new ApplicationUser
            {
                Id = "14a0183f-1e96-4930-a83d-6ef5f22d8c09",
                UserName = "normaluser@gmail.com",
                NormalizedUserName = "admin@GMAIL.COM",
                Email = "normaluser@gmail.com",
                NormalizedEmail = "admin@GMAIL.COM",
                EmailConfirmed = true,
                FirstName = "Admin2",
                LastName = "User",
             
                PasswordHash = hasher.HashPassword(null, "Qwe123.,")
            };
            modelBuilder.Entity<ApplicationUser>().HasData(adminUser, adminUser2);

            // Assign roles to users
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminUser.Id, RoleId = "115c7796-cfac-44de-91b5-916eaae125b5" },
                new IdentityUserRole<string> { UserId = adminUser2.Id, RoleId = "811f466c-9d06-43f8-a054-24aedbb4161b" }
            );
            modelBuilder.Entity<Customer>().HasData(
       new Customer { Id = 1, Address="dfdfdf", Email="sdsds", Name="dfdf", LastName="fgdfd", Phone="34322"},
        new Customer { Id = 2, Address = "dfdfdf", Email = "sdsds", Name = "dfdf", LastName = "fgdfd", Phone = "34322" }
   );
            modelBuilder.Entity<Product>().HasData(
    new Product
    {
        Id = 1,
        Name = "Laptop",
        MonthlyPrice = 300.00m,  // Aylık kira bedeli
        StockQuantity = 100,
        Description = "Laptop description"
    },
    new Product
    {
        Id = 2,
        Name = "Projector",
        MonthlyPrice = 150.00m,
        StockQuantity = 200,
        Description = "Projector description"
    }
);


            base.OnModelCreating(modelBuilder);
        }
    }
}

