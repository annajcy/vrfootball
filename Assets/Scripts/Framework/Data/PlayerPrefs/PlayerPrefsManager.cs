using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public class PlayerPrefsManager : SingletonBase<PlayerPrefsManager>
{
    /// <summary>
    /// Save data
    /// </summary>
    /// <param name="data">Data object</param>
    /// <param name="key">Unique key</param>
    public void Save(string key, object data)
    {
        var dataType = data.GetType();
        var dataFields = dataType.GetFields();
        foreach (var field in dataFields)
            SaveValue(GetKeyName(key, dataType, field), field.GetValue(data));
    }

    /// <summary>
    /// Load data
    /// </summary>
    /// <param name="key">Unique key</param>
    /// <param name="type">Data type</param>
    /// <returns>Get saved data as an object</returns>
    public object Get(string key, Type type)
    {
        var data = Activator.CreateInstance(type);
        var dataFields = type.GetFields();
        foreach (var field in dataFields)
            field.SetValue(data, GetValue(GetKeyName(key, type, field), field.FieldType));
        return data;
    }

    /// <summary>
    /// Check basic type
    /// </summary>
    /// <param name="type">type</param>
    /// <returns>Is basic type</returns>
    private bool IsBasicType(Type type)
    {
        return type == typeof(int) || 
               type == typeof(float) ||
               type == typeof(string) ||
               type == typeof(bool);
    }
    
    /// <summary>
    /// Check list type
    /// </summary>
    /// <param name="type">type</param>
    /// <returns>Is list type</returns>

    private bool IsListType(Type type)
    {
        return typeof(IList).IsAssignableFrom(type);
    }
    
    /// <summary>
    /// Check dictionary type
    /// </summary>
    /// <param name="type">type</param>
    /// <returns>Is dictionary type</returns>
    private bool IsDictType(Type type)
    {
        return typeof(IDictionary).IsAssignableFrom(type);
    }

    /// <summary>
    /// Get Full key name
    /// </summary>
    /// <param name="key"></param>
    /// <param name="type"></param>
    /// <param name="info"></param>
    /// <returns></returns>
    private string GetKeyName(string key, Type type, FieldInfo info)
    {
        return key + "_" + type.Name + "_" + info.FieldType.Name + "_" + info.Name;
    }

    /// <summary>
    /// Save single piece of data according to type of value
    /// If using custom class, save it recursively 
    /// </summary>
    /// <param name="key">Unique key</param>
    /// <param name="value">Value</param>
    private void SaveValue(string key, object value)
    {
        var type = value.GetType();
        if (IsBasicType(type)) SaveBasic(key, value);
        else if (IsListType(type)) SaveList(key, value as IList);
        else if (IsDictType(type)) SaveDict(key, value as IDictionary);
        else Save(key, value);
    }
    
    /// <summary>
    /// Save basic data that can be directly used by PlayerPrefs
    /// </summary>
    /// <param name="key">Unique key</param>
    /// <param name="value">Value</param>
    private void SaveBasic(string key, object value)
    {
        var type = value.GetType();
        if (type == typeof(int)) PlayerPrefs.SetInt(key, (int)value);
        else if (type == typeof(float)) PlayerPrefs.SetFloat(key, (float)value);
        else if (type == typeof(string)) PlayerPrefs.SetString(key, value.ToString());
        else if (type == typeof(bool)) PlayerPrefs.SetInt(key, (bool)value ? 1 : 0);
    }
    
    /// <summary>
    /// Save list
    /// </summary>
    /// <param name="key">Unique key</param>
    /// <param name="list">List</param>
    private void SaveList(string key, IList list)
    {
        PlayerPrefs.SetInt(key, list.Count);
        int i = 0;
        foreach (var obj in list)
        {
            SaveValue(key + "_" + i, obj);
            i++;
        }
    }
    
    /// <summary>
    /// Save dictionary
    /// </summary>
    /// <param name="key">Unique key</param>
    /// <param name="dict">List</param>
    private void SaveDict(string key, IDictionary dict)
    {
        PlayerPrefs.SetInt(key, dict.Count);
        int i = 0;
        foreach (var keyObj in dict.Keys)
        {
            SaveValue(key + "_key_" + i, keyObj);
            SaveValue(key + "_value_" + i, dict[keyObj]);
            i++;
        }
    }
    

    /// <summary>
    /// Get single piece of data according to type of value
    /// If using custom class, get it recursively 
    /// </summary>
    /// <param name="key">Unique key</param>
    /// <param name="type">Target Type</param>
    /// <returns>Get saved data as an object</returns>
    private object GetValue(string key, Type type)
    {
        if (IsBasicType(type)) return GetBasic(key, type);
        if (IsListType(type)) return GetList(key, type);
        if (IsDictType(type)) return GetDict(key, type);
        return Get(key, type);
    }
    
    /// <summary>
    /// Get basic type data
    /// </summary>
    /// <param name="key">Unique key</param>
    /// <param name="type">Type</param>
    /// <returns>Get saved basic data as an object</returns>
    private object GetBasic(string key, Type type)
    {
        if (type == typeof(int)) return PlayerPrefs.GetInt(key);
        if (type == typeof(float)) return PlayerPrefs.GetFloat(key);
        if (type == typeof(string)) return PlayerPrefs.GetString(key);
        if (type == typeof(bool)) return PlayerPrefs.GetInt(key) == 1;
        return null;
    }

    /// <summary>
    /// Get list type data
    /// </summary>
    /// <param name="key">Unique key</param>
    /// <param name="type">type</param>
    /// <returns>Get saved list data as an object</returns>
    private object GetList(string key, Type type)
    {
        var count = PlayerPrefs.GetInt(key);
        var list = Activator.CreateInstance(type) as IList;
        if (list == null) return null;
        var genericType = type.GetGenericArguments()[0];
        for (int i = 0; i < count; i++)
            list.Add(GetValue(key + "_" + i, genericType));
        return list;
    }
    
    /// <summary>
    /// Get dict type data
    /// </summary>
    /// <param name="key">Unique key</param>
    /// <param name="type">Type</param>
    /// <returns>Get saved dict data as an object</returns>
    private object GetDict(string key, Type type)
    {
        var count = PlayerPrefs.GetInt(key);
        var dict = Activator.CreateInstance(type) as IDictionary;
        if (dict == null) return null;
        var genericKeyType = type.GetGenericArguments()[0];
        var genericValueType = type.GetGenericArguments()[1];
        for (int i = 0; i < count; i++)
        {
            var keyObj = GetValue(key + "_key_" + i, genericKeyType);
            var valueObj = GetValue(key + "_value_" + i, genericValueType);
            dict.Add(keyObj, valueObj);
        }
        return dict;
    }
}
