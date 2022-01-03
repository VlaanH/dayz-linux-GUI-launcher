using System;
using System.Collections.Generic;
using System.Linq;
using DayZLinuxGUILauncher.Data;

namespace DayZLinuxGUILauncher.Database
{
    public class DatabaseManagement
    {
        public string _databasePathg;

        public DatabaseManagement()
        {
            _databasePathg = "Servers.db";
        }

   
        
        public void AddRecordDb(ServerData userData)
        {
           
            using ( AppDbContext appDbContext = new AppDbContext())
            {
                Console.WriteLine(userData.queryport);
                appDbContext._databasePath = _databasePathg;

                appDbContext.ServerData.Add(userData);

                appDbContext.SaveChanges();
            }
            
           
        }

        public void UpdateRecordDb(ServerData userData)
        {
            using (AppDbContext appDbContext = new AppDbContext())
            { 
                appDbContext._databasePath = _databasePathg;
               
                var updateFunc = new Func<ServerData, bool>(user => user.Id == userData.Id);

                var serverData = appDbContext.ServerData.Single(updateFunc);
                serverData.queryport = userData.queryport;
                serverData.ServerName = userData.ServerName;
                serverData.ServerIP = userData.ServerIP;
                serverData.User = userData.User;
                
                appDbContext.SaveChanges();

            }


        }


        public List<ServerData> GetAllRecords()
        {
            using (AppDbContext appDbContext = new AppDbContext())
            {
                appDbContext._databasePath = _databasePathg;
                return appDbContext.ServerData.ToList();

              

            }

         
        }

        public void DeleteRecord(int id)
        {
            using (AppDbContext appDbContext = new AppDbContext())
            {
                appDbContext._databasePath = _databasePathg;
                
                var linqFun = new Func<ServerData, bool>(ud => ud.Id == id);


             
                appDbContext.Remove(appDbContext.ServerData.Single(linqFun));
                
                appDbContext.SaveChanges();

            }
        }
        

        
        
        
    }
}
