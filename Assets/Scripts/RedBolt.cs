using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBolt : MonoBehaviour
{
    public HealthManager healthManager;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            healthManager.UseHealth(10);
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        healthManager = GameObject.Find("PlayerWolffe").GetComponentInChildren<HealthManager>();
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
