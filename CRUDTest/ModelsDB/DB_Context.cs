using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CRUDTest.ModelsDB
{
    public partial class DB_Context : DbContext
    {
        public DB_Context()
        {
        }

        public DB_Context(DbContextOptions<DB_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<brand> brands { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<order_item> order_items { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<staff> staffs { get; set; }
        public virtual DbSet<stock> stocks { get; set; }
        public virtual DbSet<store> stores { get; set; }
        public virtual DbSet<user_login> user_logins { get; set; }
        public virtual DbSet<res_customer_list> res_customer_list { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=TestDB;User id=sa;Password=Keng1234");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Thai_100_CI_AS");

            modelBuilder.Entity<brand>(entity =>
            {
                entity.HasKey(e => e.brand_id)
                    .HasName("PK__brands__5E5A8E2780F7142B");

                entity.ToTable("brands", "production");

                entity.Property(e => e.brand_name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<category>(entity =>
            {
                entity.HasKey(e => e.category_id)
                    .HasName("PK__categori__D54EE9B49B447202");

                entity.ToTable("categories", "production");

                entity.Property(e => e.category_name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<customer>(entity =>
            {
                entity.HasKey(e => e.customer_id)
                    .HasName("PK__customer__CD65CB851826FFAD");

                entity.ToTable("customers", "sales");

                entity.Property(e => e.city)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.first_name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.last_name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.phone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.state)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.street)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.zip_code)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<order>(entity =>
            {
                entity.HasKey(e => e.order_id)
                    .HasName("PK__orders__46596229914F6445");

                entity.ToTable("orders", "sales");

                entity.Property(e => e.order_date).HasColumnType("date");

                entity.Property(e => e.required_date).HasColumnType("date");

                entity.Property(e => e.shipped_date).HasColumnType("date");

                entity.HasOne(d => d.customer)
                    .WithMany(p => p.orders)
                    .HasForeignKey(d => d.customer_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__orders__customer__6D0D32F4");

                entity.HasOne(d => d.staff)
                    .WithMany(p => p.orders)
                    .HasForeignKey(d => d.staff_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orders__staff_id__6EF57B66");

                entity.HasOne(d => d.store)
                    .WithMany(p => p.orders)
                    .HasForeignKey(d => d.store_id)
                    .HasConstraintName("FK__orders__store_id__6E01572D");
            });

            modelBuilder.Entity<order_item>(entity =>
            {
                entity.HasKey(e => new { e.order_id, e.item_id })
                    .HasName("PK__order_it__837942D428A36B16");

                entity.ToTable("order_items", "sales");

                entity.Property(e => e.discount).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.list_price).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.order)
                    .WithMany(p => p.order_items)
                    .HasForeignKey(d => d.order_id)
                    .HasConstraintName("FK__order_ite__order__72C60C4A");

                entity.HasOne(d => d.product)
                    .WithMany(p => p.order_items)
                    .HasForeignKey(d => d.product_id)
                    .HasConstraintName("FK__order_ite__produ__73BA3083");
            });

            modelBuilder.Entity<product>(entity =>
            {
                entity.HasKey(e => e.product_id)
                    .HasName("PK__products__47027DF54E53DE4D");

                entity.ToTable("products", "production");

                entity.Property(e => e.list_price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.product_name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.brand)
                    .WithMany(p => p.products)
                    .HasForeignKey(d => d.brand_id)
                    .HasConstraintName("FK__products__brand___619B8048");

                entity.HasOne(d => d.category)
                    .WithMany(p => p.products)
                    .HasForeignKey(d => d.category_id)
                    .HasConstraintName("FK__products__catego__60A75C0F");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.HasKey(e => e.staff_id)
                    .HasName("PK__staffs__1963DD9C87CD83A3");

                entity.ToTable("staffs", "sales");

                entity.HasIndex(e => e.email, "UQ__staffs__AB6E6164FF4A25F0")
                    .IsUnique();

                entity.Property(e => e.email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.first_name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.last_name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.phone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.manager)
                    .WithMany(p => p.Inversemanager)
                    .HasForeignKey(d => d.manager_id)
                    .HasConstraintName("FK__staffs__manager___6A30C649");

                entity.HasOne(d => d.store)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.store_id)
                    .HasConstraintName("FK__staffs__store_id__693CA210");
            });

            modelBuilder.Entity<stock>(entity =>
            {
                entity.HasKey(e => new { e.store_id, e.product_id })
                    .HasName("PK__stocks__E68284D3AAA75421");

                entity.ToTable("stocks", "production");

                entity.HasOne(d => d.product)
                    .WithMany(p => p.stocks)
                    .HasForeignKey(d => d.product_id)
                    .HasConstraintName("FK__stocks__product___778AC167");

                entity.HasOne(d => d.store)
                    .WithMany(p => p.stocks)
                    .HasForeignKey(d => d.store_id)
                    .HasConstraintName("FK__stocks__store_id__76969D2E");
            });

            modelBuilder.Entity<store>(entity =>
            {
                entity.HasKey(e => e.store_id)
                    .HasName("PK__stores__A2F2A30CB1F4D28D");

                entity.ToTable("stores", "sales");

                entity.Property(e => e.city)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.phone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.state)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.store_name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.street)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.zip_code)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<user_login>(entity =>
            {
                entity.HasKey(e => e.user_id)
                    .HasName("PK_user.login");

                entity.ToTable("user_login");

                entity.Property(e => e.created_time).HasColumnType("datetime");

                entity.Property(e => e.password).IsRequired();

                entity.Property(e => e.salt_key)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.user_name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<res_customer_list>(entity =>
            {
                entity.HasNoKey();
                entity.Property(e => e.customer_id);
                entity.Property(e => e.city);
                entity.Property(e => e.email);
                entity.Property(e => e.first_name);
                entity.Property(e => e.last_name);
                entity.Property(e => e.phone);
                entity.Property(e => e.state);
                entity.Property(e => e.street);
                entity.Property(e => e.zip_code);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
