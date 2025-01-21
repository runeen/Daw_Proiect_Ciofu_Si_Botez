using cbapp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace cbapp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Songs> Songs {get;set;}
    public DbSet<Project> projects {get;set;}

    public DbSet<ProjectRatings> ProjectRatings {get;set;}
    public DbSet<CustomUsers> CustomUsers { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);

    // Configurare pentru cheia primară compusă
    builder.Entity<ProjectRatings>()
        .HasKey(pr => new { pr.projectId, pr.UserId });

    // Relația cu Project
    builder.Entity<ProjectRatings>()
        .HasOne(pr => pr.Project)
        .WithMany(p => p.ratings)
        .HasForeignKey(pr => pr.projectId)
        .OnDelete(DeleteBehavior.Cascade);

    // Relația cu CustomUsers
    builder.Entity<ProjectRatings>()
        .HasOne(pr => pr.User)
        .WithMany(u => u.ProjectRatings)
        .HasForeignKey(pr => pr.UserId)
        .OnDelete(DeleteBehavior.Cascade);
}
}
