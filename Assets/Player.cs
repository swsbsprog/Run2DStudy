using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        jellyScore = PlayerPrefs.GetInt("jellyScore");

        UpdateJelllyScore();
    }
    void SaveJellyScore()
    {
        PlayerPrefs.SetInt("jellyScore", jellyScore);
        PlayerPrefs.Save();
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

        if(state == State.BeginJump)
        {
            float currentY = transform.position.y;
            if(previousY > currentY)
            {
                state = State.BeginDown;
                animator.Play("JumpDown");
            }
            previousY = transform.position.y;
        }

        if (state == State.BeginDown)
        {
            bool isContact = false;
            List<ContactPoint2D> contacts = new List<ContactPoint2D>();
            int count = rb.GetContacts(contacts);
            if (contacts.Count > 0)
            {
                //print($"count:{count}, contacts:{contacts[0].collider.name}");
                isContact = true;
            }

            if (isContact && rb.velocity.y < 0.00001f) // 땅에 떨어 졌는데도 0이 안되고 있다(늦게 되고 있다)
            {
                state = State.Normal; // 너무 늦게 되고있다.(가끔발생, 평지에선 발생 안됨)
                continuAirJumpCount = 0;
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
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

        if(autoMoveX)
            moveX = 1;

        if (moveX != 0)
        { 
            transform.Translate(moveX * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = moveX < 0;
            if(state == State.Normal)
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
    public bool autoMoveX = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            print(collision.name + "부딛혔다");
            Destroy(collision.gameObject);

            //// 점수 증가.
            //score = score + 100;
            score += 100;
            scoreText.text = score.ToString();

            jellyScore += 1;
            UpdateJelllyScore();
        }
    }

    private void UpdateJelllyScore()
    {
        jellyScoreText.text = jellyScore.ToString();
        SaveJellyScore();
    }

    public int score;
    public TextMeshProUGUI scoreText;
    static public int jellyScore;
    public TextMeshProUGUI jellyScoreText;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void MyInit()
    {
        Debug.Log("jellyScore reset.");
        jellyScore = 0;
    }
}
