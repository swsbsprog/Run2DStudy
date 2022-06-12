using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffToggleButton : MonoBehaviour
{
    public GameObject targetGo;

    public void OnClickToggle()
    {
        //// 전체 버전
        //bool setActive;
        //if (targetGo.activeSelf)
        //    setActive = false;
        //else
        //    setActive = true;

        //targetGo.SetActive(setActive);


        ////// 3항 연산자 버전.
        //bool setActive = targetGo.activeSelf ? false : true;
        //targetGo.SetActive(setActive);

        // false 비교 버전
        //targetGo.SetActive(targetGo.activeSelf == false);


        //// ! 사용
        targetGo.SetActive(!targetGo.activeSelf);
    }
}
