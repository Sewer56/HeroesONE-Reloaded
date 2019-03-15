using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Reloaded.IO;
using Reloaded.Paths;

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

            // Copy Reloaded Default Theme if missing.
            string themeFolderPath      = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\Theme";

            if (IsDirectoryEmpty(LoaderPaths.GetModLoaderThemeDirectory()))
                DirectoryCopy(themeFolderPath, LoaderPaths.GetModLoaderThemeDirectory(), true);

            // Boot
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

        /// <summary>
        /// Copies the contents of a directory to another directory.
        /// </summary>
        /// <param name="sourceDirName">The directory to copy from.</param>
        /// <param name="destDirName">The directory to copy to.</param>
        /// <param name="copySubDirs">If true, recursively copies subdirectories.</param>
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo directory             = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] subDirectories      = directory.GetDirectories();

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = directory.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in subDirectories)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    Directory.CreateDirectory(tempPath);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }

        /// <summary>
        /// Returns true if a directory is empty.
        /// </summary>
        private static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
    }
}
