using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneSwitchManager : SingletonBase<SceneSwitchManager>
{
    public void Load(string name, UnityAction action = null)
    {
        SceneManager.LoadScene(name);
        action?.Invoke();
    }

    public void LoadAsync(string name, UnityAction action = null)
    {
        MonoManager.Instance().StartCoroutine(LoadAsyncReal(name, action));
    }

    private IEnumerator LoadAsyncReal(string name, UnityAction action = null)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(name);
        while (!asyncOperation.isDone)
        {
            EventManager.Instance().EventTrigger("LoadingUpdate", asyncOperation.progress);
            yield return asyncOperation.progress;
        }
        action?.Invoke();
    }
}