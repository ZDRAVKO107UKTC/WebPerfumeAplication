using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebCosmeticApp.Models;
using WebPerfumeAplication.Models;

namespace WebCosmeticApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Cosmetic> Cosmetics { get; set; }
    }
}
