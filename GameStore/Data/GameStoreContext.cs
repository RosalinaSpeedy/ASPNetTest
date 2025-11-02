using System;
using GameStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .Entity<Genre>()
            .HasData(
                new { Id = 1, Name = "Puzzle" },
                new { Id = 2, Name = "Platforming" },
                new { Id = 3, Name = "Stealth Action" },
                new { Id = 4, Name = "RPG" },
                new { Id = 5, Name = "Idle" }
            );
    }
}
