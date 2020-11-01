using System;

namespace SQLFileHandler
{

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) ConsoleInterface.Run();
            else BackgroundInterface.Run(args);
            
            Console.WriteLine("Press any key to coninue...");
            Console.ReadLine();
        }
    }
}
