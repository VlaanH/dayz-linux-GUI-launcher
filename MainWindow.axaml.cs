using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using DayZLinuxGUILauncher.Data;
using DayZLinuxGUILauncher.Database;
using DayZLinuxGUILauncher.InterfaceObjects;
using DayZLinuxGUILauncher.Message;
using DayZLinuxGUILauncher.Settings;

namespace DayZLinuxGUILauncher
{
    public class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Initialization();
            
        }

        async void Initialization()
        {
            //Create Main StackPanel
            InterfaceDatabaseRecord.MainStackPanel.Background= SolidColorBrush.Parse("#292828");
            
            this.FindControl<ScrollViewer>("DbRecord").Content = InterfaceDatabaseRecord.MainStackPanel;

            Update();
            
        }
        

        ServerData GetSettings()
        {
            ServerData settings = new ServerData();
            settings.ServerName = this.Find<TextBox>("ServerNmae").Text;
            settings.ServerIP = this.Find<TextBox>("ServerIP").Text;
            settings.queryport = this.Find<TextBox>("Queryport").Text;
            settings.User = this.Find<TextBox>("UserNameTextBox").Text;
            

            return settings;
        }

        
        

        async void Update()
        {
            bool error = false;
            
      

            
            ServerData settings = GetSettings();
            DatabaseManagement databaseManagement= new DatabaseManagement();
 

            List<ServerData> listAllRecordsDb = new List<ServerData>();

            await Task.Run(() =>
            {
                try
                {
                    listAllRecordsDb = databaseManagement.GetAllRecords();

                    

                    
                }
                catch (Exception e)
                {
                    error = true;
                }


               
            });
           

            if (error==false)
                InterfaceDatabaseRecord.DatabaseOutput(listAllRecordsDb,databaseManagement,this);
            else
            {
                MessageDialog.ShowMessage("Error");
            }
            
            
            

        }


        async Task<bool> Add()
        {
            bool error = false;

   
            ServerData settings = GetSettings();

            
            DatabaseManagement databaseManagement = new DatabaseManagement();
            
            
            List<ServerData> listAllRecordsDb = new List<ServerData>();
           
     
            
            await Task.Run(() =>
            {

                try
                {
                    
                
                    
                    databaseManagement.AddRecordDb(settings);
                   
             
                
                    listAllRecordsDb = databaseManagement.GetAllRecords();

                   
                }
                catch (Exception)
                {
                    error = true;
                }
                
                

                   
                return error;
                
                
                
            });
            
            
            if (error==false)
                InterfaceDatabaseRecord.DatabaseOutput(listAllRecordsDb,databaseManagement,this);

            return error;
        }

     

        private async void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            
            bool error = false;
           
         
            
            
            
            
           
                error = await Add();
                if (error==true)
                {
                    MessageDialog.ShowMessage("Failed to add record to database");
                }
            
            
        }




        private void QueryportButton_OnClick(object? sender, RoutedEventArgs e)
        {
            MessageDialog.OpenUrl("https://www.battlemetrics.com/servers/dayz/");
        }
    }
}

