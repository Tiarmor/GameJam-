using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    GameObject pausePanel;

    private void Awake()
    {
        pausePanel = GameObject.Find("Canvas").transform.Find("PausePanel").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            pausePanel.SetActive(Time.timeScale == 0);
        }
    }
}
