using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillCount : MonoBehaviour
{
    [Header("Kill Counter")]
    public int count = 0;
    private TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    void Update()
    {
        text.text = count.ToString();
    }
    public void AddKill()
    {
        count++;
    }
}
