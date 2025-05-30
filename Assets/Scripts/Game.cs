using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Volume volume;
    public Image image;
    public PlayerHealth playerHealth;
    Vignette vignette;

    [Header("Low Health Settings")]
    [SerializeField] float maxVignetteIntensity = 0.8f;
    [SerializeField] float maxImageAlpha = 0.5f;

    void Update()
    {
        float healthPercentage = playerHealth.health / playerHealth.maxHP;
        float vignetteIntensity = Mathf.Lerp(0, maxVignetteIntensity, 1 - healthPercentage);
        float imageAlpha = Mathf.Lerp(0, maxImageAlpha, 1 - healthPercentage);

        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.intensity.value = vignetteIntensity;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, imageAlpha);
    }
}
