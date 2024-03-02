using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicManager : SingletonBase<MusicManager>
{
    private AudioSource bgm;
    private float bgmVolume = 1;

    private GameObject soundEffectObject;
    private float soundEffectVolume = 1;
    private readonly List<AudioSource> soundEffectList = new();

    public MusicManager()
    {
        MonoManager.Instance().AddUpdateListener(() =>
        {
            for (int i = soundEffectList.Count - 1; i >= 0; i--)
            {
                if (soundEffectList[i].isPlaying) continue;
                Object.Destroy(soundEffectList[i]);
                soundEffectList.RemoveAt(i);
            }
        });
    }

    public void PlayBGM(string name)
    {
        if (bgm == null)
        {
            var gameObject = new GameObject { name = "BGMPlayer" };
            bgm = gameObject.AddComponent<AudioSource>();
        }

        ResourceManager.Instance().LoadAsync<AudioClip>(ResourceManager.GetBGMPath(name), clip =>
        {
            bgm.clip = clip;
            bgm.loop = true;
            bgm.Play();
        });
    }

    public void PauseBGM()
    {
        if (bgm == null) return;
        bgm.Pause();
    }

    public void StopBGM()
    {
        if (bgm == null) return;
        bgm.Stop();
    }

    public void ChangeBGMVolume(float value)
    {
        bgmVolume = value;
        if (bgm == null) return;
        bgm.volume = bgmVolume;
    }

    public void PlaySoundEffect(string name, bool isLoop = false, UnityAction<AudioSource> callback = null)
    {
        if (soundEffectObject == null)
            soundEffectObject = new GameObject { name = "SoundEffect" };


        ResourceManager.Instance().LoadAsync<AudioClip>(ResourceManager.GetSoundEffectPath(name), clip =>
        {
            AudioSource soundEffectMusic = soundEffectObject.AddComponent<AudioSource>();
            soundEffectMusic.clip = clip;
            soundEffectMusic.loop = isLoop;
            soundEffectMusic.volume = soundEffectVolume;
            soundEffectList.Add(soundEffectMusic);
            callback?.Invoke(soundEffectMusic);
        });
    }

    public void ChangeSoundEffectVolume(float value)
    {
        soundEffectVolume = value;
        foreach (var audioSource in soundEffectList)
            audioSource.volume = soundEffectVolume;
    }

    public void StopSound(AudioSource audioSource)
    {
        if (!soundEffectList.Contains(audioSource)) return;
        soundEffectList.Remove(audioSource);
        audioSource.Stop();
        Object.Destroy(audioSource);
    }
}
