using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace SQLFileHandler
{
    class SQLDirectory
    {
        private string root;
        private string dataPath;
        private string fileDataPath;
        private string viewPath;
        private string importPath;
        private string storagePath;

        private XmlDocument storage;

        public SQLDirectory(string path)
        {
            this.root = path;
            this.dataPath = Path.Combine(path, Constants.relativeDataDirectory);
            this.fileDataPath = Path.Combine(path, Constants.relativeFileDataDirectory);
            this.viewPath = Path.Combine(path, Constants.relativeViewDirectory);
            this.importPath = Path.Combine(path, Constants.relativeImportDirectory);
            this.storagePath = Path.Combine(path, Constants.relativeStorageFilePath);
            this.storage = new XmlDocument();
            storage.PreserveWhitespace = true;

            init();
        }

        public bool init()
        {
            if (!Helper.DirectoryExists(dataPath))
            {
                Helper.CreateDir(Constants.relativeTmpDirectory);
                Directory.Move(root, Constants.relativeTmpDirectory);

                Directory.Delete(root);
                Helper.CreateDir(root);

                Helper.CreateHiddenDir(dataPath);
                Helper.CreateDir(fileDataPath);
                Helper.CreateDir(importPath);
                Helper.CreateDir(viewPath);

                InitializeStorage();

                Directory.Move(Constants.relativeTmpDirectory, importPath);
                Directory.Delete(Constants.relativeTmpDirectory);

                import();
            }

            try
            {
                storage.Load(storagePath);
            }
            catch
            {
                storage.LoadXml(Constants.emptyStorageContent);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Storage file could not be loaded");
                Console.ForegroundColor= ConsoleColor.White;
            }

            foreach (XmlNode node in storage.DocumentElement.ChildNodes)
            {
                string text = node.InnerText; //or loop through its children as well
                Console.WriteLine(text);
            }

            return false;
        }

        public void InitializeStorage()
        {
            if (!File.Exists(storagePath))
            {
                File.Create(storagePath);
                File.WriteAllText(storagePath, Constants.emptyStorageContent);
            }
        }

        #region Import
        public bool import()
        {
            ImportProcessDirectory(importPath);
            return true;
        }

        public void ImportProcessDirectory(string inDir)
        {
            string[] fileEntries = Directory.GetFiles(inDir);
            foreach (string filePath in fileEntries)
                ImportProcessFile(filePath);

            string[] subdirectoryEntries = Directory.GetDirectories(inDir);
            foreach (string subdirectory in subdirectoryEntries)
                ImportProcessDirectory(subdirectory);
        }

        public void ImportProcessFile(string path)
        {
            string fileName = path.Substring(path.LastIndexOf("\\"));
            //ToDo gather data about file and add it into permanent storage system
            File.Copy(path, Path.Combine(fileDataPath, fileName));
        }
        #endregion
    }
}
