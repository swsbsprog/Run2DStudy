using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0; // �������� �ӵ�.
    }
    private void OnDisable()
    {
        Time.timeScale = 1; // ���� �ӵ�
    }
}
