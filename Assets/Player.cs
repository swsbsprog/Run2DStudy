using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;
    Rigidbody2D rb;
    public float speed = 5;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public enum State
    {
        Normal,      //normal (idle, run)
        BeginJump,   // 점프시작   
        BeginDown,   // 하강시작
        DownIng,     // 떨어지는중   
    }
    public State state = State.Normal;
    float previousY;



    // 점프포스가 상승에서 하강으로 바뀔때 1~4번 재생. JumpDown

    // 위로 상승 중에는 0번 재생 -> JumpUp 점프 시작시 JumpUp재생
    // 하강으로 바뀔때 JumpDown재생.
    // 땅에 닿았을때 Jump끝 -> Idle, Run

    int continuAirJumpCount = 0;

    public float groundVelocity = -0.2f;
    public ContactFilter2D filter;
    List<Collider2D> contactColliders = new List<Collider2D>();
    void Update()
    {
        if (continuAirJumpCount < 2)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = Vector2.zero;
                //rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // Alt + F12
                animator.Play("JumpUp");
                state = State.BeginJump;
                previousY = float.MinValue;
                continuAirJumpCount++;
            }
        }

        if (state == State.BeginJump)
        {
            float currentY = transform.position.y;
            if (previousY > currentY)
            {
                state = State.BeginDown;
                animator.Play("JumpDown");
            }
            previousY = transform.position.y;
        }

        if (state == State.BeginDown)
        {
            if (rb.velocity.y > groundVelocity)
            {
                if (rb.GetContacts(filter, contactColliders) > 0)
                {
                    state = State.Normal;
                    continuAirJumpCount = 0;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.Play("Attack", 1, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.Play("Damage", 1, 0);
        }


        float moveX = 0;
        if (Input.GetKey(rightMoveKey))
            moveX = speed;
        if (Input.GetKey(leftMoveKey))
            moveX = -speed;

        if (moveX != 0)
        {
            transform.Translate(moveX * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = moveX < 0;
            if (state == State.Normal)
                animator.Play("Run");//Run
        }
        else
        {
            // Idle
            if (state == State.Normal)
                animator.Play("Idle");
        }
    }
    public KeyCode leftMoveKey = KeyCode.A;
    public KeyCode rightMoveKey = KeyCode.D;
    public float jumpForce = 20;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            print(collision.name + "부딛혔다");
            Destroy(collision.gameObject);
        }
    }
}
