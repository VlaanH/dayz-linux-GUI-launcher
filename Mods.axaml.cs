using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DayZLinuxGUILauncher.Data;
using DayZLinuxGUILauncher.Message;
using DayZLinuxGUILauncher.Settings;

namespace DayZLinuxGUILauncher
{
    public class Mods : Window
    {
        public Mods()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

        }

        public static ServerData UseServerData = default;
      

        public static void Show(Window parent)
        {
            var msgbox = new Mods();
            msgbox.ShowDialog(parent);
        }


        private async void ConfigureMods_OnClick(object? sender, RoutedEventArgs e)
        { 
            var mods= await MessageDialog.DataInput("enter mods separated by space for example:100 252 354", "Enter mod");
            string logs = default;
            this.Find<TextBox>("log").Text = "Getting data...";
           
            await Task.Run( () =>
            {
               
                logs= GameManagement.ConfigureMods(mods);
                
            });
            this.Find<TextBox>("log").Text = logs;
        }

        private async void ModList_OnClick(object? sender, RoutedEventArgs e)
        {
            string logs = default;

            this.Find<TextBox>("log").Text = "Getting data...";
            await Task.Run( () =>
            {
                logs= GameManagement.GetMods(UseServerData);
            
            });
            this.Find<TextBox>("log").Text = logs;
        }
    }
}