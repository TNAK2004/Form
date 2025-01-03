using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.MySQL.Data{
    public class AppDbContext:DbContext{
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
        public DbSet<FunForm> FunForms {get; set;}
        
    }
}
    
