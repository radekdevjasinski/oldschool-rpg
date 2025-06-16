using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class EnemySound : MonoBehaviour
{
    public List<AudioClip> randomClips;
    public float minInterval = 7f;
    public float maxInterval = 15f;

    public EnemyAI enemyAI; 

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1.0f; // 3D dźwięk
        audioSource.minDistance = 1f;
        audioSource.maxDistance = 30f;

        StartCoroutine(PlayRandomSounds());
    }
    void Update()
    {
        if (enemyAI.isDead)
        {
            audioSource.Stop();
            StopAllCoroutines();
        }
    }

    IEnumerator PlayRandomSounds()
    {
        while (true)
        {
            if (Random.Range(0f, 1f) < 0.5f) // 50% szans na zagranie dźwięku
            {
                if (randomClips.Count > 0 && !audioSource.isPlaying)
                {
                    AudioClip clip = randomClips[Random.Range(0, randomClips.Count)];
                    audioSource.PlayOneShot(clip);
                }
            }
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            
        }
    }
}
