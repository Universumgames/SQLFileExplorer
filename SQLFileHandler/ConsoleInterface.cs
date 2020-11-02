using System;
using System.Collections.Generic;
using System.Text;

namespace SQLFileHandler
{
    static class ConsoleInterface
    {
        public static string WorkingDir
        {
            get => EnviromentVariables.workingDir;
            set => EnviromentVariables.workingDir = value;
        }

        public static SQLDirectory CurrentSQLDir
        {
            get => EnviromentVariables.currentSQLDir;
            set => EnviromentVariables.currentSQLDir = value;
        }

        private static string EasyConsoleLine
        {
            get => EnviromentVariables.EasyConsoleLine;
            set => EnviromentVariables.EasyConsoleLine = value;
        }

        private static string EasyConsole
        {
            get => EnviromentVariables.EasyConsole;
            set => EnviromentVariables.EasyConsole = value;
        }

        public static void PrintExecutor()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            EasyConsole = "SQLFileExplorer> ";
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Run()
        {
            EnviromentVariables.mode = RunningMode.Console;
            while (true)
            {
                PrintExecutor();
                string commandline = EasyConsoleLine;
                ConsoleCommandHandler(commandline);
            }
        }

        public static void ConsoleCommandHandler(string line)
        {
            EasyConsoleLine = "running command: " + line;
            string[] args = line.Split(" ");
            switch (args[0].ToLower())
            {
                case "help":
                    PrintHelp();
                    break;
                case "path":
                    if (args.Length == 2)
                    {
                        if (WorkingDir == "C:\\" || String.IsNullOrEmpty(WorkingDir) | String.IsNullOrWhiteSpace(WorkingDir))
                        {
                            EasyConsoleLine = "Working directory is not allowed";
                        }
                        else
                        {
                            WorkingDir = args[1];
                            CurrentSQLDir = null;
                            EasyConsoleLine = "Set working directory to " + WorkingDir;
                        }
                    }
                    else
                    {
                        EasyConsoleLine = "Working directory is " + WorkingDir;
                    }
                    break;
                case "init":
                    if (CurrentSQLDir == null)
                        CurrentSQLDir = new SQLDirectory(WorkingDir);
                    CurrentSQLDir.Init();
                    break;
                default:
                    EasyConsoleLine = "Command not found";
                    break;
            }

        }

        public static void PrintHelp()
        {
            string help = "Help information \n" +
                "\n" +
                "Following commands can be used here: \n" +
                "help: Displays this site \n" +
                "path <path>: Sets working directory, if path is not set the current workdirectory is printed";
            EasyConsoleLine = help;
        }
    }
}
