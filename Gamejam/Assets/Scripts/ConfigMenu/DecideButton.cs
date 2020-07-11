using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DecideButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    Text buttonText;
    Vector3 originTextPos;
    Color originTextColor;

    ActionConfig[] playerConfig;
    ActionConfig[] robotConfig;

    GameObject errorPanel;
    void Awake()
    {
        buttonText = this.transform.Find("Text").GetComponent<Text>();
        originTextPos = buttonText.transform.position;
        originTextColor = buttonText.color;

        errorPanel = this.transform.parent.Find("ErrorPanel").gameObject;

        //获取顺序
        playerConfig = this.transform.parent.Find("PlayerAction").GetComponentsInChildren<ActionConfig>();
        robotConfig = this.transform.parent.Find("RobotLoop").GetComponentsInChildren<ActionConfig>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = new Color(155f / 255f, 155f / 255f, 155f / 255f, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = originTextColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonText.transform.position = originTextPos + new Vector3(3, -3, 0);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonText.transform.position = originTextPos;
        //将数组内数据放入ActionManager类中

        ActionManager.GetInstance().robotAction = new int[robotConfig.Length];
        //如果机器人的循环有空，无法继续
        for(int i = 0; i < robotConfig.Length; i++)
        {
            if (robotConfig[i].curActionName.Equals("null"))
            {
                errorPanel.SetActive(true);
                return;
            }
            ActionManager.GetInstance().robotAction[i] = robotConfig[i].curActionID;
        }

        ActionManager.GetInstance().playerAction = new int[playerConfig.Length];
        for (int i = 0; i < playerConfig.Length; i++)
        {
            ActionManager.GetInstance().playerAction[i] = playerConfig[i].curActionName.Equals("null") ? playerConfig[i].curActionID : 10;  //未设置的设为10，不对应操作
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
