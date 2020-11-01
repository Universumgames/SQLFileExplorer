using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SQLFileHandler
{
    static class Constants
    {

        public const string relativeDataDirectory = ".\\data\\";
        public const string relativeFileDataDirectory = ".\\data\\files\\";
        public const string relativeViewDirectory = ".\\view\\";
        public const string relativeImportDirectory = ".\\import\\";

        public const string relativeTmpDirectory = "..\\tmpSQLData";

        public const string relativeStorageFilePath = relativeDataDirectory + "data.xml";

        public const string emptyStorageContent =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>"+
            "<SQLFileSystem lastRun=\"1970-01-01\">"+
            "<stats>"+
            "</stats>"+
            "<files>"+
            "</files>"+
            "</SQLFileSystem>";
    }
}
