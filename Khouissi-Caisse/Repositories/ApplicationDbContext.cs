using Microsoft.EntityFrameworkCore;
using Khouissi_Caisse.Models;

namespace Khouissi_Caisse.Repositories;

/// <summary>
/// Database context for the Khouissi-Caisse application
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Members in the database
    /// </summary>
    public DbSet<Member> Members { get; set; } = null!;

    /// <summary>
    /// Subscriptions in the database
    /// </summary>
    public DbSet<Subscription> Subscriptions { get; set; } = null!;

    /// <summary>
    /// Expenses in the database
    /// </summary>
    public DbSet<Expense> Expenses { get; set; } = null!;

    /// <summary>
    /// Users in the database
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// Application settings in the database
    /// </summary>
    public DbSet<ApplicationSettings> ApplicationSettings { get; set; } = null!;

    /// <summary>
    /// Creates a new instance of the database context
    /// </summary>
    /// <param name="options">Database context options</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Configure the database model
    /// </summary>
    /// <param name="modelBuilder">Model builder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Member entity relationships
        modelBuilder.Entity<Member>()
            .HasOne(m => m.ParentMember)
            .WithMany(m => m.ChildMembers)
            .HasForeignKey(m => m.ParentMemberId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Subscription entity relationships
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Member)
            .WithMany()
            .HasForeignKey(s => s.MemberId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.RecordedByUser)
            .WithMany()
            .HasForeignKey(s => s.RecordedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Expense entity relationships
        modelBuilder.Entity<Expense>()
            .HasOne(e => e.RecordedByUser)
            .WithMany()
            .HasForeignKey(e => e.RecordedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure ApplicationSettings entity relationships
        modelBuilder.Entity<ApplicationSettings>()
            .HasOne(s => s.LastModifiedByUser)
            .WithMany()
            .HasForeignKey(s => s.LastModifiedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
