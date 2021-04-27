using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierManager : MonoBehaviour {
    private int m_cash;

	// Use this for initialization
	void Start () {
        m_cash = 0;
	}
	public void addCash(int cash)
    {
        this.m_cash = cash;
    }
    public int GetCash()
    {
        return m_cash;
    }
}
