using Microsoft.EntityFrameworkCore;

namespace UWS.Shared

{
    public class Chinook : DbContext
    {
      public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }
      
        public Chinook(DbContextOptions<Chinook> options)
        : base(options) { Database.EnsureCreated(); }
        protected override void OnModelCreating(
        ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Artist>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(120);

            // define a one-to-many relationship
          //  modelBuilder.Entity<Artist>()
          //  .HasMany(c => c.Albums)
           // .WithOne(p => p.Artist)
           // .OnDelete(DeleteBehavior.Cascade);

              modelBuilder.Entity<Album>()
            .Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(100);

           //  modelBuilder.Entity<Album>()
          //  .HasOne(p => p.Artist)
           // .WithMany(c => c.Albums)
          //  .OnDelete(DeleteBehavior.Cascade);
           
        
              modelBuilder.Entity<Track>()
            .Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(200);

           //    modelBuilder.Entity<Track>()
           // .HasOne(p => p.Album)
            //.WithMany(b => b.Tracks)
           // .OnDelete(DeleteBehavior.Cascade);
              
        }

    }
}
