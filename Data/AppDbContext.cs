using Microsoft.EntityFrameworkCore;
using GestionePuntiVendita.Models;

namespace GestionePuntiVendita.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
}
