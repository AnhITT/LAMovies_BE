using Libs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Libs.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Movie>? Movies { get; set; }
        public DbSet<RefreshToken>? RefreshTokens { get; set; }

        public DbSet<Genre>? Genres { get; set; }
        public DbSet<Actor>? Actors { get; set; }
        public DbSet<MovieActor>? MovieActors { get; set; }

        public DbSet<MovieGenre>? MovieGenres { get; set; }
        public DbSet<Pricing>? Pricings { get; set; }

        public DbSet<UserPricing>? UserPricings { get; set; }
        public DbSet<MovieHistory>? MovieHistorys { get; set; }
        public DbSet<OddMovie>? OddMovies { get; set; }
        public DbSet<SeriesMovie>? SeriesMovies { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<UserInRoom> UserInRooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>()
                    .HasKey(pc => new { pc.IdMovie, pc.IdGenre });
            modelBuilder.Entity<MovieGenre>()
                    .HasOne(p => p.Movie)
                    .WithMany(pc => pc.MovieGenre)
                    .HasForeignKey(p => p.IdMovie);
            modelBuilder.Entity<MovieGenre>()
                    .HasOne(p => p.Genre)
                    .WithMany(pc => pc.MovieGenre)
                    .HasForeignKey(c => c.IdGenre);

            modelBuilder.Entity<MovieActor>()
                    .HasKey(pc => new { pc.IdMovie, pc.IdActor });
            modelBuilder.Entity<MovieActor>()
                    .HasOne(p => p.Movie)
                    .WithMany(pc => pc.MovieActors)
                    .HasForeignKey(p => p.IdMovie);
            modelBuilder.Entity<MovieActor>()
                    .HasOne(p => p.Actor)
                    .WithMany(pc => pc.MovieActors)
                    .HasForeignKey(c => c.IdActor);

            modelBuilder.Entity<UserPricing>()
                    .HasKey(pc => new { pc.IdUser, pc.IdPricing });
            modelBuilder.Entity<UserPricing>()
                    .HasOne(p => p.Pricing)
                    .WithMany(pc => pc.UserPricing)
                    .HasForeignKey(p => p.IdPricing);
            modelBuilder.Entity<UserPricing>()
                    .HasOne(p => p.User)
                    .WithMany(pc => pc.UserPricing)
                    .HasForeignKey(c => c.IdUser);

            modelBuilder.Entity<MovieHistory>()
                   .HasKey(pc => new { pc.IdUser, pc.IdMovie });
            modelBuilder.Entity<MovieHistory>()
                    .HasOne(p => p.Movie)
                    .WithMany(pc => pc.MovieHistory)
                    .HasForeignKey(p => p.IdMovie);
            modelBuilder.Entity<MovieHistory>()
                    .HasOne(p => p.User)
                    .WithMany(pc => pc.MovieHistory)
                    .HasForeignKey(c => c.IdUser);


            modelBuilder.Entity<IdentityUserLogin<string>>()
        .HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<string>>()
        .HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>()
        .HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });

            modelBuilder.Entity<Movie>()
            .HasOne(m => m.OddMovie)
            .WithOne(u => u.Movie)
            .HasForeignKey<OddMovie>(u => u.IdMovie);

            modelBuilder.Entity<SeriesMovie>()
            .HasOne(u => u.Movie)
            .WithMany(m => m.SeriesMovie)
            .HasForeignKey(u => u.IdMovie);
        }
    }
}
