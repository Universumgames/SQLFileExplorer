using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace SQLFileHandler
{
    class SQLDirectory
    {
        private static string EasyConsoleLine
        {
            get => EnviromentVariables.EasyConsoleLine;
            set => EnviromentVariables.EasyConsoleLine = value;
        }

        private SQLDirectoryInfo info = new SQLDirectoryInfo();

        private DirectoryInfo root { get => info.root; set => info.root = value; }
        private DirectoryInfo dataPath { get => info.dataPath; set => info.dataPath = value; }
        private DirectoryInfo fileDataPath { get => info.fileDataPath; set => info.fileDataPath = value; }
        private DirectoryInfo viewPath { get => info.viewPath; set => info.viewPath = value; }
        private DirectoryInfo importPath { get => info.importPath; set => info.importPath = value; }
        private FileInfo storagePath { get => info.storagePath; set => info.storagePath = value; }
        private XmlDocument storage { get => info.storage; set => info.storage = value; }

        private List<string> fileUUDs = new List<string>();

        public SQLDirectory(string path)
        {
            //this.info.root = new DirectoryInfo(path);
            this.dataPath = new DirectoryInfo(Path.Combine(path, Constants.relativeDataDirectory));
            this.fileDataPath = new DirectoryInfo(Path.Combine(path, Constants.relativeFileDataDirectory));
            this.viewPath = new DirectoryInfo(Path.Combine(path, Constants.relativeViewDirectory));
            this.importPath = new DirectoryInfo(Path.Combine(path, Constants.relativeImportDirectory));
            this.storagePath = new FileInfo(Path.Combine(path, Constants.relativeStorageFilePath));
            this.storage = new XmlDocument();
            storage.PreserveWhitespace = true;
        }

        public SQLDirectory(SQLDirectoryInfo info)
        {
            this.info = info;
        }

        public bool Init()
        {
            EasyConsoleLine = "Running initialization";
            if (!Helper.DirectoryExists(dataPath))
            {
                EasyConsoleLine = "Data path does not exist, treating directory as new SQL Directory";
                EasyConsoleLine = "Moving olde files in Directory to temporary directory";
                EasyConsoleLine = "If something goes wrong, files are located here: " + Path.Combine(Directory.GetCurrentDirectory(), Constants.relativeTmpDirectory);
                Helper.CreateDir(Constants.relativeTmpDirectory);
                Directory.Move(root.FullName, Constants.relativeTmpDirectory);

                EasyConsoleLine = "Deleting content of root directory";
                Directory.Delete(root.FullName);
                Helper.CreateDir(root.FullName);

                EasyConsoleLine = "Creating new Enviroment";
                Helper.CreateHiddenDir(dataPath.FullName);
                Helper.CreateDir(fileDataPath.FullName);
                Helper.CreateDir(importPath.FullName);
                Helper.CreateDir(viewPath.FullName);

                InitializeStorage();

                EasyConsoleLine = "Move old files into the import folder to prepare for import into data structure";
                Directory.Move(Constants.relativeTmpDirectory, importPath.FullName);
                Directory.Delete(Constants.relativeTmpDirectory);

                EasyConsoleLine = "Importing files, this may take some while...";
                Import();
                EasyConsoleLine = "Finished importing";
            }

            try
            {
                storage.Load(storagePath.FullName);
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
            if (!storagePath.Exists)
            {
                File.Create(storagePath.FullName);
                File.WriteAllText(storagePath.FullName, Constants.emptyStorageContent);
            }
        }

        #region Import
        public bool Import()
        {
            ImportProcessDirectory(importPath.FullName);
            return true;
        }

        public void ImportProcessDirectory(string inDir)
        {
            EasyConsoleLine = $"Gathering files in {inDir}...";
            string[] fileEntries = Directory.GetFiles(inDir);
            foreach (string filePath in fileEntries)
                ImportProcessFile(filePath);

            string[] subdirectoryEntries = Directory.GetDirectories(inDir);
            foreach (string subdirectory in subdirectoryEntries)
                ImportProcessDirectory(subdirectory);
        }

        public void ImportProcessFile(string path)
        {
            XMLFileInfo xmlInfo = GenerateXMLFileInfo(path);
            AddFileToXML(xmlInfo);
            
            File.Copy(path, xmlInfo.Data);
        }
        #endregion

        public XMLFileInfo GenerateXMLFileInfo(string path)
        {
            XMLFileInfo xmlInfo = new XMLFileInfo();
            FileInfo fileInfo = new FileInfo(path);

            xmlInfo.Filename = fileInfo.Name;
            xmlInfo.Created = Helper.DateTimeToString(fileInfo.CreationTime);
            xmlInfo.Data = Path.Combine(fileDataPath.FullName, xmlInfo.Filename);
            xmlInfo.View = viewPath.FullName;
            xmlInfo.LastCapturedEdition = Helper.DateTimeToString(fileInfo.LastWriteTime);
            return xmlInfo;
        }

        public void AddFileToXML(XMLFileInfo xmlInfo)
        {
            XmlNode fileNode = storage.CreateNode(XmlNodeType.Element, "file", "");
            foreach(KeyValuePair<string, string> infoPair in xmlInfo.info)
            {
                XmlNode infoNode = storage.CreateNode(XmlNodeType.Element, infoPair.Key, "");
                infoNode.InnerText = infoPair.Value;
                fileNode.AppendChild(infoNode);
            }
            
            storage["files"].AppendChild(fileNode);
        }

        public void ScanForUUIDs()
        {
            string[] fileEntries = Directory.GetFiles(dataPath.FullName);
            foreach (string filePath in fileEntries)
            {
                FileInfo fileIinfo = new FileInfo(filePath);
                fileUUDs.Add(fileIinfo.Name);
            }
        }

        public string GenerateFileUUID()
        {
            ScanForUUIDs();
            string uid;
            while (fileUUDs.Contains(uid = Guid.NewGuid().ToString())) { }
            return uid;
        }

        public static SQLDirectoryInfo LoadInfo(string filePath)
        {
            SQLDirectoryInfo info = new SQLDirectoryInfo();
            XmlDocument xmlData = new XmlDocument();
            xmlData.PreserveWhitespace = true;
            xmlData.Load(filePath);
            XmlNode statsNode = xmlData.GetElementsByTagName("stats")[0];
            info.storage = xmlData;
            info.storagePath = new FileInfo(filePath);
            foreach(XmlNode node in statsNode.ChildNodes)
            {
                string name = node.Name;
                string value = node.InnerText;
                switch (name)
                {
                    case "root":
                        info.root = new DirectoryInfo(value);
                        break;
                    case "data":
                        info.dataPath = new DirectoryInfo(value);
                        break;
                    case "view":
                        info.viewPath = new DirectoryInfo(value);
                        break;
                    case "filedata":
                        info.fileDataPath = new DirectoryInfo(value);
                        break;
                    case "import":
                        info.importPath = new DirectoryInfo(value);
                        break;
                    case "lastupdate":
                        info.lastupdate = DateTime.Parse(value);
                        break;
                    case "filecount":
                        info.filecount = long.Parse(value);
                        break;
                    case "customelements":
                        info.customElementCount = node.ChildNodes.Count;
                        List<string> entries = new List<string>();
                        foreach(XmlNode customNode in node.ChildNodes)
                        {
                            entries.Add(customNode.InnerText);
                        }
                        info.customElements = entries.ToArray();
                        break;
                }
            }
            return info;
        }
    }

    public struct SQLDirectoryInfo
    {
        public XmlDocument storage;
        public DirectoryInfo root;
        public DirectoryInfo dataPath;
        public DirectoryInfo viewPath;
        public DirectoryInfo fileDataPath;
        public DirectoryInfo importPath;
        public FileInfo storagePath;
        public DateTime lastupdate;
        public long filecount;
        public int customElementCount;
        public string[] customElements;
    }
}
