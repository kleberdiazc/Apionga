using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppSonga.Models;

namespace AppSonga.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<AppSonga.Models.Tallas> Tallas { get; set; }
        public DbSet<AppSonga.Models.Consulta> Consulta { get; set; }
    }
}
