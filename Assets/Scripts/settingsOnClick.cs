using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsOnClick : MonoBehaviour {
    public GameObject settingsButton;
    public GameObject settingsCanvas; 
    public void openSettings()
    {
        settingsButton.GetComponent<Animator>().SetBool("clicked", true);
        settingsCanvas.SetActive(true);
        settingsCanvas.GetComponent<Animator>().SetBool("popup", true);
    }
    public void closeSettings()
    {
        settingsButton.GetComponent<Animator>().SetBool("clicked", false);
        settingsCanvas.GetComponent<Animator>().SetBool("popup", false);
    }
}
