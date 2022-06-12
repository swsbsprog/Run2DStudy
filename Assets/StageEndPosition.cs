using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEndPosition : MonoBehaviour
{
    public GameObject gameResultUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("OnTriggerEnter2D" + collision.gameObject.tag);
        //if(collision.gameObject.tag == "Player") // 비추, 
        //{
        //    gameResultUI.SetActive(true);
        //}
        if (collision.gameObject.CompareTag("Player")) // CompareTag 추천
        {
            //print("게임 종료");
            gameResultUI.SetActive(true);
        }
    }
}
