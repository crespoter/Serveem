using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endingController : MonoBehaviour {
    public GameObject endingCanvas;
    public Text status;
    public Text score_value;
    public Text target_value;
    public Button NextLevel;
    public Image locked;

	public void End()
    {
        GetComponent<Pause>().OnClickPause();
        endingCanvas.SetActive(true);
        int currentScore = GetComponent<MoneyManager>().currentScore();
        int target = GetComponent<StartingController>().target_value;
        score_value.text = currentScore.ToString();
        target_value.text = target.ToString();
        if(currentScore >= target)
        {
            status.text = "Passed";
            PlayerPrefs.SetInt("currentLevel", (int.Parse(GetComponent<StartingController>().Level) + 1));
        }
        else
        {
            status.text = "Failed";
            NextLevel.image = locked;
            NextLevel.interactable = false;

        }
    }
    public void onClickMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void OnClickReplay()
    {
        SceneManager.LoadScene("Level "+ GetComponent<StartingController>().Level);
    }
    public void OnClickNextLevel()
    {
        PlayerPrefs.SetString("level to load", "Level " + (GetComponent<StartingController>().Level + 1));
        SceneManager.LoadScene("levleLoader");
    }
}
