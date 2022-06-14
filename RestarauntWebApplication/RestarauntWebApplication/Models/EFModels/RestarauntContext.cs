using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RestarauntWebApplication.Models.EFModels
{
    public partial class RestarauntContext : DbContext
    {
        public RestarauntContext()
        {
        }

        public RestarauntContext(DbContextOptions<RestarauntContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Cook> Cooks { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<DishCookOrder> DishCookOrders { get; set; }
        public virtual DbSet<DishType> DishTypes { get; set; }
        public virtual DbSet<DishesIngridient> DishesIngridients { get; set; }
        public virtual DbSet<Ingridient> Ingridients { get; set; }
        public virtual DbSet<Operator> Operators { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<Visitor> Visitors { get; set; }
        public virtual DbSet<VisitorsTable> VisitorsTables { get; set; }
        public virtual DbSet<Waiter> Waiters { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Restaraunt;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.Property(e => e.AdministratorId).HasColumnName("administrator_id");

                entity.Property(e => e.WorkerId).HasColumnName("worker_id");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.Administrators)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Administrators_Workers");
            });

            modelBuilder.Entity<Cook>(entity =>
            {
                entity.Property(e => e.CookId).HasColumnName("cook_id");

                entity.Property(e => e.CookPost)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("cook_post");

                entity.Property(e => e.WorkerId).HasColumnName("worker_id");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.Cooks)
                    .HasForeignKey(d => d.WorkerId)
                    .HasConstraintName("FK_Cooks_Workers");
            });

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.Property(e => e.DishId).HasColumnName("dish_id");

                entity.Property(e => e.DishCost)
                    .HasColumnType("money")
                    .HasColumnName("dish_cost");

                entity.Property(e => e.DishName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("dish_name");

                entity.Property(e => e.DishSeason)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("dish_season");

                entity.Property(e => e.DishTypeId).HasColumnName("dish_type_id");

                entity.HasOne(d => d.DishType)
                    .WithMany(p => p.Dishes)
                    .HasForeignKey(d => d.DishTypeId)
                    .HasConstraintName("FK_Dishes_DishTypes");
            });

            modelBuilder.Entity<DishCookOrder>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.DishId });

                entity.ToTable("Dish_Cook-Order");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.DishId).HasColumnName("dish_id");

                entity.Property(e => e.CookId).HasColumnName("cook_id");

                entity.Property(e => e.DishCount).HasColumnName("dish_count");

                entity.Property(e => e.DishStatus)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("dish_status");

                entity.HasOne(d => d.Cook)
                    .WithMany(p => p.DishCookOrders)
                    .HasForeignKey(d => d.CookId)
                    .HasConstraintName("FK_Dish_Cook-Order_Cooks");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.DishCookOrders)
                    .HasForeignKey(d => d.DishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dish_Cook-Order_Dishes");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.DishCookOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dish_Cook-Order_Orders");
            });

            modelBuilder.Entity<DishType>(entity =>
            {
                entity.Property(e => e.DishTypeId).HasColumnName("dish_type_id");

                entity.Property(e => e.DishTypeName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("dish_type_name");
            });

            modelBuilder.Entity<DishesIngridient>(entity =>
            {
                entity.HasKey(e => new { e.DishId, e.IngridientId });

                entity.ToTable("Dishes_Ingridients");

                entity.Property(e => e.DishId).HasColumnName("dish_id");

                entity.Property(e => e.IngridientId).HasColumnName("ingridient_id");

                entity.Property(e => e.IngridientCount).HasColumnName("ingridient_count");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.DishesIngridients)
                    .HasForeignKey(d => d.DishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dishes_Ingridients_Dishes");

                entity.HasOne(d => d.Ingridient)
                    .WithMany(p => p.DishesIngridients)
                    .HasForeignKey(d => d.IngridientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dishes_Ingridients_Ingridients");
            });

            modelBuilder.Entity<Ingridient>(entity =>
            {
                entity.Property(e => e.IngridientId).HasColumnName("ingridient_id");

                entity.Property(e => e.IngridientMeasure)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ingridient_measure");

                entity.Property(e => e.IngridientName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ingridient_name");

                entity.Property(e => e.IngridientQuantity).HasColumnName("ingridient_quantity");

                entity.Property(e => e.IngridientUnits).HasColumnName("ingridient_units");
            });

            modelBuilder.Entity<Operator>(entity =>
            {
                entity.Property(e => e.OperatorId).HasColumnName("operator_id");

                entity.Property(e => e.OperatorPost)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("operator_post");

                entity.Property(e => e.WorkerId).HasColumnName("worker_id");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.Operators)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Operators_Workers");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.OrderNumber).HasColumnName("order_number");

                entity.Property(e => e.OrderStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("order_status");

                entity.Property(e => e.TableId).HasColumnName("table_id");

                entity.Property(e => e.WaiterId).HasColumnName("waiter_id");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TableId)
                    .HasConstraintName("FK_Orders_Tables");

                entity.HasOne(d => d.Waiter)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.WaiterId)
                    .HasConstraintName("FK_Orders_Waiters1");
            });

            modelBuilder.Entity<Table>(entity =>
            {
                entity.Property(e => e.TableId).HasColumnName("table_id");

                entity.Property(e => e.TableCountSeat).HasColumnName("table_count_seat");

                entity.Property(e => e.TableDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("table_description");

                entity.Property(e => e.TableFloor).HasColumnName("table_floor");

                entity.Property(e => e.TableMapPosition)
                    .IsUnicode(false)
                    .HasColumnName("table_map_position");
            });

            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.Property(e => e.VisitorId).HasColumnName("visitor_id");

                entity.Property(e => e.VisitorEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("visitor_email");

                entity.Property(e => e.VisitorFullName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("visitor_full_name");

                entity.Property(e => e.VisitorTelephone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("visitor_telephone");
            });

            modelBuilder.Entity<VisitorsTable>(entity =>
            {
                entity.HasKey(e => e.BookingId)
                    .HasName("PK_Visitors_Tables_1");

                entity.ToTable("Visitors_Tables");

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.DateBooking)
                    .HasColumnType("datetime")
                    .HasColumnName("date_booking");

                entity.Property(e => e.TableId).HasColumnName("table_id");

                entity.Property(e => e.VisitorId).HasColumnName("visitor_id");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.VisitorsTables)
                    .HasForeignKey(d => d.TableId)
                    .HasConstraintName("FK_Visitors_Tables_Tables");

                entity.HasOne(d => d.Visitor)
                    .WithMany(p => p.VisitorsTables)
                    .HasForeignKey(d => d.VisitorId)
                    .HasConstraintName("FK_Visitors_Tables_Visitors");
            });

            modelBuilder.Entity<Waiter>(entity =>
            {
                entity.Property(e => e.WaiterId).HasColumnName("waiter_id");

                entity.Property(e => e.WorkerId).HasColumnName("worker_id");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.Waiters)
                    .HasForeignKey(d => d.WorkerId)
                    .HasConstraintName("FK_Waiters_Workers");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.Property(e => e.WorkerId).HasColumnName("worker_id");

                entity.Property(e => e.WorkerFullName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("worker_full_name");

                entity.Property(e => e.WorkerLogin)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("worker_login");

                entity.Property(e => e.WorkerPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("worker_password");

                entity.Property(e => e.WorkerPosition)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("worker_position");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
