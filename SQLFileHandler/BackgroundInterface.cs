using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SQLFileHandler
{

    static class BackgroundInterface
    {

        public static void Run(string[] args)
        {
            EnviromentVariables.mode = RunningMode.Background;
            //Helper.createHiddenDir(@".\\testDir");
        }

        
    }

    struct SortOptions
    {
        string column;
    }
}
