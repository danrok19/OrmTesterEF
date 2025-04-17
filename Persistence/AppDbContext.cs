using System;
using Domain;
using Microsoft.EntityFrameworkCore;


namespace Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<AccountDetails> AccountDetails { get; set; }
    public DbSet<Boss> Bosses { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<Fight> Fights { get; set; }
    public DbSet<Guild> Guilds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Fight>()
            .HasKey(f => new { f.CharacterId, f.BossId });

        modelBuilder.Entity<Fight>()
            .HasOne(f => f.Character)
            .WithMany(c => c.Fights)
            .HasForeignKey(f => f.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Fight>()
            .HasOne(f => f.Boss)
            .WithMany(b => b.Fights)
            .HasForeignKey(f => f.BossId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Equipment>()
            .HasMany(e => e.Characters)
            .WithMany(c => c.Equipments)
            .UsingEntity<Dictionary<string, object>>(
            "character_equipment",
            j => j
                .HasOne<Character>()
                .WithMany()
                .HasForeignKey("character_id")
                .HasConstraintName("FK_CHARACTER_EQUIPMENT_character_id")
                .OnDelete(DeleteBehavior.Cascade),
            j => j
                .HasOne<Equipment>()
                .WithMany()
                .HasForeignKey("equipment_id")
                .HasConstraintName("FK_CHARACTER_EQUIPMENT_equipment_id")
                .OnDelete(DeleteBehavior.Cascade),
            j =>
            {
                j.ToTable("character_equipment");
                j.HasKey("equipment_id", "character_id");
            });
    }
}