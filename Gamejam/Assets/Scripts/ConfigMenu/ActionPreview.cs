using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPreview : MonoBehaviour
{
    Animator playerPreview, robotPreview;

    private void Awake()
    {
        playerPreview = this.transform.Find("PlayerActionPreview").GetComponent<Animator>();
        robotPreview = this.transform.Find("RobotActionPreview").GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPreviewAction(int actionID)
    {
        switch ((ActionType)actionID)
        {
            case (ActionType.WalkLeft):
                
                break;
            case (ActionType.WalkRight):
                playerPreview.SetTrigger("Run");
                //robotPreview.SetTrigger("Run");
                break;
            case (ActionType.Jump):
                playerPreview.SetTrigger("Jump");
                //robotPreview.SetTrigger("Jump");
                break;
            case (ActionType.Crouch):
                playerPreview.SetTrigger("Crouch");
                //robotPreview.SetTrigger("Crouch");
                break;
            case (ActionType.Attack):
                playerPreview.SetTrigger("Attack");
                //robotPreview.SetTrigger("Attack");
                break;
        }
    }

}
