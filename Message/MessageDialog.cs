using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia.Controls;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.Models;

namespace DayZLinuxGUILauncher.Message
{
    public static class MessageDialog
    {
        
        public static void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
        
        public static void ShowMessage(string message)
        {
            var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBoxStandardParams{
                    ButtonDefinitions = ButtonEnum.Ok,
                    ContentTitle = "MSG",
                    ContentMessage = message,
                    Icon = Icon.Plus,
                    Style = Style.UbuntuLinux,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                    
                });
        
            msBoxStandardWindow.Show();
            
        }

        public static async Task<ButtonResult> YesNo(string message)
        {
            var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBoxStandardParams{
                    ButtonDefinitions = ButtonEnum.YesNo,
                    ContentMessage = message,
                    Icon = Icon.Stopwatch,
                    Style = Style.UbuntuLinux,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                    
                });
            return await msBoxStandardWindow.Show();

        }
        
        
        
        public static async Task<string> DataInput(string message,string title)
        {
            var messageBoxInputWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxInputWindow(new MessageBoxInputParams
                {
                    Style = Style.UbuntuLinux,
                    Topmost = true,
                    ShowInCenter = true,
                    ContentMessage = message,
                    ContentTitle = title,
                 
                    ButtonDefinitions = new[]
                    {
                        new ButtonDefinition { Name = "Cancel", IsCancel = true },
                        new ButtonDefinition { Name = "Confirm", Type = ButtonType.Colored, IsDefault = true }
                    },
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                  

                    
                });
              var result= await messageBoxInputWindow.Show();
             
              return result.Message;

        }



    }
    
    
    
}