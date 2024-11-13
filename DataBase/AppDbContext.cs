using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace MohaymenProject.DataBase
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }


        public AppDbContext()
            : base(new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=DESKTOP-CNEAI22\\FINALSERVER;Database=MyDatabase;Trusted_Connection=True;")
                .Options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
