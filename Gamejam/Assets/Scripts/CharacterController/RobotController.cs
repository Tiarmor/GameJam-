using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    Rigidbody2D thisrb;
    Animator animationController;
    BoxCollider2D myFeet;


    public delegate void ActionExcute();
    public ActionExcute[] actionLoop;
    int curAction;  //当前处于循环的哪一个位置

    public float runSpeed;
    public float jumpSpeed;
    bool isGround;

    private void Awake()
    {
        thisrb = GetComponent<Rigidbody2D>();
        animationController = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();

        //设置循环
        actionLoop = new ActionExcute[ActionManager.GetInstance().robotLoopLength];
        for(int i = 1; i < actionLoop.Length; i++)
        {
            actionLoop[i] = delegate () { };
            switch ((ActionType)ActionManager.GetInstance().robotAction[i])
            {
                case (ActionType.WalkLeft):
                    actionLoop[i] += WalkLeft;
                    break;
                case (ActionType.WalkRight):
                    actionLoop[i] += WalkRight;
                    break;
                case (ActionType.Jump):
                    actionLoop[i] += Jump;
                    break;
                case (ActionType.Crouch):
                    actionLoop[i] += Crouch;
                    break;
                case (ActionType.Attack):
                    actionLoop[i] += Attack;
                    break;
            }
        }
    }

    void Start()
    {
        runSpeed = 5f;
        jumpSpeed = 5f;
        curAction = 0;
        StartCoroutine("LoopTimer");
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        actionLoop[curAction]();
    }

    IEnumerator LoopTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            curAction = curAction + 1 >= actionLoop.Length ? 0 : curAction + 1;
        }
    }

    void WalkLeft()
    {
        thisrb.velocity = new Vector2(runSpeed * -1, thisrb.velocity.y);
        this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x) * -1, this.transform.localScale.y, this.transform.localScale.z);
        animationController.SetFloat("HorizontalSpeed", Mathf.Abs(thisrb.velocity.x));
    }

    void WalkRight()
    {
        thisrb.velocity = new Vector2(runSpeed, thisrb.velocity.y);
        this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        animationController.SetFloat("HorizontalSpeed", Mathf.Abs(thisrb.velocity.x));
    }

    void Jump()
    {
            //只有在地面上时才能跳跃
            if (isGround)
            {
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                thisrb.velocity = Vector2.up * jumpVel;
            }
    }

    void CheckGrounded()
    {
        //只有脚着地时才能跳跃
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    void Crouch()
    {
        animationController.SetBool("Crouch", true);
        animationController.SetBool("Idle", false);
    }


    void Attack()
    {
        animationController.SetTrigger("Attack");
        animationController.SetBool("Idle", false);
        animationController.SetBool("Crouch", false);
    }


}
