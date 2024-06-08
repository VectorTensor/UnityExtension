using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SaveAndLoad
{
    public class FileDataService:IDataService
    {
        private ISerializer serializer;
        private string dataPath;
        private string fileExtension;


        public FileDataService(ISerializer serializer)
        {
            
            this.dataPath = Application.persistentDataPath;
            this.fileExtension = ".json";
            this.serializer = serializer;
            
        }
        
        string GetPathToFile(string filename)
        {

            return Path.Combine(dataPath, string.Concat(filename, ".", fileExtension));

        }
        public void Save(GameData data, bool overwrite = true)
        {

            throw new System.NotImplementedException();

        }


        public GameData Load(string name)
        {
            
            throw new System.NotImplementedException();
        }

        public void Delete(string name)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> ListSaves()
        {
            throw new System.NotImplementedException();
        }
    }
}