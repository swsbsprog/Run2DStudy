using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUV : MonoBehaviour
{
    public Vector2 scrollSpeed = new Vector2(0.001f, 0);

    public Vector2 currentOffset = new Vector2(0, 0);
    private void Update()
    {
        currentOffset += scrollSpeed;
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", currentOffset);
    }
}
