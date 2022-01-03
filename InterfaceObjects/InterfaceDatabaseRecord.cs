using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using DayZLinuxGUILauncher.Data;
using DayZLinuxGUILauncher.Database;
using DayZLinuxGUILauncher.Message;

namespace DayZLinuxGUILauncher.InterfaceObjects
{
    public static class InterfaceDatabaseRecord
    {
        
        public static StackPanel MainStackPanel = new StackPanel();
        public static Grid GetRecordGrid(ServerData userData,DatabaseManagement databaseManagement,Window mainWindow)
        {
            Grid record = new Grid();
            
            record.ColumnDefinitions.Add( new ColumnDefinition());
            record.ColumnDefinitions.Add( new ColumnDefinition());
            record.ColumnDefinitions.Add( new ColumnDefinition());
            record.ColumnDefinitions.Add( new ColumnDefinition());
            record.ColumnDefinitions.Add( new ColumnDefinition());
            record.ColumnDefinitions.Add( new ColumnDefinition());
            record.ColumnDefinitions.Add( new ColumnDefinition());
            record.ColumnDefinitions[0].Width = GridLength.Parse("90");
            record.ColumnDefinitions[1].Width = GridLength.Parse("80");
            record.ColumnDefinitions[6].Width = GridLength.Parse("130");
            bool isActivated = false;
            
            TextBox id = new TextBox();
            id.Classes = Classes.Parse("records");
            id.Text = userData.Id.ToString();
            record.Children.Add(id);
            id.IsEnabled = false;
            Grid.SetColumn(id,1);
            
            
            TextBox User = new TextBox();
            User.Classes = Classes.Parse("records");
            User.Text = userData.User;
            record.Children.Add(User);
            User.IsEnabled = false; 
            Grid.SetColumn(User,2);
            
            
            
            
            
            
            TextBox ServerIP = new TextBox();
            ServerIP.Classes = Classes.Parse("records");
            ServerIP.Text = userData.ServerIP;
            record.Children.Add(ServerIP);
            ServerIP.IsEnabled = false; 
            Grid.SetColumn(ServerIP,3);
    
           
           
            TextBox ServerName = new TextBox();
            ServerName.Classes = Classes.Parse("records");
            ServerName.Text = userData.ServerName;
            record.Children.Add(ServerName);
            ServerName.IsEnabled = false;
            Grid.SetColumn(ServerName,4);
            
            
            TextBox queryport = new TextBox();
            queryport.Classes = Classes.Parse("records");
            queryport.Text = userData.queryport;
            record.Children.Add(queryport);
            queryport.IsEnabled = false;
            Grid.SetColumn(queryport,5);



            Grid buttons = new Grid();
            buttons.ColumnDefinitions.Add( new ColumnDefinition());
            buttons.ColumnDefinitions.Add( new ColumnDefinition());

            Button recordUpdateButton = new Button();
            recordUpdateButton.Content = "↻";
            buttons.Children.Add(recordUpdateButton);
            Grid.SetColumn(recordUpdateButton,0);
            
            Button dellRecordButton = new Button();
            dellRecordButton.Content = "-";
            buttons.Children.Add(dellRecordButton);
            Grid.SetColumn(dellRecordButton,1);

            
            Grid buttonsDayZ = new Grid();
            buttonsDayZ.ColumnDefinitions.Add( new ColumnDefinition());
            buttonsDayZ.ColumnDefinitions.Add( new ColumnDefinition());

            Button mods = new Button();
            mods.Content = "mods";
            buttonsDayZ.Children.Add(mods);
            Grid.SetColumn(mods,0);
            
            Button launch = new Button();
            launch.Content = "▶️";
            buttonsDayZ.Children.Add(launch);
            Grid.SetColumn(launch,1);
            
           
  
            launch.Click += async (s, e) =>
            {
                
                if ((string) launch.Content!="!▶")
                {
                    string log = default; 
                    launch.Content = "!▶";
                    await Task.Run(() =>
                    {
                        ServerData userData = new ServerData()
                        {   
                            Id = int.Parse(id.Text),
                            queryport = queryport.Text,
                            ServerIP = ServerIP.Text,
                            ServerName = ServerName.Text,
                            User = User.Text
                            
                            
                        };
                        log = GameManagement.GameStart(userData);
                       
                    
                    });
                    MessageDialog.ShowMessage(log);
                    launch.Content = "▶️";  
                }
               
            };
            
            mods.Click += (s, e) =>
            {
                ServerData userData = new ServerData()
                {   
                    Id = int.Parse(id.Text),
                    queryport = queryport.Text,
                    ServerIP = ServerIP.Text,
                    ServerName = ServerName.Text,
                    User = User.Text
                            
                            
                };
                Mods.UseServerData = userData;
                Mods.Show(mainWindow);
                    
                
              
             
            };
                
            
            dellRecordButton.Click += (s, e) =>
            {
                
                    databaseManagement.DeleteRecord(int.Parse(id.Text));
                    MainStackPanel.Children.Remove(record);
                    
                
              
             
            };
            
            recordUpdateButton.Click += (s, e) =>
            {
                bool error = false;
                
                if (isActivated==false)
                {
                    User.IsEnabled = true;
                    ServerIP.IsEnabled = true;
                    ServerName.IsEnabled = true;
                    queryport.IsEnabled = true;
                    isActivated = true;
                    recordUpdateButton.Content = "✓";
                }
                else
                {
                    
                    ServerData userData = new ServerData()
                    {   
                        Id = int.Parse(id.Text),
                        queryport = queryport.Text,
                        ServerIP = ServerIP.Text,
                        ServerName = ServerName.Text,
                        User = User.Text
                        
                        
                    };
                    
                    databaseManagement.UpdateRecordDb(userData);
                    
                    User.IsEnabled = false;
                    ServerIP.IsEnabled = false;
                    ServerName.IsEnabled = false;
                    queryport.IsEnabled = false;
                    isActivated = false;
                    recordUpdateButton.Content = "↻";
                }
            };
            
            record.Children.Add(buttonsDayZ);
            Grid.SetColumn(buttonsDayZ,6);
        
            Grid.SetColumn(buttons,0);
            record.Children.Add(buttons);
            
          
            return record;
        }

        public static void DatabaseOutput(List<ServerData> listUserData,DatabaseManagement databaseManagement,Window mainWindow)
        {
            MainStackPanel.Children.Clear();
            for (int i = 0; i < listUserData.Count; i++)
            {

               Grid recordGrid = GetRecordGrid(listUserData[i],databaseManagement,mainWindow);
               MainStackPanel.Children.Add(recordGrid);
               
            }
            
            
        }
    }
}