using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject can1;
    public GameObject can2;
    public Animator can2Anim;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwitchCanvases());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SwitchCanvases()
    {
        can1.gameObject.SetActive(true);
        can2.gameObject.SetActive(false);
        yield return new WaitForSeconds(15);
        can2.gameObject.SetActive(true);
        can2Anim.Play("FadeIn");
        yield return new WaitForSeconds(2);
        can1.gameObject.SetActive(false);
    }
}
