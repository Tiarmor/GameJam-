using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!!");
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    //取消设置并重置所有的界面显示
    public void CancelChange()
    {

    }

}
