using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionConfig : MonoBehaviour
{
    public string curActionName;
    public int curActionID;

    // Start is called before the first frame update
    void Start()
    {
        curActionName = "null";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAction(string name,int id)
    {
        curActionName = name;
        curActionID = id;
    }
}
