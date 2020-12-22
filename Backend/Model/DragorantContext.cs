using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Backend.Model
{
    public partial class DragorantContext : DbContext
    {
        public DragorantContext()
        {
        }

        public DragorantContext(DbContextOptions<DragorantContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Deliverer> Deliverers { get; set; }
        public virtual DbSet<DelivererRate> DelivererRates { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<FoodIngredient> FoodIngredients { get; set; }
        public virtual DbSet<FoodOrder> FoodOrders { get; set; }
        public virtual DbSet<FoodRate> FoodRates { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderStation> OrderStations { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=Dragorant;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Name).IsFixedLength(true);
            });

            modelBuilder.Entity<Deliverer>(entity =>
            {
                entity.Property(e => e.Address).IsFixedLength(true);

                entity.Property(e => e.Email).IsFixedLength(true);

                entity.Property(e => e.FirstName).IsFixedLength(true);

                entity.Property(e => e.LastName).IsFixedLength(true);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Username).IsFixedLength(true);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Deliverers)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Deliverer_City");
            });

            modelBuilder.Entity<DelivererRate>(entity =>
            {
                entity.HasOne(d => d.Deliverer)
                    .WithMany(p => p.DelivererRates)
                    .HasForeignKey(d => d.DelivererId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DelivererRate_Deliverer");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.Property(e => e.Name).IsFixedLength(true);
            });

            modelBuilder.Entity<FoodIngredient>(entity =>
            {
                entity.HasOne(d => d.Food)
                    .WithMany(p => p.FoodIngredients)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoodIngredient_Food");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.FoodIngredients)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoodIngredient_Ingredient");
            });

            modelBuilder.Entity<FoodOrder>(entity =>
            {
                entity.HasOne(d => d.Food)
                    .WithMany(p => p.FoodOrders)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoodOrder_Food");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.FoodOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoodOrder_Order");
            });

            modelBuilder.Entity<FoodRate>(entity =>
            {
                entity.HasOne(d => d.Food)
                    .WithMany(p => p.FoodRates)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoodRate_Food");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FoodRates)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoodRate_User");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.Property(e => e.Name).IsFixedLength(true);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Status).IsFixedLength(true);

                entity.HasOne(d => d.Deliverer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DelivererId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Deliverer");

                entity.HasOne(d => d.OrderStation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_OrderStation");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<OrderStation>(entity =>
            {
                entity.Property(e => e.Address).IsFixedLength(true);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Username).IsFixedLength(true);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.OrderStations)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderStation_City");

                entity.HasOne(d => d.Resteurant)
                    .WithMany(p => p.OrderStations)
                    .HasForeignKey(d => d.ResteurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderStation_Restaurant");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.Property(e => e.Address).IsFixedLength(true);

                entity.Property(e => e.Email).IsFixedLength(true);

                entity.Property(e => e.FirstName).IsFixedLength(true);

                entity.Property(e => e.LastName).IsFixedLength(true);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Username).IsFixedLength(true);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Owners)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Owner_City");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.Property(e => e.Name).IsFixedLength(true);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Restaurant_Owner");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Address).IsFixedLength(true);

                entity.Property(e => e.Email).IsFixedLength(true);

                entity.Property(e => e.FirstName).IsFixedLength(true);

                entity.Property(e => e.LastName).IsFixedLength(true);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Username).IsFixedLength(true);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_City");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
