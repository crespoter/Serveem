using UnityEngine;
using UnityEngine.UI;
public class tableScript : MonoBehaviour {
    private enum Status { FREE, OCCUPIED, CUSTOMER_INCOMING, ORDERING, ORDERED, EATING, PAYING,COLLECT_BILL };

    public bool tutorial = false;
    private bool takeOrdersTutorialDisplayed = false;
    private bool tipsTutorialDisplayed = false;
    public TutorialController tutorialController;
    public GameObject chair;
    public Vector2 orderingDelay;
    public Image arrow;
    public Vector2 orderingPatienceRange;
    public GameObject tipPrefab;
    public Transform waitingPosition;
    public Transform trayPosition;
    public menuItems mitemsScriptRef;
    public Vector2 eatingTimeRange;
    public messageManager messageScript;
    public Slider patienceBar;
    private float customerPatienceTime;
    private GameObject occupingCustomer;
    private float timer=0.0f;
    private float orderTime;
    private int status;
    private menuItems.menuItem itemOrdered;
    private GameObject floatingMessageReference = null;
    private GameObject foodReference;
    private GameObject tipReference;
    private float eatingTime;
    private int tip;
    private bool sliderEnabled = false;
    private SliderScript ssRef;
    private bool paused = false;
    public string itemOrderedName()
    {
        if (status == (int)Status.ORDERED)
            return itemOrdered.name;
        else
            return null;
    }
    public float itemCost()
    {
        return itemOrdered.priceBonus;
    }
    private void Start()
    {
        status = (int)Status.FREE;
        patienceBar.gameObject.SetActive(false);
        ssRef = patienceBar.GetComponent<SliderScript>();
        
    }
    public bool isFree()
    {
        return (status == (int)Status.FREE);
    }
    public void orderTaken(PlayerMovement pmRef)
    {
        pmRef.addOrder(itemOrdered);
        messageScript.deactivateImage();
        status = (int)Status.ORDERED;
        tag = "order_waiting";
    }
    public menuItems.menuItem MenuItemOrdered()
    {
        return itemOrdered;
    }
    public bool isOccupied()
    {
        return status != (int)Status.FREE;
    }

    public float PatienceTime()
    {
        return customerPatienceTime;
    }

    public void occupy()
    {
        tag = "Table";
        timer = 0.0f;
        status = (int)Status.CUSTOMER_INCOMING;
        orderTime = Random.Range(orderingDelay.x, orderingDelay.y);
        customerPatienceTime = Random.Range(orderingPatienceRange.x, orderingPatienceRange.y);
    }
    public void leave()
    {
        status = (int)Status.FREE;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().calculateArrows();
        arrow.enabled = false;
    }
    public void sit(GameObject customer)
    {
        occupingCustomer = customer;
        ssRef.myParentCustomer = customer;
        
        ssRef.toggleSlider();
        
        patienceBar.gameObject.SetActive(true);
        
        
        status = (int)Status.OCCUPIED;
        timer = 0.0f;
        customer.transform.parent = chair.transform;
        customer.transform.position = chair.transform.position;
        customer.transform.rotation = chair.transform.rotation;
    }
    public void serveFood()
    {
        //Pause the timer and calculate the tip
        foodReference = Instantiate(itemOrdered.obj, trayPosition.position, trayPosition.rotation);
        eatingTime = Random.Range(eatingTimeRange.x, eatingTimeRange.y);
        foodReference.transform.localScale = new Vector3(2, 2, 2);
        
        tag = "customer_eating";
        timer = 0.0f;
        status = (int)Status.EATING;
        patienceBar.gameObject.SetActive(false);
        occupingCustomer.GetComponent<customerManager>().calculateTip();
    }

    public void CustomerLeaving()
    {
        status = (int)Status.FREE;
        arrow.enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().calculateArrows();
        itemOrdered = null;
        timer = 0;
        patienceBar.gameObject.SetActive(false);
        ssRef.customerLeft();
        ssRef.toggleSlider();
        messageScript.deactivateImage();
    }

    public int CollectTip()
    {
        status = (int)Status.FREE;
        Destroy(tipReference);
        return tip;
    }
    private void Update()
    {
        if (!paused)
        {
            if (status == (int)Status.OCCUPIED)
            {
                timer += Time.deltaTime * 1000;
                if (timer > orderTime)
                {
                    if(tutorial && !takeOrdersTutorialDisplayed)
                    {
                        takeOrdersTutorialDisplayed = true;
                        tutorialController.Display("takeOrders", 1000);
                    }
                    status = (int)Status.ORDERING;
                    timer = 0.0f;
                    itemOrdered = mitemsScriptRef.menu[Random.Range(0, mitemsScriptRef.menu.Length)];
                    messageScript.AddImage(itemOrdered.foodImage);
                    tag = "ordering_table";
                }
            }
            if (status == (int)Status.EATING)
            {
                timer += Time.deltaTime * 1000;
                if (timer > eatingTime)
                {
                    if(tutorial && !tipsTutorialDisplayed)
                    {
                        tipsTutorialDisplayed = true;
                        tutorialController.Display("tip", 200);
                    }
                    status = (int)Status.PAYING;
                    timer = 0.0f;
                    Destroy(foodReference);
                    tip = occupingCustomer.GetComponent<customerManager>().tip;
                    tipReference = Instantiate(tipPrefab);
                    tipReference.transform.localPosition = trayPosition.position;
                    occupingCustomer.GetComponent<customerManager>().LeaveTable();
                    tag = "collect_tip";
                    status = (int)Status.COLLECT_BILL;
                }
            }
        }
       

    }
    public void ActivateArrow()
    {
        arrow.enabled = true;
    }

    public void Pause()
    {
        paused = true;
        ssRef.Pause();
    }
    public void Resume()
    {
        paused = false;
        ssRef.Resume();
    }
}
