using cbapp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace cbapp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Songs> Songs {get;set;}
    public DbSet<Project> projects {get;set;}

    public DbSet<SongRatings> songRatings {get;set;}
    public DbSet<CustomUsers> CustomUsers { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
         base.OnModelCreating(builder);
        builder.Entity<SongRatings>()
        .HasOne(sr=>sr.Song)
        .WithMany(s=>s.ratings)
        .HasForeignKey(sr=>sr.SongId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<SongRatings>()
        .HasOne(sr=>sr.User)
        .WithMany(u=>u.SongRatings)
        .HasForeignKey(SR=>SR.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<SongRatings>()
        .HasKey(sr=> new{sr.SongId,sr.UserId});

        builder.Entity<Songs>()
        .HasIndex(s=>s.title)
        .IsUnique();


    }
}
