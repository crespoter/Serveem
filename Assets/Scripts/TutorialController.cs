using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {
    public GameObject seatingCustomer;
    public GameObject takingOrders;
    public GameObject placeOrders;
    public GameObject foodReady;
    public GameObject servingCustomer;
    public GameObject trash;
    public GameObject collectingTips;
    public GameObject seatingCustomer2;
    
    public Pause pause;
    public GameObject ending;
    private GameObject currentActive = null;
    public void Display(string key,float delay)
    {
        StartCoroutine(showHint(key, delay));
    }

    IEnumerator showHint(string key,float delay)
    {
        pause.Pause_No_Menu();
        yield return new WaitForSeconds(delay / 1000);
        if(key.Equals("customerArrival"))
        {
            currentActive = seatingCustomer;
            seatingCustomer.SetActive(true);
        }
        else if(key.Equals("seating2"))
        {
            currentActive = seatingCustomer2;
            seatingCustomer2.SetActive(true);
        }
        else if (key.Equals("takeOrders"))
        {
            currentActive = takingOrders;
            takingOrders.SetActive(true);
        }
        else if(key.Equals("foodReady"))
        {
            currentActive = foodReady;
            foodReady.SetActive(true);
        }
        else if(key.Equals("placeOrder"))
        {
            currentActive = placeOrders;
            placeOrders.SetActive(true);
        }
        else if(key.Equals("servingCustomer"))
        {
            currentActive = servingCustomer;
            servingCustomer.SetActive(true);
        }
        else if(key.Equals("trash"))
        {
            currentActive = trash;
            trash.SetActive(true);
        }
        else if(key.Equals("tip"))
        {
            currentActive = collectingTips;
            collectingTips.SetActive(true);
        }
        else if(key.Equals("end"))
        {
            currentActive = ending;
            ending.SetActive(true);
        }
    }
    public void OnClickOK()
    {
        currentActive.SetActive(false);
        pause.noMenuPlay();
    }
}
