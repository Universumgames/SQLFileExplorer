using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SQLFileHandler
{
    static class Helper
    {
        public static void CreateHiddenDir(string path)
        {
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
        }

        public static void CreateDir(string path)
        {
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                di.Attributes = FileAttributes.Directory;
            }
        }

        public static Boolean DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }
    }
}
