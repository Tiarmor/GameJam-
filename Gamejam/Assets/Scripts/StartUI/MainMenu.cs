using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    Slider volSlider;

    private void Awake()
    {
        volSlider = this.transform.parent.Find("Options").Find("Slider").GetComponent<Slider>();
        volSlider.maxValue = 0;
        volSlider.minValue = -80;
    }

    private void Start()
    {
        volSlider.value = AudioManager.GetInstance().defaultVol;
    }

    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //场景切换携程
    /*IEnumerator SceneChange()
    {

    }*/

    public void QuitGame()
    {
        Debug.Log("Quit!!");
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void SaveVolumeSetting()
    {
        AudioManager.GetInstance().SetMasterVolume(volSlider.value);
    }

    //取消设置并重置所有的界面显示
    public void CancelChange()
    {
        volSlider.value = AudioManager.GetInstance().vol;
    }

}
