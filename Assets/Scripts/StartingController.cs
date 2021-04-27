using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartingController : MonoBehaviour {
    public Text target;
    public bool tutorial = false;
    public Text previous_best;
    public int target_value;
    public GameObject Camera;
    public string Level;
    public GameObject StartingCanvas;
    private void Start()
    {
        Camera.GetComponent<Animator>().enabled = false;
        if (!tutorial)
        {
            target.text = target_value.ToString();
            if (PlayerPrefs.HasKey("Level " + Level + " Best"))
            {
                previous_best.text = PlayerPrefs.GetString("Level " + Level + " Best");
            }
            else
            {
                previous_best.text = "-";
            }
        }
        GetComponent<Pause>().OnClickPause();
        
    }
    public void OnClickMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void OnClickPlay()
    {
        GetComponent<Pause>().OnClickPlay();
        Camera.GetComponent<Animator>().enabled = true;
        StartingCanvas.GetComponent<Animator>().SetBool("Start", true);
    }
    
}
