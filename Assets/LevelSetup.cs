using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject playerSpawnPoint;

    public GameObject enviorment;

    // Start is called before the first frame update
    void Start()
    {
        enviorment.SetActive(false);
        StartCoroutine(SpawnPlayer());
        StartCoroutine(EnviormentSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator EnviormentSpawn()
    {
        yield return new WaitForSeconds(22.63f);
        enviorment.SetActive(true);
    }

    IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(33);
        Instantiate(playerObject, playerSpawnPoint.transform.position, playerSpawnPoint.transform.rotation);
    }
}
