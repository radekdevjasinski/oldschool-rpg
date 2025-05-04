using System.Collections;
using UnityEngine;

public class GunFlashLight : MonoBehaviour
{
    public Light muzzleFlashLight;       // Światło typu Point Light
    public float flashDuration = 0.05f;  // Czas trwania błysku
    public float flashIntensity = 10f;   // Intensywność błysku

    void Start()
    {
        if (muzzleFlashLight != null)
        {
            muzzleFlashLight.enabled = false; // Ukryj światło na start
        }
    }

    public void TriggerFlash()
    {
        if (muzzleFlashLight != null)
        {
            StartCoroutine(FlashRoutine());
        }
    }

    private IEnumerator FlashRoutine()
    {
        muzzleFlashLight.intensity = flashIntensity;
        muzzleFlashLight.enabled = true;

        yield return new WaitForSeconds(flashDuration);

        muzzleFlashLight.enabled = false;
    }
}
