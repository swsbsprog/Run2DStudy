using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffToggleButton : MonoBehaviour
{
    public GameObject targetGo;

    public void OnClickToggle()
    {
        //// ��ü ����
        //bool setActive;
        //if (targetGo.activeSelf)
        //    setActive = false;
        //else
        //    setActive = true;

        //targetGo.SetActive(setActive);


        ////// 3�� ������ ����.
        //bool setActive = targetGo.activeSelf ? false : true;
        //targetGo.SetActive(setActive);

        // false �� ����
        //targetGo.SetActive(targetGo.activeSelf == false);


        //// ! ���
        targetGo.SetActive(!targetGo.activeSelf);
    }
}
