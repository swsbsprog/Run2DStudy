using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 1;

    private void Update()
    {
        Translate(speed * Time.deltaTime, 0, 0);
    }
    public void Translate(float x, float y, float z, Space relativeTo = Space.World)
    {
        transform.Translate(new Vector3(x, y, z));
    }
}
