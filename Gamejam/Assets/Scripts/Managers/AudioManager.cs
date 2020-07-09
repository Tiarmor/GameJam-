using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //分别控制背景音乐和音效
    AudioSource musicController;
    AudioSource soundController;
    int vol;

    void Awake()
    {
        AudioSource[] sources = this.GetComponents<AudioSource>();
        musicController = sources[0];
        soundController = sources[1];
    }

    // Start is called before the first frame update
    void Start()
    {
        vol = PlayerPrefs.GetInt("Volume");
    }

    // Update is called once per frame
    void Update()
    {
        
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

}
