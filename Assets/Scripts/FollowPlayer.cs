using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public NavMeshAgent ai;
    public Transform player;
    public Animator aiAnim;
    Vector3 dest;
    public bool playerHasReachedPosition = false;

    void Update()
    {
        if (playerHasReachedPosition)
        {
            ai.SetDestination(player.position);
            if (ai.remainingDistance <= ai.stoppingDistance)
            {
                aiAnim.ResetTrigger("Jog");
                aiAnim.SetTrigger("Idle");
            }
            else
            {
                aiAnim.ResetTrigger("Idle");
                aiAnim.SetTrigger("Jog");
            }
        }
    }
}