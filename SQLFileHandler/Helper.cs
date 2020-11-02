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

        public static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public static bool DirectoryExists(DirectoryInfo info)
        {
            return info.Exists;
        }

        public static string DateTimeToString(DateTime dateTime)
        {
            return dateTime.ToLocalTime().ToString(Constants.datePatt);
        }

        public static DateTime StringToDateTime(string dateTime)
        {
            return DateTime.Parse(dateTime);
        }
    }
}
