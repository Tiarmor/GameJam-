using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;

    //分别控制背景音乐和音效
    AudioSource musicController;
    AudioSource soundController;
    public AudioMixer audioOutput;
    float vol, defaultVol;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        AudioSource[] sources = this.GetComponents<AudioSource>();
        musicController = sources[0];
        soundController = sources[1];
    }

    // Start is called before the first frame update
    void Start()
    {
        
        vol = defaultVol;
        audioOutput.SetFloat("MasterVolume", vol);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioManager GetInstance()
    {
        return instance;
    }

    //选择BGM播放
    public void MusiePlay(string musicName)
    {
        musicController.Stop();
        musicController.clip = Resources.Load<AudioClip>("Audio/Music/" + musicName);
        musicController.Play();
    }

    //选择音效播放
    public void SoundPlay(string soundName)
    {
        soundController.Stop();
        soundController.clip = Resources.Load<AudioClip>("Audio/Music/" + soundName);
        soundController.Play();
    }

    
    //设置主音量
    public void SetMasterVolume(float volume)
    {
        vol = volume;
        audioOutput.SetFloat("MasterVolume", vol);
    }

    //设置背景音乐音量
    public void SetMusicVolume(float volume)
    {
        vol = volume;
        audioOutput.SetFloat("MusicVolume", volume);
    }

    //设置场景特效音量
    public void SetSoundVolume(float volume)
    {
        vol = volume;
        audioOutput.SetFloat("SoundVolume", vol);
    }

}
