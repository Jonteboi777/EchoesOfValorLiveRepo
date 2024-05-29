using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBolt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyNav enemyHealth = collision.gameObject.GetComponent<EnemyNav>();
            enemyHealth.UseHealth(25);
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
