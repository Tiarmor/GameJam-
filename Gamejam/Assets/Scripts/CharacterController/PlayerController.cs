using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float runSpeed;
    public float jumpSpeed;
    float lastHor;
    float gravity;

    Rigidbody2D myRigidbody;
    Animator myAnim;
    BoxCollider2D myFeet;
    public bool isGround;

    bool isActing;  //两个动作不能同时进行
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        lastHor = this.transform.localScale.x;
        gravity = 6;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();

        Move();
        Jump();
        Attack();
        Crouch();

        myRigidbody.velocity = myRigidbody.velocity - new Vector2(0, gravity * Time.deltaTime);

        myAnim.SetFloat("HorizontalSpeed", Mathf.Abs(myRigidbody.velocity.x));
        myAnim.SetFloat("VerticalSpeed", myRigidbody.velocity.y);
        myAnim.SetBool("Crouch", Input.GetKey(KeyCode.S));
        myAnim.SetBool("OnGround", isGround);
    }

    void CheckGrounded()
    {
        //只有脚着地时才能跳跃
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    void Move()
    {
        float moveDir = Input.GetAxis("Horizontal");

        if (moveDir * lastHor < 0)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
        }
        lastHor = moveDir == 0 ? lastHor : moveDir;
        myRigidbody.velocity = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.K)&& isGround) 
        {
            isActing = true;
            Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
            myRigidbody.velocity = Vector2.up * jumpVel;              
        }
    }

    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            isActing = true;
            myAnim.SetTrigger("Attack");
            myAnim.SetBool("Idle", false);
            myAnim.SetBool("Crouch", false);
        }
    }

    void Crouch()
    {
        if (isActing || !Input.GetKeyDown(KeyCode.S))
        {
            //return;
        }
        myAnim.SetBool("Crouch", true);
        myAnim.SetBool("Idle", false);
    }

    void ActEnd()
    {
        isActing = false;
    }

}
