using System;
using DemoProductApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoProductApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}
