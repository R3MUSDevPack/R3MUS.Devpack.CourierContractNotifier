using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using R3MUS.Devpack.CourierContractNotifier.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace R3MUS.Devpack.CourierContractNotifier.Services
{
    public class DataStorageService : IDataStorageService
    {
        private readonly string _directoryPath;
        private readonly string _filePath;

        public DataStorageService()
        {
            _directoryPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"\Data\");
            _filePath = string.Concat(_directoryPath, @"data.json");
            CheckPaths();
        }

        public DataStore LoadDataStore()
        {
            if (File.Exists(_filePath))
            {
                var serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;

                using (var streamReader = new StreamReader(_filePath))
                {
                    using (var textReader = new JsonTextReader(streamReader))
                    {
                        return serializer.Deserialize<DataStore>(textReader);
                    }
                }
            }
            return new DataStore() { Data = new List<DataItem>() };
        }

        public void SaveDataStore(DataStore data)
        {
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (var streamWriter = new StreamWriter(_filePath))
            {
                using (var textWriter = new JsonTextWriter(streamWriter))
                {
                    serializer.Serialize(textWriter, data);
                }
            }
        }

        private void CheckPaths()
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
        }
    }
}
