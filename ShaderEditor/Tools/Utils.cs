using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace NGEd.Tools
{
    internal static class Utils
    {
        public static bool LaunchAppFromPathWithArgs(string filename, string arguments)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo(path, arguments);
            if (!process.Start())
                return false;

            return true;
        }

        /// <summary>
        /// if true -> x64 else x86
        /// </summary>
        /// <returns></returns>
        public static bool IsX64Platform()
        {
            return IntPtr.Size == 8;
        }

        public static void OpenURL(string _url)
        {
            Process.Start(_url);
        }

        public static string OpenDirectoryDialog()
        {
            string _selectedFolder = "..//";
            var fbd = new FolderBrowserDialog();
            {
                var result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    // DEBUG: Editor: Clean: this only debug
                    //string[] files = Directory.GetFiles(fbd.SelectedPath);
                    //XtraMessageBox.Show("Files found: " + files.Length.ToString(), "Message");

                    _selectedFolder = fbd.SelectedPath;
                }
            }

            return _selectedFolder;
        }

        public static void CreateDirectory(string _path)
        {
            bool exists = System.IO.Directory.Exists(_path);

            if (!exists)
                System.IO.Directory.CreateDirectory(_path);
        }

        public static void CreateProjectDirectoryAndStructure(string _path, string _projectName)
        {
            // Создаем  корневую папку
            CreateDirectory(_path);
            // Создаем Bins папки
            CreateDirectory(_path + "/Bin64");
            CreateDirectory(_path + "/Bin32");
            MessageBox.Show("Files found: " + _path + " " + _projectName, "Message");
            // Создаем папку проекта
            CreateDirectory(_path + "/" + _projectName);
            // Создаем подпапки
            CreateDirectory(_path + "/" + _projectName + "/meshes");
            CreateDirectory(_path + "/" + _projectName + "/sounds");
            CreateDirectory(_path + "/" + _projectName + "/scripts");
            CreateDirectory(_path + "/" + _projectName + "/levels");
            CreateDirectory(_path + "/" + _projectName + "/materials");
            CreateDirectory(_path + "/" + _projectName + "/textures");
        }
    }
}