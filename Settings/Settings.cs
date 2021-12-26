using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace DayZLinuxGUILauncher.Settings
{
  
    

   
    
    public class SettingsManagement
    {
        public static class PatchDialog
        {
            public static async Task<string?> GetFilePatch(Window mainWindow)
            {
                var dialog = new OpenFileDialog();
            
                var result = await dialog.ShowAsync(mainWindow);

                if (result != null)
                    return result[0];
                else
                    return default;

            }
            public static async Task<string?> GetFolderPatch(Window mainWindow)
            {
                var dialog = new OpenFolderDialog();
            
                var result = await dialog.ShowAsync(mainWindow);

                if (result != null)
                    return result;
                else
                    return default;

            }


   
        }
      
        
        
    
        
        
        
    }

}