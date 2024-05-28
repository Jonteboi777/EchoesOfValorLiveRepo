using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class DetectArea : MonoBehaviour
{
    public bool detected;
    public bool playerDead;
    GameObject target;
    public Transform Enemy;

    public GameObject bullet;
    public ParticleSystem muzzleFlash;
    public Transform ShootPoint;
    public AudioSource adSourceFire;
    public AudioSource adSourceArm;
    
    public float shootSpeed = 5f;
    public float timeToShoot = 1.3f;
    float originalTime;

    void Start()
    {
        originalTime = timeToShoot;
    }


    // Update is called once per frame
    void Update()
    {
        HealthManager healthManager = GameObject.Find("PlayerWolffe").GetComponentInChildren<HealthManager>();
        if (detected)
        {
            Enemy.LookAt(target.transform);
            
            timeToShoot -= Time.deltaTime;
            if (timeToShoot < 0)
            {
                if(healthManager.healthAmount > 0)
                {
                    ShootPlayer();
                    timeToShoot = originalTime;
                }
            }
        }

        if (healthManager.healthAmount <= 0)
        {
            playerDead = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            detected = true;
            target = other.gameObject;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            adSourceArm.Play();
        }
    }
    private void ShootPlayer()
    {
        GameObject currentBullet = Instantiate(bullet, ShootPoint.position, ShootPoint.rotation);
        Rigidbody rig = currentBullet.GetComponent<Rigidbody>();

        adSourceFire.Play();
        muzzleFlash.Play();
        rig.AddForce(transform.forward * shootSpeed, ForceMode.VelocityChange);
    }
}