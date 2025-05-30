using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public WeaponController controller;
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && controller.isAttacking)
        {
            //other.GetComponentInParent<EnemyAI>().Damage();
        }
    }
}
