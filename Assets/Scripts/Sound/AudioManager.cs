using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Background
{
    Bongo_Madness = 0,
    Lovable_Clown_Sit_Com,
    Safety_Net,
    clear,
    End
}

public enum SoundClip
{
    click = Background.End, // => BackgroundType 파일 끝에 이어서 번호가 붙게 됨
    death,
    floor_change,
    footstep_concrete_land_01,
    footstep_metal_low_walk_01,
    head_change,
    jump,
    key,
    laser,
    swap
}

public class AudioManager : MonoBehaviour
{
    AudioSource background;
    AudioSource effectSound;
    AudioSource effectSound2;
    AudioListener listener;
    float volume; // 전체적인 사운드 볼륨 제어, Audio Listener에 사용할 값

    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("AudioManager", typeof(AudioManager));
                instance = obj.GetComponent<AudioManager>();

                instance.background = obj.AddComponent<AudioSource>();
                instance.AudioSetting(instance.background);
                instance.effectSound = obj.AddComponent<AudioSource>();
                instance.AudioSetting(instance.effectSound);
                instance.effectSound2 = obj.AddComponent<AudioSource>();
                instance.AudioSetting(instance.effectSound2);

                //AudioClip clip = Resources.Load<AudioClip>("");

                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    // 사운드 출력 설정 함수
    void AudioSetting(AudioSource audio, bool loop = false)
    {
        if (audio == null)
            return;

        audio.loop = loop;
        audio.playOnAwake = true;
        audio.spatialBlend = 0; // 공간감 0(2D) ~ 1(3D)
        audio.priority = 0;
        audio.pitch = 1;
        audio.panStereo = 0;
        audio.volume = volume;
    }

    public void PlayBG(AudioSource source, string soundType, bool loop = false, float volume = 1.0f)
    {
        if (background != null)
        {
            if (audioClips.ContainsKey(soundType))
            {
                background.clip = audioClips[soundType];
                background.volume = volume;
                background.loop = loop;
                background.Play();
            }
        }
    }

    public void PlayEF(AudioSource source, string soundType, bool loop = false, float volume = 1.0f)
    {
        if (effectSound != null)
        {
            if (audioClips.ContainsKey(soundType))
            {
                effectSound.clip = audioClips[soundType];
                effectSound.volume = volume;
                effectSound.loop = loop;
                effectSound.Play();
            }
        }
    }

    public void PlayEF2(AudioSource source, string soundType, bool loop = false, float volume = 1.0f)
    {
        if (effectSound2 != null)
        {
            if (audioClips.ContainsKey(soundType))
            {
                effectSound2.clip = audioClips[soundType];
                effectSound2.volume = volume;
                effectSound2.loop = loop;
                effectSound2.Play();
            }
        }
    }

    public void PlayBackground(string sType, bool loop = true, float volume = 1.0f)
    {
        PlayBG(background, sType, loop, volume);
    }

    public void PlayBackground(Background bgType, bool loop = true, float volume = 1.0f)
    {
        PlayBG(background, bgType.ToString(), loop, volume);
    }

    public void PlayEffect(string soundType, bool loop = false, float volume = 1.0f)
    {
        PlayEF(effectSound, soundType, loop, volume);
    }

    public void PlayEffect(SoundClip soundType, bool loop = false, float volume = 1.0f)
    {
        PlayEF(effectSound, soundType.ToString(), loop, volume);
    }

    public void PlayEffect2(string soundType, bool loop = false, float volume = 1.0f)
    {
        PlayEF2(effectSound2, soundType, loop, volume);
    }

    public void PlayEffect2(SoundClip soundType, bool loop = false, float volume = 1.0f)
    {
        PlayEF2(effectSound2, soundType.ToString(), loop, volume);
    }

    public void AddClip(string soundType, string path)
    {
        if (!audioClips.ContainsKey(soundType))
        {
            AudioClip clip = Resources.Load<AudioClip>(path);
            if (clip != null)
                audioClips.Add(soundType, clip);
        }
    }

    public void LoadClip<T>(string path)
    {
        T[] files = EnumHelper.GetValues<T>();

        for (int i = 0; i < files.Length; i++)
            AddClip(files[i].ToString(), path + files[i].ToString());
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetPause(bool state)
    {
        AudioListener.pause = state;
    }

    // 특정 사운드 파일의 볼륨이 유독 크거나 작을 경우, 따로 관리하는 함수
    public void SetEffectVolume(string path, float volume)
    {
        //if (sourceDic.ContainsKey(path))
        //    sourceDic[path].volume = volume;
    }
}
