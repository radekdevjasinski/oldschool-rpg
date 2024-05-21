using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveScript : MonoBehaviour
{
    public float dissolveDuration = 2f;
    public float dissolveStrength;

    public Color startColor;
    public Color endColor;

    public void StartDissolver()
    {
        StartCoroutine(dissolver());
    }

    public IEnumerator dissolver()
    {
        float elapseTime = 0;

        Material dissolveMaterial = GetComponent<Renderer>().material;
        Color lerpedColor;

        while ( elapseTime < dissolveDuration)
        {
            elapseTime += Time.deltaTime;

            dissolveStrength = Mathf.Lerp(0, 1, elapseTime / dissolveDuration);
            dissolveMaterial.SetFloat("_DissolveStrength", dissolveStrength);

            lerpedColor = Color.Lerp(startColor, endColor, dissolveStrength);
            dissolveMaterial.SetColor("_Color", lerpedColor);

            yield return null;
        }
    }

}
