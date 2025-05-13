using Microsoft.EntityFrameworkCore;
using GreenGrid.Models;
using System.Collections.Generic;

namespace GreenGrid.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
