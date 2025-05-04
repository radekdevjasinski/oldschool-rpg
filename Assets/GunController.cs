using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Shoot")]
    public float cooldown;
    public Animator fpsAnimator;
    public ParticleSystem muzzleFlash;
    private bool canShoot = true;
    public float range = 100f;       
    public GameObject bloodPrefab;
    public GunFlashLight gunFlashLight;


    public LayerMask hitMask;        

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }    
    }
    void Shoot()
    {
        if (!canShoot)
        {
            return;    
        }

        fpsAnimator.SetTrigger("shoot");
        muzzleFlash.Play();
        gunFlashLight.TriggerFlash();
        canShoot = false;
        StartCoroutine(shootCooldown());

        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, hitMask))
        {
            EnemyAI enemy = hit.collider.GetComponentInParent<EnemyAI>();
            if (enemy!=null)
            {
                enemy.Damage();
                GameObject bloodPS = Instantiate(bloodPrefab, hit.collider.transform.position, Quaternion.identity);
                bloodPS.GetComponent<ParticleSystem>().Play();
                Destroy(bloodPS, 5f);
            }
        }
    }
    IEnumerator shootCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
    
}
