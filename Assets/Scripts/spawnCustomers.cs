using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class spawnCustomers : MonoBehaviour {
    public GameObject[] prefab;
    public TutorialController tutorialController;
    public bool tutorial = false;
    private bool customerArrivalTutorialDisplayed = false;
    public float minCustArrival;
    public float maxCustArrival;
    private GameObject[] waitingCustomers;//List of waiting customers   
    private int noOfWaitingCustomers = 0;
    public GameObject []waitingQueue;//List of available waiting spots.
    public Transform exit;  
    public float customerSpeed;
    private bool[] customerWaitingAtPosition; //A boolean whether a customer is waiting at a given position
    private float nextSpawn;
    private float timer;
    private bool paused = false;
    public void removeFirstCustomer()
    {
        timer = 0.0f;
        nextSpawn = Random.Range(minCustArrival, maxCustArrival);
        int i;
        for (i=waitingQueue.Length-1;i>0 && customerWaitingAtPosition[i-1];i--)
        {
            waitingCustomers[i] = waitingCustomers[i - 1];
            if (i == waitingQueue.Length - 1)
            {
                waitingCustomers[i].tag = "WaitingCustomer";
            }
            NavMeshAgent nav = waitingCustomers[i].GetComponent<NavMeshAgent>();
            nav.enabled = true;
            waitingCustomers[i].GetComponent<customerManager>().customerAnimator.SetBool("walking", true);
            nav.speed = customerSpeed;
            nav.SetDestination(waitingQueue[i].transform.position);
        }
        customerWaitingAtPosition[i] = false;
        noOfWaitingCustomers--;
    }
    private void Start()
    {
        nextSpawn = Random.Range(minCustArrival, maxCustArrival);
        timer = 0.0f;
        waitingCustomers = new GameObject[waitingQueue.Length];
        customerWaitingAtPosition = new bool[waitingQueue.Length];
        for(int i =0;i<waitingQueue.Length;i++)
        {
            customerWaitingAtPosition[i] = false;
        }
    }
    private void Update()
    {
        if(!paused)
        { 
            timer += (Time.deltaTime*1000);
            if (nextSpawn <= timer)
            {
                

                if (tutorial && !customerArrivalTutorialDisplayed)
                {
                    customerArrivalTutorialDisplayed = true;
                    tutorialController.Display("customerArrival", 2000);
                }
                nextSpawn = timer + Random.Range(minCustArrival, maxCustArrival);
                if (tutorial)
                {
                    nextSpawn = 500000;
                }
                int luckyCustomer = Random.Range(0, prefab.Length);
                if (noOfWaitingCustomers != waitingQueue.Length)
                {
                    GameObject tempCustomer = Instantiate(prefab[luckyCustomer], waitingQueue[0].transform.position, waitingQueue[0].transform.rotation);
                    tempCustomer.GetComponent<customerManager>().exit = exit;
                    for (int i = waitingQueue.Length - 1; i >= 0; i--)
                    {
                        if (!customerWaitingAtPosition[i])
                        {
                            if (i == waitingQueue.Length - 1)
                            {
                                tempCustomer.tag = "WaitingCustomer";
                            }
                            NavMeshAgent nav = tempCustomer.GetComponent<NavMeshAgent>();
                            nav.enabled = true;
                            tempCustomer.GetComponent<customerManager>().customerAnimator.SetBool("walking", true);
                            nav.speed = customerSpeed;
                            nav.SetDestination(waitingQueue[i].transform.position);
                            customerWaitingAtPosition[i] = true;
                            waitingCustomers[i] = tempCustomer;
                            break;
                        }
                    }

                    noOfWaitingCustomers++;
                }
            }         
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
