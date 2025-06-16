using UnityEngine;
using UnityEngine.UI;

public class SensSlider : MonoBehaviour
{
    public FPSCamera fpsCamera;
    public Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fpsCamera = Camera.main.gameObject.GetComponent<FPSCamera>();
        slider = GetComponent<Slider>();
        slider.value = fpsCamera.sens;

    }

    public void OnSensChanged()
    {
        fpsCamera.sens = slider.value;
    }
}
