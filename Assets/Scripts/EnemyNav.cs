using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyNav : MonoBehaviour
{
    public NavMeshAgent enemyNavAgent;
    public Transform enemy;
    private Transform player;
    public Transform[] patrolPoints;
    public int current;
    public float patrolSpeed;
    bool stopMoving;

    public Animator anim;
    public DetectArea detection;
    public Image healthBar;
    public float healthAmount = 100f;

    // Start is called before the first frame update
    void Start()
    {
        current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.detection.detected)
        {
            if (!stopMoving)
            {
                if (transform.position != patrolPoints[current].position)
                {
                    anim.SetTrigger("NormalWalk");
                    transform.position = Vector3.MoveTowards(transform.position, patrolPoints[current].position, patrolSpeed * Time.deltaTime);
                    enemy.LookAt(patrolPoints[current].transform);
                }
                else
                    current = (current + 1) % patrolPoints.Length;
            }
        }

        player = GameObject.Find("PlayerCommanderWolffe").GetComponent<Transform>();
        if (healthAmount > 0f)
        {
            if (this.detection.detected)
            {
                if (!this.detection.playerDead)
                {
                    stopMoving = true;
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
        BoxCollider col = gameObject.GetComponent<BoxCollider>();
        col.enabled = false;
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
