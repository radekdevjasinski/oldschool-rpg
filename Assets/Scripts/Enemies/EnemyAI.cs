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

    [Header("States")]
    public float attackRange;
    public bool playerInAttackRange;

    [Header("HitPoints")]
    public float maxHP = 100f;
    public float playerDMG = 50f;
    [SerializeField]
    private float hp;
    public GameObject killCount;

    private Rigidbody[] ragdollBodies;
    public GameObject game;


    void Start()
    {
        player = GameObject.Find("Player").transform;
        killCount = GameObject.Find("Count");
        agent = GetComponent<NavMeshAgent>();
        game = GameObject.Find("Game");
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        SetRagdollState(false);


        hp = maxHP;
    }

    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
        ChasePlayer();
        if (playerInAttackRange) AttackPlayer();
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
        Destroy(gameObject,5f);
        SetRagdollState(true);
        killCount.GetComponent<KillCount>().AddKill();
        gameObject.GetComponentInChildren<Animator>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<EnemyAI>().enabled = false;

    }
    private void SetRagdollState(bool state)
    {
        foreach (var rb in ragdollBodies)
        {
            rb.isKinematic = !state;
        }
    }
}
