using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    public LevelSetup levelsetup;
    public AudioSource deathSound;
    bool playedDS = false;

    public void UseHealth(float lostHealth)
    {
        healthAmount -= lostHealth;
        healthBar.fillAmount = healthAmount / 100f;
    }

    private void Start()
    {
        levelsetup = GameObject.Find("--LEVEL SETUP--").GetComponent<LevelSetup>();
        deathSound = GameObject.Find("MISSION FAILED _ AudioSource").GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(healthAmount == 0)
        {
            if (!playedDS)
            {
                deathSound.Play();
                playedDS = true;
            }
            levelsetup.StartCoroutine(levelsetup.KillPlayer());
        }
    }
}
