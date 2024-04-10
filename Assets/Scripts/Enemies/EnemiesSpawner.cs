using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [Header("EnemiesSpawner")]
    public float spawnCooldown = 5;
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnDistance = 10f;

    void Start()
    {
        Spawn();
    }
    void Update()
    {
        if (spawnCooldown > 1)
        {
            spawnCooldown -= Time.deltaTime * 0.01f;
        }
    }
    void Spawn()
    {
        Vector3 spawnPos;
        do
        {
            spawnPos = new Vector3(Random.Range(-60f, 60f), 0, Random.Range(-60f, 60f));

        } while (Vector3.Distance(spawnPos, player.position) <= spawnDistance);
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity, transform);
        Invoke(nameof(Spawn), spawnCooldown);
    }
}
