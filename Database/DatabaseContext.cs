using Backend_TechFix.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace Backend_TechFix.Database
{
    public class DatabaseContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        // DbSet properties for each custom entity
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SupplierProduct> SupplierProducts { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuoteItem> QuoteItems { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Rfq> Rfqs { get; set; }
        public DbSet<RfqItem> RfqItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure primary keys for custom entities
            modelBuilder.Entity<Supplier>().HasKey(s => s.SupplierID);
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryID);
            modelBuilder.Entity<Brand>().HasKey(b => b.BrandID);
            modelBuilder.Entity<Product>().HasKey(p => p.ProductID);
            modelBuilder.Entity<SupplierProduct>().HasKey(sp => sp.SupplierProductID);
            modelBuilder.Entity<Quote>().HasKey(q => q.QuoteID);
            modelBuilder.Entity<QuoteItem>().HasKey(qi => qi.QuoteItemID);
            modelBuilder.Entity<InventoryItem>().HasKey(ii => ii.InventoryItemID);
            modelBuilder.Entity<Order>().HasKey(o => o.OrderID);
            modelBuilder.Entity<OrderItem>().HasKey(oi => oi.OrderItemID);
            modelBuilder.Entity<UserType>().HasKey(ut => ut.UserTypeID);
            modelBuilder.Entity<Rfq>().HasKey(r => r.RfqID);
            modelBuilder.Entity<RfqItem>().HasKey(ri => ri.RfqItemID);

            // Identity-specific configurations
            modelBuilder.Entity<IdentityUserLogin<int>>().HasKey(iul => iul.UserId);
            modelBuilder.Entity<IdentityUserRole<int>>().HasKey(iur => new { iur.UserId, iur.RoleId });
            modelBuilder.Entity<IdentityRoleClaim<int>>().HasKey(irc => irc.Id);
            modelBuilder.Entity<IdentityUserClaim<int>>().HasKey(iuc => iuc.Id);
            modelBuilder.Entity<IdentityUserToken<int>>().HasKey(iut => new { iut.UserId, iut.LoginProvider, iut.Name });

            // Configure Permission-Role relationships
            modelBuilder.Entity<RolePermission>().HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany()
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany()
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Category-Product relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            // Brand-Product relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandID)
                .OnDelete(DeleteBehavior.Restrict);

            // SupplierProduct relationship
            modelBuilder.Entity<SupplierProduct>()
                .HasOne(sp => sp.Supplier)
                .WithMany(s => s.SupplierProducts)
                .HasForeignKey(sp => sp.SupplierID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupplierProduct>()
                .HasOne(sp => sp.Product)
                .WithMany(p => p.SupplierProducts)
                .HasForeignKey(sp => sp.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            // Rfq relationships
            modelBuilder.Entity<Rfq>()
                .HasOne(r => r.TechFixUser)
                .WithMany()
                .HasForeignKey(r => r.TechFixUserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rfq>()
                .HasOne(r => r.Supplier)
                .WithMany(s => s.Rfqs)
                .HasForeignKey(r => r.SupplierID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RfqItem>()
                .HasOne(ri => ri.Rfq)
                .WithMany(r => r.RfqItems)
                .HasForeignKey(ri => ri.RfqID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RfqItem>()
                .HasOne(ri => ri.Product)
                .WithMany(p => p.RfqItems)
                .HasForeignKey(ri => ri.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            // Quote relationships
            modelBuilder.Entity<Quote>()
                .HasOne(q => q.Supplier)
                .WithMany(s => s.Quotes)
                .HasForeignKey(q => q.SupplierID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Quote>()
                .HasOne(q => q.Rfq)
                .WithMany()
                .HasForeignKey(q => q.RfqID)
                .OnDelete(DeleteBehavior.Cascade);

            // Enum mapping for QuoteStatus
            modelBuilder.Entity<Quote>()
                .Property(q => q.QuoteStatus)
                .HasConversion<string>();  // Store enum as string in database

            // QuoteItem relationship
            modelBuilder.Entity<QuoteItem>()
                .HasOne(qi => qi.Quote)
                .WithMany(q => q.QuoteItems)
                .HasForeignKey(qi => qi.QuoteID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuoteItem>()
                .HasOne(qi => qi.Product)
                .WithMany(p => p.QuoteItems)
                .HasForeignKey(qi => qi.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            // InventoryItem relationship
            modelBuilder.Entity<InventoryItem>()
                .HasOne(ii => ii.Supplier)
                .WithMany(s => s.InventoryItems)
                .HasForeignKey(ii => ii.SupplierID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InventoryItem>()
                .HasOne(ii => ii.Product)
                .WithMany(p => p.InventoryItems)
                .HasForeignKey(ii => ii.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            // Quote-Order relationship
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Quote)
                .WithMany()  
                .HasForeignKey(o => o.QuoteID)
                .OnDelete(DeleteBehavior.Restrict);

            // Enum mapping for OrderStatus
            modelBuilder.Entity<Order>()
                .Property(o => o.OrderStatus)
                .HasConversion<string>();  // Store enum as string in database

            // OrderItem relationship
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            // User-Supplier relationship (optional)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Supplier)
                .WithMany(s => s.Users)
                .HasForeignKey(u => u.SupplierID)
                .OnDelete(DeleteBehavior.Restrict);

            // User-UserType relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserType)
                .WithMany(ut => ut.Users)
                .HasForeignKey(u => u.UserTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            // Add unique constraints to product
            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.BrandID, p.ModelNumber })
                .IsUnique()
                .HasDatabaseName("IX_Product_Brand_Model");
        }
    }
}
