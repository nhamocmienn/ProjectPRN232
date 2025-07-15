using Microsoft.EntityFrameworkCore;
using RapPhim1.Models;

namespace RapPhim1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Actor> Actors => Set<Actor>();
        public DbSet<Director> Directors => Set<Director>();
        public DbSet<MovieGenre> MovieGenres => Set<MovieGenre>();
        public DbSet<MovieActor> MovieActors => Set<MovieActor>();
        public DbSet<MovieDirector> MovieDirectors => Set<MovieDirector>();

        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<SeatType> SeatTypes => Set<SeatType>();
        public DbSet<Seat> Seats => Set<Seat>();
        public DbSet<Showtime> Showtimes => Set<Showtime>();

        public DbSet<ServiceItem> ServiceItems => Set<ServiceItem>();

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderTicket> OrderTickets => Set<OrderTicket>();
        public DbSet<OrderService> OrderServices => Set<OrderService>();




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Khóa chính cho bảng nhiều-nhiều
            modelBuilder.Entity<MovieGenre>().HasKey(mg => new { mg.MovieId, mg.GenreId });
            modelBuilder.Entity<MovieActor>().HasKey(ma => new { ma.MovieId, ma.ActorId });
            modelBuilder.Entity<MovieDirector>().HasKey(md => new { md.MovieId, md.DirectorId });

            // Quan hệ Seat - Room
            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Room)
                .WithMany(r => r.Seats)
                .HasForeignKey(s => s.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Seat>()
                .HasOne(s => s.SeatType)
                .WithMany(st => st.Seats)
                .HasForeignKey(s => s.SeatTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Showtime - Movie & Room
            modelBuilder.Entity<Showtime>()
                .HasOne(s => s.Movie)
                .WithMany(m => m.Showtimes)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Showtime>()
                .HasOne(s => s.Room)
                .WithMany(r => r.Showtimes)
                .HasForeignKey(s => s.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderTicket
            modelBuilder.Entity<OrderTicket>()
                .HasOne(ot => ot.Order)
                .WithMany(o => o.OrderTickets)
                .HasForeignKey(ot => ot.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderTicket>()
                .HasOne(ot => ot.Seat)
                .WithMany()
                .HasForeignKey(ot => ot.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderTicket>()
                .HasOne(ot => ot.Showtime)
                .WithMany()
                .HasForeignKey(ot => ot.ShowtimeId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderService
            modelBuilder.Entity<OrderService>()
                .HasOne(os => os.Order)
                .WithMany(o => o.OrderServices)
                .HasForeignKey(os => os.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderService>()
                .HasOne(os => os.ServiceItem)
                .WithMany()
                .HasForeignKey(os => os.ServiceItemId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }
    }
}
