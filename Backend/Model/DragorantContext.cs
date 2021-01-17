using System;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Backend
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
        public virtual DbSet<FoodUser> FoodUsers { get; set; }
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
                optionsBuilder.UseSqlServer("Server=DESKTOP-IJ6PTEF;Database=Dragorant;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Deliverer>(entity =>
            {
                entity.ToTable("Deliverer");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("address")
                    .IsFixedLength(true);

                entity.Property(e => e.CityId).HasColumnName("cityId");

                entity.Property(e => e.DragonCoinBalance).HasColumnName("dragonCoinBalance");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .IsFixedLength(true);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("firstName")
                    .IsFixedLength(true);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lastName")
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username")
                    .IsFixedLength(true);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Deliverers)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Deliverer_City");
            });

            modelBuilder.Entity<DelivererRate>(entity =>
            {
                entity.ToTable("DelivererRate");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.DelivererId).HasColumnName("delivererId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.Deliverer)
                    .WithMany(p => p.DelivererRates)
                    .HasForeignKey(d => d.DelivererId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DelivererRate_Deliverer");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("Food");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .IsFixedLength(true);

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<FoodIngredient>(entity =>
            {
                entity.ToTable("FoodIngredient");

                entity.Property(e => e.FoodId).HasColumnName("foodId");

                entity.Property(e => e.IngredientId).HasColumnName("ingredientId");

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
                entity.ToTable("FoodOrder");

                entity.Property(e => e.FoodId).HasColumnName("foodId");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

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
                entity.ToTable("FoodRate");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.FoodId).HasColumnName("foodId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Value).HasColumnName("value");

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

            modelBuilder.Entity<FoodUser>(entity =>
            {
                entity.ToTable("FoodUser");

                entity.HasOne(d => d.IdFoodNavigation)
                    .WithMany(p => p.FoodUsers)
                    .HasForeignKey(d => d.IdFood)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoodUser_Food");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.FoodUsers)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoodUser_User");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredient");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.DelivererId).HasColumnName("delivererId");

                entity.Property(e => e.EndTime).HasColumnName("endTime");

                entity.Property(e => e.OrderStationId).HasColumnName("orderStationId");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("status")
                    .IsFixedLength(true);

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Deliverer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DelivererId)
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
                entity.ToTable("OrderStation");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("address")
                    .IsFixedLength(true);

                entity.Property(e => e.CityId).HasColumnName("cityId");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.ResteurantId).HasColumnName("resteurantId");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username")
                    .IsFixedLength(true);

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
                entity.ToTable("Owner");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("address")
                    .IsFixedLength(true);

                entity.Property(e => e.CityId).HasColumnName("cityId");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .IsFixedLength(true);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("firstName")
                    .IsFixedLength(true);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lastName")
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username")
                    .IsFixedLength(true);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Owners)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Owner_City");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("Restaurant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .IsFixedLength(true);

                entity.Property(e => e.OwnerId).HasColumnName("ownerId");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Restaurant_Owner");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("address")
                    .IsFixedLength(true);

                entity.Property(e => e.CityId).HasColumnName("cityId");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .IsFixedLength(true);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("firstName")
                    .IsFixedLength(true);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lastName")
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username")
                    .IsFixedLength(true);

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
