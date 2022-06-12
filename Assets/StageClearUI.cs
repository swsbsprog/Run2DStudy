using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClearUI : MonoBehaviour
{
    public Player player;
    public List<GameObject> rankStars; // 0 : 1스타 넣자.

    private void Awake() => print("Awake");
    private void Start() => print("Start");
    private void OnEnable()
    {
        print("OnEnable");
        int rank;
        if (player.score >= 500)
            rank = 2;
        else if (player.score >= 300)
            rank = 1;
        else
            rank = 0;
        SetRankStar(rank);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }


    // 0 낮은거 1스타
    // 2 젤 놓은거 3스타.
    // 0가정
    void SetRankStar(int rank)
    {
        for (int i = 0; i < rankStars.Count; i++)
        {
            rankStars[i].SetActive(i == rank);
        }
    }

    //[ContextMenu("랭크 1로 설정")]
    //void SetRank1() { SetRankStar(0); }
    //[ContextMenu("랭크 2로 설정")]
    //void SetRank2() { SetRankStar(1); }
    //[ContextMenu("랭크 3로 설정")]
    //void SetRank3() { SetRankStar(2); }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1)) SetRankStar(0);
    //    if (Input.GetKeyDown(KeyCode.Alpha2)) SetRankStar(1);
    //    if (Input.GetKeyDown(KeyCode.Alpha3)) SetRankStar(2);
    //}
}
