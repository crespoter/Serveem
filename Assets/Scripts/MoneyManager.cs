using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {
    public Text score;
    private int scoreINT;
    private void Awake()
    {
        scoreINT = 0;
    }
    private void Start()
    {
        score.text = "Cash " + "0";
    }
    public void AddScore(int x)
    {
        scoreINT += x;
        score.text = "Cash " + scoreINT.ToString();
    }
    public int currentScore()
    {
        return scoreINT;
    }
}
