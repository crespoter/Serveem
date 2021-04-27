using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClickLevelButton : MonoBehaviour {
    public void loadLevel()
    {
        PlayerPrefs.SetString("level to load", "Level " + name);
        Application.LoadLevel("levelLoader"); 
    }
}
