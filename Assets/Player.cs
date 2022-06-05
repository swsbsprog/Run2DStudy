using System.Collections;
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // Alt + F12
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.Play("Attack", 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.Play("Damage", 0, 0);
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
        }
    }
    public KeyCode leftMoveKey = KeyCode.A;
    public KeyCode rightMoveKey = KeyCode.D;
    public float jumpForce = 20;
}
