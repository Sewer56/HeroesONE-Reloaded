using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeroesONE_R_GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            SetDefault();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Fix Reloaded Theme missing
            string reloadedConfigPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\Reloaded-Mod-Loader\\Reloaded-Config";
            string asmPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\theme";
            if (!File.Exists(Path.Combine(reloadedConfigPath, "Config.json")))
            {
                Directory.CreateDirectory(reloadedConfigPath + "\\Themes\\!Reloaded\\Fonts");
                Directory.CreateDirectory(reloadedConfigPath + "\\Themes\\!Reloaded\\Images");
                File.Copy(asmPath + "\\Config.json", reloadedConfigPath + "\\Config.json");
                File.Copy(asmPath + "\\Themes\\!Reloaded\\Config.json", reloadedConfigPath + "\\Themes\\!Reloaded\\Config.json");
                File.Copy(asmPath + "\\Themes\\!Reloaded\\Theme.json", reloadedConfigPath + "\\Themes\\!Reloaded\\Theme.json");
                File.Copy(asmPath + "\\Themes\\!Reloaded\\Fonts\\CategoryFont.ttf", reloadedConfigPath + "\\Themes\\!Reloaded\\Fonts\\CategoryFont.ttf");
                File.Copy(asmPath + "\\Themes\\!Reloaded\\Fonts\\TextFont.ttf", reloadedConfigPath + "\\Themes\\!Reloaded\\Fonts\\TextFont.ttf");
                File.Copy(asmPath + "\\Themes\\!Reloaded\\Fonts\\TitleFont.ttf", reloadedConfigPath + "\\Themes\\!Reloaded\\Fonts\\TitleFont.ttf");
                foreach (var file in Directory.GetFiles(asmPath + "\\Themes\\!Reloaded\\Images"))
                {
                    File.Copy(file, reloadedConfigPath + "\\Themes\\!Reloaded\\Images\\" + Path.GetFileName(file));
                }
            }
            //Until lib is updated

            if (args.Length > 0) { Application.Run(new MainWindow(args[0])); }
            else { Application.Run(new MainWindow()); }      
        }

        /// <summary>
        /// Sets HeroesONE Reloaded as the default application for .one files.
        /// </summary>
        public static void SetDefault()
        {
            // Navigate to Computer\HKEY_CURRENT_USER\Software\Classes\
            var classesKey = Registry.CurrentUser.OpenSubKey("Software", true)?.OpenSubKey("Classes", true);

            // Create an entry for ONE files.
            var oneKey = classesKey.CreateSubKey(".one");

            // Gets the path of the executable and the command string.
            string myExecutable = Assembly.GetEntryAssembly().Location;
            string command = $"\"{myExecutable}\" \"%1\"";

            // Create default key for .one\shell\Open\command\
            var commandKey = oneKey.CreateSubKey("shell\\Open\\command");
            commandKey.SetValue("", command);
        }
    }
}
