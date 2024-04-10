using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSlider : MonoBehaviour
{
    private Slider slider;
    private PlayerMovement playerMovement;
    void Start()
    {
        slider = GetComponent<Slider>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        slider.minValue = 0;
        slider.maxValue = playerMovement.maxStamina;
    }

    void Update()
    {
        slider.value = playerMovement.readStamina();
    }
}
