using System;
using System.Collections.Generic;
using System.Text;

namespace SQLFileHandler
{
    static class EnviromentVariables
    {
        public static string workingDir = "C:\\Users";
        public static SQLDirectory currentSQLDir;
        public static RunningMode mode;

        public static string EasyConsoleLine
        {
            get { return Console.ReadLine(); }
            set { Console.WriteLine(value); }
        }

        public static string EasyConsole
        {
            get { return Char.ToString((char)Console.Read()); }
            set { Console.Write(value); }
        }
    }

    enum RunningMode
    {
        Console,
        Background
    }
}
