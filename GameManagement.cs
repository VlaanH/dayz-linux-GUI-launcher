using System;
using System.Diagnostics;
using DayZLinuxGUILauncher.Data;

namespace DayZLinuxGUILauncher
{
    public class GameManagement
    {
  


        private static string Bash(string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return result;
        }

        public static string GameStart(ServerData serverData)
        {
            //./dayz-launcher.sh -d -l -s 185.189.255.184:2602 -p 27023 -n vhcomp

            return Bash($@"./dayz-launcher.sh -d -l -s {serverData.ServerIP} -p {serverData.queryport} -n {serverData.User}");
        }

        public static string GetMods(ServerData serverData)
        {
            //./dayz-launcher.sh -d  -s 185.189.255.184:2602 -p 27023
            return Bash($@"./dayz-launcher.sh -d -s {serverData.ServerIP} -p {serverData.queryport}");
        }

        public static string ConfigureMods(string mods)
        {

            return Bash("./dayz-launcher.sh -d "+mods);
        }



    }
}