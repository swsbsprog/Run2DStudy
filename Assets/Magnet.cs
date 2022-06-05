using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            print(collision.name + "ÀÚ¼®¿¡ ºÎµóÇû´Ù");
            MoveToTarget moveToTarget = collision.gameObject.AddComponent<MoveToTarget>();
            moveToTarget.target = transform;
        }
    }
}
