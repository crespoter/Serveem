using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CustomerSelector : MonoBehaviour
{
    public GameObject[] tables;
    public GameObject Player;
    public LayerMask clickableLayer;
    private float camRayLength = 100;
    private bool firstCustomerSelected = false;
    private Color defaultColor;
    private bool pause;

    public float scale;  //Variable to change the intensity of highlighting
    Color Highlight(Color A)
    {
        float h, s, v;
        Color.RGBToHSV(A, out h, out s, out v);
        s += scale;
        return Color.HSVToRGB(h, s, v);
    }

    private void Start()
    {
        defaultColor = tables[0].GetComponentInChildren<Renderer>().material.color;
    }

    void FixedUpdate()
    {
        if(!pause)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit objectHit;
                if (Physics.Raycast(camRay, out objectHit, camRayLength, clickableLayer))
                {
                    foreach (GameObject x in tables)
                    {
                        x.GetComponentInChildren<Renderer>().material.SetColor("_Color", defaultColor); // Set all tables to default Color
                    }
                    if (objectHit.transform.tag == "WaitingCustomer")
                    {
                        Player.GetComponent<PlayerMovement>().ApproachCustomerInQueue();
                    }
                    else if (firstCustomerSelected && objectHit.transform.tag == "Free Table")
                    {
                        GameObject firstCustomer = GameObject.FindGameObjectWithTag("WaitingCustomer");
                        firstCustomer.GetComponent<customerManager>().moveToTable(objectHit.transform.gameObject);
                        firstCustomer.tag = "Eating Customer";
                        GetComponent<spawnCustomers>().removeFirstCustomer();
                        firstCustomerSelected = false;
                    }
                    else if (objectHit.transform.tag == "ordering_table" || objectHit.transform.tag == "message_ordering")
                    {
                        PlayerMovement pmover = Player.GetComponent<PlayerMovement>();
                        if (objectHit.transform.tag == "ordering_table")
                            pmover.takeOrdersFromTable(objectHit.transform.gameObject);
                        else
                            pmover.takeOrdersFromTable(objectHit.transform.parent.gameObject);
                    }
                    else if (objectHit.transform.tag == "kitchen_counter" || objectHit.transform.tag == "food_in_kitchen")
                    {
                        Player.GetComponent<PlayerMovement>().approachKitchen();
                    }
                    else if (objectHit.transform.tag == "order_waiting")
                    {
                        Player.GetComponent<PlayerMovement>().serveTable(objectHit.transform.gameObject);
                    }
                    else if (objectHit.transform.tag == "collect_tip")
                    {
                        Player.GetComponent<PlayerMovement>().collectTip(objectHit.transform.gameObject);
                    }
                    else if (objectHit.transform.tag == "trash")
                    {
                        Player.GetComponent<PlayerMovement>().MoveToTrash();
                    }
                    else
                    {
                        firstCustomerSelected = false;
                    }
                }
            }
        }
    }
    public void selectCustomer()
    {
        firstCustomerSelected = true;
        foreach (GameObject x in tables)
        {
            tableScript tableManager = x.GetComponent<tableScript>();
            if (!tableManager.isOccupied())
            {
                //HighLight x 
                Color B = Highlight(defaultColor); // Highlight Color
                x.GetComponentInChildren<Renderer>().material.SetColor("_Color", B);
                x.tag = "Free Table";
            }
        }
    }
    public void Pause()
    {
        pause = true;
    }
    public void Resume()
    {
        pause = false;
    }
}