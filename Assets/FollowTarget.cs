using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float forwardOffsetX = 0.5f;
    public float lerp = 0.01f;
    private void Awake()
    {
        print("Awake");
    }
    private void Start()
    {
        print("Start");
    }
    private void OnEnable()
    {
        print("OnEnable");
    }

    void Update()
    {
        var pos = target.position;
        pos.z = transform.position.z;

        var sr = target.GetComponent<SpriteRenderer>();
        bool isRightDirection = sr.flipX == false;

        if (isRightDirection)
            pos.x += forwardOffsetX;
        else
            pos.x += -forwardOffsetX;

        transform.position = Vector3.Lerp(transform.position, pos, lerp);
    }
}
