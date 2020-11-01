using System;
using System.Collections.Generic;
using System.Text;

namespace SQLFileHandler
{
    static class ConsoleInterface
    {
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

        public static void PrintExecutor()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            EasyConsole = "SQLFileExplorer> ";
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Run()
        {
            while (true)
            {
                PrintExecutor();
                string commandline = EasyConsoleLine;
                EasyConsoleLine = commandline;
            }
        }
    }
}
