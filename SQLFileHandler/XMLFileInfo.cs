using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SQLFileHandler
{
    public class XMLFileInfo
    {
        /*public string owner { 
            get { return info["owner"]; }
            set { info["owner"] = value; }
        }*/
        
        public string Filename
        {
            get => info["filename"];
            set => info["filename"] = value;
        }

        public string Created
        {
            get => info["created"];
            set => info["created"] = value;
        }

        public string Data
        {
            get => info["data"];
            set => info["data"] = value;
        }

        public string View
        {
            get => info["view"];
            set => info["view"] = value;
        }

        public string LastCapturedEdition
        {
            get => info["lastcapturededition"];
            set => info["lastcapturededition"] = value;
        }

        public void SetCustom(string key, string value)
        {
            info[key] = value;
        }

        public string GetValue(string key)
        {
            if(info.ContainsKey(key))
                return info[key];
            return "";
        }

        public Dictionary<string, string> info = new Dictionary<string, string>();
    }
}
