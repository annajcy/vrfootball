using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ResourceManager : SingletonBase<ResourceManager>
{
    public static string GetUIPanelPath(string name) { return "UI/Panel/" + name; }
    public static string GetUIComponentPath(string name) { return "UI/Component/" + name; }
    public static string GetUICanvasPath(string name) { return "UI/Canvas/" + name; }
    public static string GetBGMPath(string name) { return "Music/BGM/" + name; }
    public static string GetSoundEffectPath(string name) { return "Music/SoundEffect/" + name; }
    public static string GetItemPicturePath(string name) { return "UI/ItemPicture/" + name; }
    
    public T Load<T>(string name) where T : Object
    {
        var resource = Resources.Load<T>(name);
        resource.name = name;
        return resource is GameObject ? Object.Instantiate(resource) : resource;
    }
    
    public T Load<T>(string name, string rename) where T : Object
    {
        var resource = Resources.Load<T>(name);
        resource.name = rename;
        return resource is GameObject ? Object.Instantiate(resource) : resource;
    }
    
    public void LoadAsync<T>(string name, UnityAction<T> action = null) where T : Object
    {
        MonoManager.Instance().StartCoroutine(LoadAsyncReal(name, action));
    }

    public void LoadAsync<T>(string name, string rename, UnityAction<T> action = null) where T : Object
    {
        MonoManager.Instance().StartCoroutine(LoadAsyncReal(name, rename, action));
    }

    private IEnumerator LoadAsyncReal<T>(string name, UnityAction<T> action = null) where T : Object
    {
        var request = Resources.LoadAsync<T>(name);
        yield return request;

        if (request.asset is GameObject)
        {
            var gameObject = Object.Instantiate(request.asset) as T;
            if (gameObject == null) yield break;
            gameObject.name = name;
            action?.Invoke(gameObject);
        }
        else
            action?.Invoke(request.asset as T);
    }

    private IEnumerator LoadAsyncReal<T>(string name, string rename, UnityAction<T> action = null) where T : Object
    {
        var request = Resources.LoadAsync<T>(name);
        yield return request;

        if (request.asset is GameObject)
        {
            var gameObject = Object.Instantiate(request.asset) as T;
            if (gameObject == null) yield break;
            gameObject.name = rename;
            action?.Invoke(gameObject);
        }
        else 
            action?.Invoke(request.asset as T);
    }
}