using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterBoltMoveForward : MonoBehaviour
{
    public float speed = 40.0f;

    private void Start()
    {
        StartCoroutine(SpawnDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    IEnumerator SpawnDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
