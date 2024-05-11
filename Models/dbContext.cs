using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ECommerce.Models;

    public class dbContext : DbContext
    {
    public dbContext(){}
    protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=dbContext-5e2b9977-2478-4a32-b081-5b5e8f8ed412.db");
    public dbContext (DbContextOptions<dbContext> options)
            : base(options)
        {
        }
    

        public DbSet<ECommerce.Models.Libri> Libri { get; set; } = default!;

        public DbSet<ECommerce.Models.Utente> Utente { get; set; } = default!;
    }
