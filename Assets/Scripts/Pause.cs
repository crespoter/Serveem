using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    public GameObject pauseCanvas;
    public GameObject[] tables;
    public GameObject kitchen;
    public TimeClock clockLink;
    private bool noMenuPause = false;
    public void Pause_No_Menu()
    {
        noMenuPause = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Pause();
        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
        foreach (GameObject customer in customers)
        {
            customer.GetComponent<customerManager>().Pause();
        }
        customers = GameObject.FindGameObjectsWithTag("WaitingCustomer");
        foreach (GameObject customer in customers)
        {
            customer.GetComponent<customerManager>().Pause();
        }
        customers = GameObject.FindGameObjectsWithTag("Eating Customer");
        foreach (GameObject customer in customers)
        {
            customer.GetComponent<customerManager>().Pause();
        }
        customers = GameObject.FindGameObjectsWithTag("customer_leaving");
        foreach (GameObject customer in customers)
        {
            customer.GetComponent<customerManager>().Pause();
        }
        foreach (GameObject table in tables)
        {
            table.GetComponent<tableScript>().Pause();
        }
        kitchen.GetComponent<kitchenScript>().Pause();
        GetComponent<spawnCustomers>().Pause();
        GetComponent<CustomerSelector>().Pause();
        clockLink.Pause();
    }
    public void OnClickPause()
    {
        pauseCanvas.GetComponent<Animator>().SetBool("popup", true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Pause();
        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
        foreach (GameObject customer in customers)
        {
            customer.GetComponent<customerManager>().Pause();
        }
        customers = GameObject.FindGameObjectsWithTag("WaitingCustomer");
        foreach (GameObject customer in customers)
        {
            customer.GetComponent<customerManager>().Pause();
        }
        customers = GameObject.FindGameObjectsWithTag("Eating Customer");
        foreach (GameObject customer in customers)
        {
            customer.GetComponent<customerManager>().Pause();
        }
        customers = GameObject.FindGameObjectsWithTag("customer_leaving");
        foreach (GameObject customer in customers)
        {
            customer.GetComponent<customerManager>().Pause();
        }
        foreach(GameObject table in tables)
        {
            table.GetComponent<tableScript>().Pause();
        }
        kitchen.GetComponent<kitchenScript>().Pause();
        GetComponent<spawnCustomers>().Pause();
        GetComponent<CustomerSelector>().Pause();
        clockLink.Pause();
    }
    public void noMenuPlay()
    {
        noMenuPause = false;
        OnClickPlay();
    }
    public void OnClickPlay()
    {
        pauseCanvas.GetComponent<Animator>().SetBool("popup", false);
        if (!noMenuPause)
        {
            
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Resume();
            GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
            foreach (GameObject customer in customers)
            {
                customer.GetComponent<customerManager>().Resume();
            }
            customers = GameObject.FindGameObjectsWithTag("WaitingCustomer");
            foreach (GameObject customer in customers)
            {
                customer.GetComponent<customerManager>().Resume();
            }
            customers = GameObject.FindGameObjectsWithTag("Eating Customer");
            foreach (GameObject customer in customers)
            {
                customer.GetComponent<customerManager>().Resume();
            }
            customers = GameObject.FindGameObjectsWithTag("customer_leaving");
            foreach (GameObject customer in customers)
            {
                customer.GetComponent<customerManager>().Resume();
            }

            foreach (GameObject table in tables)
            {
                table.GetComponent<tableScript>().Resume();
            }

            kitchen.GetComponent<kitchenScript>().Resume();
            GetComponent<spawnCustomers>().Resume();
            GetComponent<CustomerSelector>().Resume();
            clockLink.Resume();
        }
    }
    public void OnClickMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void OnClickQuit()
    {
        Application.Quit();
    }
}
