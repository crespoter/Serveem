using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;



public class TimeClock : MonoBehaviour {

    public Transform LoadingBar;
    public GameObject gameController;
    public float seconds;
    public GameObject Text;
    private float currentTime;
    private bool paused = false;
    private bool timeUp = false;
    // Use this for initialization
    private void Start()
    {
        currentTime = 0.0f;
    }
    void Update () {

        if (!paused && !timeUp)
        {
            currentTime += Time.deltaTime;
            float percentage = ((seconds - currentTime) / seconds);
            if (percentage > 0)
            {
                Text.GetComponent<Text>().text = ((int)(seconds - currentTime)).ToString();
            }
            else
            {
                timeUp = true;
                gameController.GetComponent<endingController>().End();
            }
            LoadingBar.GetComponent<Image>().fillAmount = percentage;
        }
    }
    public void Pause()
    {
        paused = true;
    }
    public void Resume()
    {
        paused = false;
    }
}
