using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : MonoBehaviour
{
    public Image ammoBar;
    public float ammoAmount = 100f;
    public bool canShoot = true;
    public Animator gunAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ammoAmount <= 0)
        {
            StartCoroutine(CanShootDelay());
        }
    }

    public void UseAmmo(float wastedAmmo)
    {
        ammoAmount -= wastedAmmo;
        ammoBar.fillAmount = ammoAmount / 100f;
    }

    public void CoolDownWeapon(float coolAmount)
    {
        ammoAmount += coolAmount;
        ammoAmount = Mathf.Clamp(ammoAmount, 0, 100);

        ammoBar.fillAmount = ammoAmount / 100f;
    }

    IEnumerator CanShootDelay()
    {
        canShoot = false;
        gunAnim.SetBool("coolDown", true);
        yield return new WaitForSeconds(2);
        CoolDownWeapon(100);
        gunAnim.SetBool("coolDown", false);
        canShoot = true;
    }
}
