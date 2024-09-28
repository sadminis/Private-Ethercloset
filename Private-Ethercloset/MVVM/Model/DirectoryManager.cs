using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Private_Ethercloset.MVVM.Model
{
    public static class DirectoryManager
    {
        public static string createAppDirectory()
        {
            string appDirectory = Path.Combine(Environment.CurrentDirectory, "Gallery");

            if (!Directory.Exists(appDirectory))
            {
                Directory.CreateDirectory(appDirectory);
            }

            return appDirectory;
        }
    }
}
