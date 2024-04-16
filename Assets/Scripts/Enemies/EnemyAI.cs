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

    [Header("Attacking")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    bool isDissolving = false;

    [Header("States")]
    public float attackRange;
    public bool playerInAttackRange;

    [Header("HitPoints")]
    public float maxHP = 100f;
    public float playerDMG = 50f;
    [SerializeField]
    private float hp;
    public GameObject killCount;

    public List<DissolveScript> dissolveScripts = new List<DissolveScript>();
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        killCount = GameObject.Find("Count");
        agent = GetComponent<NavMeshAgent>();


        hp = maxHP;

        DissolveScript[] dissolveScriptArray = GetComponentsInChildren<DissolveScript>();
        dissolveScripts.AddRange(dissolveScriptArray);

        Rigidbody[] rigidbodyArray = GetComponentsInChildren<Rigidbody>();
        rigidbodies.AddRange(rigidbodyArray);

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        if (!isDissolving)
        {
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
            ChasePlayer();
            if (playerInAttackRange) AttackPlayer();
        }
    }
    void ChasePlayer()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }
    void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            player.GetComponent<PlayerHealth>().TakeDamage();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void Damage()
    {
        hp -= 50f;
        if (hp <= 0)
        {
            isDissolving = true;

            foreach (DissolveScript dissolveScript in dissolveScripts)
            {
                dissolveScript.StartDissolver();
            }

            foreach (Rigidbody rb in rigidbodies)
            {
                rb.isKinematic = false;
                rb.AddForce(Random.onUnitSphere * 0.1f, ForceMode.Impulse);
            }

            Destroy(gameObject,1.5f);
            killCount.GetComponent<KillCount>().AddKill();
        }
    }

}
