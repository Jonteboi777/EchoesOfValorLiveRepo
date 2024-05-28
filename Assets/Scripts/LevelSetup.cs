using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSetup : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject playerSpawnPoint;
    public Transform spawnTransformModification;
    public PlayerMovement playerMov;
    public PlayerCam playerCam;
    public AudioSource backgroundMusic;

    public GameObject enviorment;
    public GameObject deathCanvas;
    public GameObject pauseCanvas;
    public Animator rightShoulder;
    public Animator leftShoulder;
    public Animator deathReloadPanelAnimator;
    public Animator deathCanvasAnimator;
    private bool canRestartGame;

    // Start is called before the first frame update
    void Start()
    {
        deathCanvas.SetActive(false);
        enviorment.SetActive(false);
        StartCoroutine(SpawnPlayer());
        StartCoroutine(EnviormentSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        rightShoulder = GameObject.Find("Right shoulder").GetComponent<Animator>();
        leftShoulder = GameObject.Find("Left shoulder").GetComponent<Animator>();
        //playerMov = GameObject.Find("PlayerWolffe").GetComponentInChildren<PlayerMovement>();
        //playerCam = GameObject.Find("PlayerWolffe").GetComponentInChildren<PlayerCam>();

        if (canRestartGame)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(ReloadLevelAnim());
            }
        }
    }

    public IEnumerator KillPlayer()
    {
        backgroundMusic.Stop();
        //rightShoulder.SetTrigger("Death");
        //leftShoulder.SetTrigger("Death");
        //yield return new WaitForSeconds(0.3f);
        deathCanvas.SetActive(true);
        playerMov.enabled = false;
        playerCam.enabled = false;
        Destroy(pauseCanvas);
        deathCanvasAnimator.SetTrigger("Death");
        yield return new WaitForSeconds(2.5f);
        canRestartGame = true;
    }

    public IEnumerator ReloadLevelAnim()
    {
        deathReloadPanelAnimator.SetTrigger("Reload");
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(1);
    }
//
    IEnumerator EnviormentSpawn()
    {
        yield return new WaitForSeconds(22.63f);
        enviorment.SetActive(true);
    }


    IEnumerator SpawnPlayer()
    {
        playerMov.enabled = false;
        playerCam.enabled = false;
        yield return new WaitForSeconds(33f);
        playerObject = GameObject.Find("PlayerWolffe");
        playerObject.SetActive(true);
        playerMov.enabled = true;
        playerCam.enabled = true;
        //Instantiate(playerObject, playerSpawnPoint.transform.position, playerSpawnPoint.transform.rotation);
        //playerObject.transform.rotation = spawnTransformModification.transform.rotation;
        backgroundMusic.Play();
    }
}
