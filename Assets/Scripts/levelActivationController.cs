using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelActivationController : MonoBehaviour {
    public GameObject panelOfButtons;
    public Sprite unlock;
    void Start ()
    {
        if(!PlayerPrefs.HasKey("currentLevel"))
        {
            PlayerPrefs.SetInt("currentLevel", 1);
        }
        int maxLevel = PlayerPrefs.GetInt("currentLevel");
        Button[] buttons = GetComponentsInChildren<Button>();
        for(int i=0;i<maxLevel;i++)
        {
            buttons[i].GetComponent<Image>().sprite = unlock;
            buttons[i].interactable = true;
        }
	}
	
}
