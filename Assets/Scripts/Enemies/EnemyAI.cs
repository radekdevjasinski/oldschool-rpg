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

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        killCount = GameObject.Find("Count");
        agent = GetComponent<NavMeshAgent>();


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
        hp -= 50f;
        if (hp <= 0)
        {
            Destroy(gameObject);
            killCount.GetComponent<KillCount>().AddKill();
        }
    }

}
