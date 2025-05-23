﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GreenGrid.Models;

namespace GreenGrid.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<EnergyProvider> EnergyProviders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Farmer> Farmers { get; set; } // 👈 Add this line
    }
}
