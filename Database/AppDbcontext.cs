using System.Security.Cryptography.X509Certificates;
using DayZLinuxGUILauncher.Data;
using Microsoft.EntityFrameworkCore;

namespace DayZLinuxGUILauncher.Database
{


    
    
    public class AppDbContext :  DbContext
    {   
        //for easy creation of migrations
        public string _databasePath = "Servers.db";
        
        
        public DbSet<ServerData> ServerData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlite($"Data Source={_databasePath}");
        }
    }


}