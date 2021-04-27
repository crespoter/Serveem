using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playOnClick : MonoBehaviour {

    public GameObject playButton;
    public GameObject playCanvas;
    private bool animationKiller = false;
    private float timer;
    public void openLevelSelector()
    {
        playCanvas.SetActive(true);
        playCanvas.GetComponent<Animator>().SetBool("popup", true);
        animationKiller = true;
        timer = 0.0f;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer>1.1 && animationKiller)
        {
            Debug.Log("Deactivating");
            playCanvas.GetComponent<Animator>().enabled = false;
            animationKiller = false;
        }
    }
    public void closeLevelSelector()
    {
        
        playCanvas.GetComponent<Animator>().SetBool("popup", false);
        playCanvas.GetComponent<Animator>().enabled = true;
    }
}
