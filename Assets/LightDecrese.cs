using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDecrese : MonoBehaviour
{
    [Header("Torch")]
    public Light torchLight;
    public float maxTorchIntensity;
    public float minTorchIntensity;
    private float yStartPos;

    public GameObject torchGameObject;

    [Header("Light")]
    public Light globalLight;
    public float maxLightIntensity;
    public float minLightIntensity;

    [Header("LightState")]
    public float lightState;
    public float lightDecreseRate;

    void Start()
    {
        lightState = 1;
        yStartPos = torchGameObject.transform.position.y;
    }

    void Update()
    {
        lightState -= lightDecreseRate * Time.deltaTime;
        lightState = Mathf.Clamp(lightState, 0, 1);
        UpdateWorld();
    }

    public void UpdateWorld()
    {
        float xSize = Mathf.Lerp(0.3f, 0.6f, lightState);
        float ySize = Mathf.Lerp(0, 2, lightState);
        torchGameObject.transform.localScale = new Vector3(xSize, ySize, torchGameObject.transform.localScale.z);
        

        float yPos = Mathf.Lerp(yStartPos - 0.4f, yStartPos, lightState);
        torchGameObject.transform.position = new Vector3(torchGameObject.transform.position.x, yPos, torchGameObject.transform.position.z);
        

        torchLight.intensity = Mathf.Lerp(minTorchIntensity, maxTorchIntensity, lightState);

        globalLight.intensity = Mathf.Lerp(minLightIntensity, maxLightIntensity, lightState);

    }
    public void AddTorch(float diff)
    {
        lightState += Mathf.Clamp(lightState + diff, 0, 1);
    }
}
