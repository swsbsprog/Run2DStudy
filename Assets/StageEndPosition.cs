using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEndPosition : MonoBehaviour
{
    public GameObject gameResultUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("OnTriggerEnter2D" + collision.gameObject.tag);
        //if(collision.gameObject.tag == "Player") // ����, 
        //{
        //    gameResultUI.SetActive(true);
        //}
        if (collision.gameObject.CompareTag("Player")) // CompareTag ��õ
        {
            //print("���� ����");
            gameResultUI.SetActive(true);
        }
    }
}
