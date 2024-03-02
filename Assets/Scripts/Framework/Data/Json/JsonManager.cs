using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class JsonManager : SingletonBase<JsonManager>
{
    /// <summary>
    /// Where to load
    /// </summary>
    public enum ELoadLocation
    {
        PersistentData,
        StreamingAssets,
    }

    /// <summary>
    /// Get persistent data path
    /// </summary>
    /// <param name="fileName">file name</param>
    /// <returns>file name</returns>
    private string GetPersistentDataPath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName + ".json";
    }

    /// <summary>
    /// Get streaming assets path
    /// </summary>
    /// <param name="fileName">file name</param>
    /// <returns>file name</returns>
    private string GetStreamingAssetsPath(string fileName)
    {
        return Application.streamingAssetsPath + "/" + fileName + ".json";
    }

    /// <summary>
    /// Save data
    /// </summary>
    /// <param name="data">object to save</param>
    /// <param name="fileName">file name</param>
    public void Save(object data, string fileName)
    {
        File.WriteAllText(GetPersistentDataPath(fileName),
            JsonMapper.ToJson(data));
    }

    /// <summary>
    /// Load data from certain path
    /// And convert it to an object
    /// </summary>
    /// <param name="fileName">file name</param>
    /// <param name="location">where to load</param>
    /// <typeparam name="T">the type of the object to load</typeparam>
    /// <returns>loaded object</returns>
    public T Load<T>(string fileName, ELoadLocation location) where T: new()
    {
        string path = "";
        if (location == ELoadLocation.StreamingAssets) path = GetStreamingAssetsPath(fileName);
        else if (location == ELoadLocation.PersistentData) path = GetPersistentDataPath(fileName);
        return !File.Exists(path) ? new T() : JsonMapper.ToObject<T>(File.ReadAllText(path));
    }
    
    public T Load<T>(string fileName) where T: new()
    {
        return Load<T>(
            fileName, 
            File.Exists(GetPersistentDataPath(fileName)) ? 
                ELoadLocation.PersistentData : ELoadLocation.StreamingAssets);
    }
}
