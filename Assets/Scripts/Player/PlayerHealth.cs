using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("PlayerHealth")]
    public float maxHP = 100;
    public float damageTaken = 20;
    [SerializeField]
    private float health;
    public Image endImage;

    
    void Start()
    {
        health = maxHP;
    }

    void Update()
    {
        if (health <= 0) Die();
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);

    }
    public void TakeDamage()
    {
        health -= damageTaken;
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
    public float ReadHP()
    {
        return health;
    }
}
