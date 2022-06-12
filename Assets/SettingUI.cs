using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0; // 멈췄을때 속도.
    }
    private void OnDisable()
    {
        Time.timeScale = 1; // 원래 속도
    }
}
