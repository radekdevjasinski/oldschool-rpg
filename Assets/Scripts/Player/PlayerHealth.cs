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
    
    void Start()
    {
        health = maxHP;
    }

    void Update()
    {
        if (health <= 0) Die();
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);

        Collider[] attackingObjects = Physics.OverlapSphere(transform.position, damageRange, damageLayer);  
        foreach (Collider collider in attackingObjects)
        {
            health -= damageAmount * Time.deltaTime;
        }
    }

    void Die()
    {
        PlayerMovement movement = GetComponent<PlayerMovement>();
        Destroy(movement);
        FPSCamera fPSCamera = GameObject.Find("Main Camera").GetComponent<FPSCamera>();
        Destroy(fPSCamera);
        WeaponController weaponController = GameObject.Find("WeaponHolder").GetComponent<WeaponController>();
        Destroy(weaponController);
        endImage.color = new Color32(50, 0, 0, 230);

    }
}
