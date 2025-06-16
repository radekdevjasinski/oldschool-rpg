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
    private FPSCamera fPSCamera;

    void Start()
    {
        GameObject cameraObject = Camera.main.gameObject;
        fPSCamera = cameraObject.GetComponent<FPSCamera>();

        
    }
    void Update()
    {
        if (fPSCamera.lockMovement)
        {
            return; // If camera movement is locked, do not allow shooting
        }
        if( PauseMenu.isGamePaused)
        {
            return; // If the game is paused, do not allow shooting
        }
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
        AudioManager.Instance.PlaySFX("shoot");
        canShoot = false;
        StartCoroutine(shootCooldown());

        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, hitMask))
        {
            EnemyAI enemy = hit.collider.GetComponentInParent<EnemyAI>();
            if (enemy != null)
            {
                enemy.Damage(hit.point, hit.point - cam.transform.position);
                GameObject bloodPS = Instantiate(bloodPrefab, hit.collider.transform.position, Quaternion.identity);
                bloodPS.GetComponent<ParticleSystem>().Play();
                Destroy(bloodPS, 5f);
                AudioManager.Instance.PlaySFX("zombie_hit");
            }
        }
    }
    IEnumerator shootCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
    
}
