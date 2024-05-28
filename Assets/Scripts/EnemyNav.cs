using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyNav : MonoBehaviour
{
    public NavMeshAgent enemyNavAgent;
    private Transform player;
    public Animator anim;
    public DetectArea detection;
    public Image healthBar;
    public float healthAmount = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("PlayerCommanderWolffe").GetComponent<Transform>();
        if (healthAmount > 0f)
        {
            if (this.detection.detected)
            {
                if (!this.detection.playerDead)
                {
                    enemyNavAgent.SetDestination(player.position);
                    anim.SetTrigger("StartWalking");
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            StartCoroutine(Die());
        }
    }
    public void UseHealth(float lostHealth)
    {
        healthAmount -= lostHealth;
        healthBar.fillAmount = healthAmount / 100f;
    }

    IEnumerator Die()
    {
        detection.enabled = false;
        enemyNavAgent.isStopped = true;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
