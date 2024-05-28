using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectScript : MonoBehaviour
{
    public bool isPlayerdetected = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerdetected = true;
        }
    }
}