using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Text;
public class DataManager : DontDestroy<DataManager>
{
    public static PlayData playData;

    public List<ItemData> itemDatas = new List<ItemData>();

    public static void SaveData()
    {
        string jsonData = ObjectToJson(playData);
        var jtc2 = JsonToOject<PlayData>(jsonData);
        if (!Application.isEditor)
            CreateJsonFile(Application.persistentDataPath, "PlayData", jsonData);
        else
            CreateJsonFile(Application.dataPath, "Resources/PlayData", jsonData);
    }

    public static bool HasData()
    {
        string filePath = Application.persistentDataPath + "/PlayData.json";
        if (Application.isEditor)
        {
            filePath = Application.dataPath + "/Resources/PlayData.json";
            return File.Exists(filePath);
        }
        else
            return File.Exists(filePath);
    }

    public static void LoadData()
    {
        string filePath = Application.persistentDataPath + "/PlayData.json";
        if (Application.isEditor)
        {
            filePath = Application.dataPath + "/Resources/PlayData.json";
            if (File.Exists(filePath))
            {
                var jtc = LoadJsonFile<PlayData>(Application.dataPath, "Resources/PlayData");
                playData = jtc;
            }
            else
            {

                playData = new PlayData();
                string jsonData = ObjectToJson(playData);
                var jtc = JsonToOject<PlayData>(jsonData);
                CreateJsonFile(Application.dataPath, "Resources/PlayData", jsonData);
                jtc = LoadJsonFile<PlayData>(Application.dataPath, "Resources/PlayData");
                playData = jtc;
            }
        }
        else
        {
            if (File.Exists(filePath))
            {
                var jtc = LoadJsonFile<PlayData>(Application.persistentDataPath, "PlayData");
                playData = jtc;
            }
            else
            {

                playData = new PlayData();
                string jsonData = ObjectToJson(playData);
                var jtc = JsonToOject<PlayData>(jsonData);
                CreateJsonFile(Application.persistentDataPath, "PlayData", jsonData);
                jtc = LoadJsonFile<PlayData>(Application.persistentDataPath, "PlayData");
                playData = jtc;
            }
        }
    }

    public static ItemData GetItem(ItemData.Type type)
    {
        foreach (ItemData item in instance.itemDatas)
            if (item.ItemType == type)
                return item;
        return null;
    }

    #region[JSON °ü¸®]
    private static string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    private static T JsonToOject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    private static void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    private static T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(jsonData);
    }

    [System.Serializable]
    public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
    {
        [SerializeField]
        List<TKey> keys;
        [SerializeField]
        List<TValue> values;

        Dictionary<TKey, TValue> target;
        public Dictionary<TKey, TValue> ToDictionary() { return target; }

        public Serialization(Dictionary<TKey, TValue> target)
        {
            this.target = target;
        }

        public void OnBeforeSerialize()
        {
            keys = new List<TKey>(target.Keys);
            values = new List<TValue>(target.Values);
        }

        public void OnAfterDeserialize()
        {
            var count = Mathf.Min(keys.Count, values.Count);
            target = new Dictionary<TKey, TValue>(count);
            for (var i = 0; i < count; ++i)
            {
                target.Add(keys[i], values[i]);
            }
        }
    }

    [System.Serializable]
    public class Serialization<T>
    {
        [SerializeField]
        List<T> target;
        public List<T> ToList() { return target; }

        public Serialization(List<T> target)
        {
            this.target = target;
        }
    }
    #endregion
}