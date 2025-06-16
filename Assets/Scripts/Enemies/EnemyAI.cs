using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("EnemyUI")]
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask playerMask;

    [Header("HitPoints")]
    public float maxHP = 100f;
    public float playerDMG = 50f;
    public bool isDead = false;
    [SerializeField]
    public GameObject killCount;

    private Rigidbody[] ragdollBodies;
    [SerializeField]
    private GameObject playerBlocker;
    public GameObject game;


    void Start()
    {
        player = GameObject.Find("Player").transform;
        killCount = GameObject.Find("Count");
        agent = GetComponent<NavMeshAgent>();
        game = GameObject.Find("Game");
        SetRagdollState(false);

    }

    void Update()
    {
        ChasePlayer();
    }
    void ChasePlayer()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }

    public void Damage(Vector3 hitPoint, Vector3 forceDirection, float forceAmount = 250f)
    {
        //Destroy(gameObject, 5f);
        Destroy(playerBlocker);
        SetRagdollState(true);
        SetRagdollLayer(this.transform);
        killCount.GetComponent<KillCount>().AddKill();
        Destroy(gameObject.GetComponentInChildren<Animator>());
        Destroy(gameObject.GetComponent<NavMeshAgent>());
        Destroy(gameObject.GetComponent<EnemyAI>());

        // Znajdź najbliższą kość (Rigidbody) względem punktu trafienia
        Rigidbody closestRb = null;
        float closestDistance = float.MaxValue;

        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            float dist = Vector3.Distance(hitPoint, rb.worldCenterOfMass);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestRb = rb;
            }
        }

        // Dodaj siłę
        if (closestRb != null)
        {
            closestRb.AddForce(forceDirection.normalized * (forceAmount / 2.5f), ForceMode.Impulse);
        }

        // Rozdziel siłę na wszystkie Rigidbody
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rigidbodies)
        {
            if (rb == closestRb) continue;
            rb.AddForce(forceDirection.normalized * (forceAmount / rigidbodies.Length), ForceMode.Impulse);
        }
        isDead = true;
    }


    private void SetRagdollState(bool state)
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in ragdollBodies)
        {
            rb.isKinematic = !state;
            rb.interpolation = state ? RigidbodyInterpolation.Interpolate : RigidbodyInterpolation.None;
        }
    }
    void SetRagdollLayer(Transform root, string layerName = "ragdoll")
    {
        int layer = LayerMask.NameToLayer(layerName);
        foreach (Transform t in root.GetComponentsInChildren<Transform>())
        {
            t.gameObject.layer = layer;
        }
    }
}
