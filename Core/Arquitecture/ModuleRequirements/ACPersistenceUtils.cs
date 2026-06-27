using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using Cysharp.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Text;


namespace AshenCore.Core
{
    public enum LoadResult
    {
        Success,
        NoSaveFound,
        VersionMismatch,
        CorruptedData,
        UnknownError,
        Failed
    }

    public enum SaveResult
    {
        Success,
        VersionMismatch,
        SlotNotFound,
        CorruptedData,
        UnknownError,
        Failed
    }

    public enum ACSaveScope
    {
        Global,
        Profile,
        Scene
    }

    public enum ACSaveSystemType
    {
        UniqueSlot,
        MultiSlot,
        AutoSlot
    }


    public class ACSlot
    {
        public int slotId;
        public ACSaveMetaData metadata;
        public ACSaveFile saveFile;
        public string path; // Path to the save file on disk (if applicable)

    }


    public class ACSaveMetaData
    {
        public int slot;
        public string sceneName;
        public System.DateTime saveDate;
        public int version;

        // Additional metadata can be added here (e.g., playtime, player level, etc.)
    }

    public class ACSaveFile
    {
        public int slot;
        public Dictionary<string, object> data; // Key-value pairs for game data

        // You can also include methods for serialization/deserialization if needed
    }

    public class ACRuntimeFile
    {
        public int slot;
        public Dictionary<Guid, object> data; // Key-value pairs for game data

        // You can also include methods for serialization/deserialization if needed
    }


    public interface IGlobalPersistenceProvider
    {
        string Key { get; }

        object Capture();
        void Restore(object data);
    }

    public interface IRuntimePersistenceProvider
    {
        Guid Key { get; }

        object Capture();
        void Restore(object data);
    }



    public interface IACSerializer
    {
        string Serialize<T>(T data);
        T Deserialize<T>(string json);
    }

    public class ACJSONSerializer : IACSerializer
    {
        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }


    public static class ACFileSystem
    {
        public static async UniTask WriteFileAsync(string path, string content)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);

            using var fs = new FileStream(
                path,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                4096,
                useAsync: true
            );

            await fs.WriteAsync(bytes, 0, bytes.Length);
            await fs.FlushAsync();
        }

        public static async UniTask<string> LoadFileAsync(string path)
        {
            using var fs = new FileStream(
                path,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite,
                4096,
                useAsync: true
            );

            byte[] bytes = new byte[fs.Length];
            await fs.ReadAsync(bytes, 0, (int)fs.Length);

            return Encoding.UTF8.GetString(bytes);
        }

    }

}
