using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Attack")]
    public float cooldown;
    public bool isAttacking = false;
    private GameObject sword;
    private Animator animator;
    private bool canAttack = true;
    

    void Start()
    {
        sword = GameObject.Find("Sword");
        animator = sword.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }    
    }
    void Attack()
    {
        if (canAttack)
        {
            isAttacking = true;
            StartCoroutine(resetAttack());
            animator.SetTrigger("Attack");
            canAttack = false;
            StartCoroutine(attackCooldown());
        }
    }
    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }
    IEnumerator resetAttack()
    {
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;
    }
    
}
