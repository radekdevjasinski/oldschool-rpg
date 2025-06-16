using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("PlayerHealth")]
    public float maxHP = 100;
    public float health;
    public Image endImage;

    [Header("Damage Settings")]
    public float damageRange = 5f;
    public float damageAmount = 1f;
    public LayerMask damageLayer;

    [Header("Regain Settings")]
    public float regainTime = 3f;
    public float regainAmount = 1f;
    public bool isRegainingHealth = true;

    [Header("Damage Sound")]
    public float damageSoundInterval = 0.5f;
    public float damageTimer = 0f;
    
    void Start()
    {
        health = maxHP;
    }

    void Update()
    {
        if (health <= 0) Die();

        Collider[] attackingObjects = Physics.OverlapSphere(transform.position, damageRange, damageLayer);
        foreach (Collider collider in attackingObjects)
        {
            health -= damageAmount * Time.deltaTime;
            isRegainingHealth = false;
            StopAllCoroutines();
            StartCoroutine(RegainHealthCoroutine());
            if (damageTimer >= damageSoundInterval)
            {
                AudioManager.Instance.PlaySFX("damage");
                damageTimer = 0f;
            }

        }
        if (isRegainingHealth && health < maxHP)
        {
            health += regainAmount * Time.deltaTime;
        }
        damageTimer += Time.deltaTime;
    }
    IEnumerator RegainHealthCoroutine()
    {
        yield return new WaitForSeconds(regainTime);
        isRegainingHealth = true;
    }



    void Die()
    {
        PlayerMovement movement = GetComponent<PlayerMovement>();
        movement.movementEnabled = false;

        GameObject camera = Camera.main.gameObject;
        FPSCamera fpsCamera = camera.GetComponent<FPSCamera>();
        fpsCamera.lockMovement = true;

    }
}
