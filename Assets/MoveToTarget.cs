using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    internal Transform target;
    public float lerp = 0.01f;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, lerp);
    }
}
