using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float runSpeed;
    public float jumpSpeed;
    public float lastHor;
    public float gravity;

    private Rigidbody2D myRigidbody;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    public bool isGround;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        lastHor = this.transform.localScale.x;
        gravity = 5;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();

        Move();
        Jump();
        Idle();
        Attack();
        Crouch();

        myRigidbody.velocity = myRigidbody.velocity - new Vector2(0, 5 * Time.deltaTime);
    }

    void CheckGrounded()
    {
        //只有脚着地时才能跳跃
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }


    void Move()
    {
        float moveDir = Input.GetAxis("Horizontal");
        myRigidbody.velocity = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        if (moveDir * lastHor < 0)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
        }
        lastHor = moveDir == 0 ? lastHor : moveDir;
        myAnim.SetBool("Run", Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            //只有在地面上时才能跳跃
            if(isGround)
            {
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);

                myRigidbody.velocity = Vector2.up * jumpVel;
            }   
        }
    }

    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            myAnim.SetTrigger("Attack");
            myAnim.SetBool("Idle", false);
            myAnim.SetBool("Crouch", false);
        }
    }

    void AttackEnd()
    {
        myAnim.ResetTrigger("Attack");
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.S) && isGround)
        {
            myAnim.SetBool("Crouch", true);
            myAnim.SetBool("Idle", false);
        }
    }

    void Idle()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            myAnim.SetBool("Idle", true);
            myAnim.SetBool("Crouch", false);
        }
    }
}
