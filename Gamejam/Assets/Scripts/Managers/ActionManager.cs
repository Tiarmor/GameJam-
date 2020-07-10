using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ActionManager : MonoBehaviour
{
    static ActionManager instance;

    public int playerActNum, robotLoopLength;   //设置动作的长度

    public int[] playerAction;  //玩家的配置动作编号
    public int[] robotAction;   //机器人的配置动作编号

    private void Awake()
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
        playerActNum = 4;
        robotLoopLength = 3;
        playerAction = new int[playerActNum];
        robotAction = new int[robotLoopLength];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static ActionManager GetInstance()
    {
        return instance;
    }

    //设置人物动作
    public void SetPlayerAction()
    {

    }

    //设置机器人动作
    public void SetRobotAction()
    {

    }

}

public enum ActionType : int
{
    WalkLeft=0,
    WalkRight=1,
    Jump=2,
    Crouch=3,
    Attack=4
}
