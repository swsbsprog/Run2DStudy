using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    internal Transform target;
    public float lerp = 0.2f;
    void Update()
    {
        if (target == null)
            return;
                
        transform.position = Vector3.Lerp(transform.position, target.transform.position, lerp * Time.deltaTime);
    }
}
