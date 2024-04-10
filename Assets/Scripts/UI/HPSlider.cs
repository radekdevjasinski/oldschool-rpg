using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    private Slider slider;
    private PlayerHealth playerHealth;
    void Start()
    {
        slider = GetComponent<Slider>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();

        slider.minValue = 0;
        slider.maxValue = playerHealth.maxHP;
    }

    void Update()
    {
        slider.value = playerHealth.ReadHP();
    }
}
