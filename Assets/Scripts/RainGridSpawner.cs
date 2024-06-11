using UnityEngine;
using System.Collections.Generic;

public class RainGridSpawner : MonoBehaviour
{
    public GameObject rainParticlePrefab;
    public GameObject player;
    public float gridSpacing = 20f;
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float activationDistance = 20f; 
    private List<ParticleSystem> rainParticleSystems = new List<ParticleSystem>();

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        Vector3 center = new Vector3(gridWidth * gridSpacing / 2, 0, gridHeight * gridSpacing / 2);

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                Vector3 position = new Vector3(x * gridSpacing, 25, z * gridSpacing) - center;
                GameObject rainObject = Instantiate(rainParticlePrefab, position, Quaternion.identity);
                rainObject.transform.parent = this.transform; // Optional: parent to the spawner for organization
                ParticleSystem rainParticleSystem = rainObject.GetComponent<ParticleSystem>();
                rainParticleSystems.Add(rainParticleSystem);
            }
        }
    }

    void Update()
    {
        foreach (ParticleSystem rainParticleSystem in rainParticleSystems)
        {
            float distance = Vector3.Distance(player.transform.position, rainParticleSystem.transform.position);

            if (distance <= activationDistance && !rainParticleSystem.isPlaying)
            {
                rainParticleSystem.Play();
            }
            else if (distance > activationDistance && rainParticleSystem.isPlaying)
            {
                rainParticleSystem.Stop();
            }
        }
    }
}
