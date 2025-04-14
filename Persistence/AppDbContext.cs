using System;
using Domain;
using Microsoft.EntityFrameworkCore;


namespace Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<AccountDetails> AccountDetails { get; set; }
}