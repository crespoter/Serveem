using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public bool tutorial = false;
    private bool placeOrderTutorialDisplayed = false;
    private bool servingCustomerTutorialDisplayed = false;
    private bool endingTutorialDisplayed = false;
    private bool trashTutorialDisplayed = false;
    public TutorialController tutorialController;
    private bool seating2CustomerTutorialDisplayed = false;
    private enum status { FREE, MOVING_TO_TABLE, MOVING_TAKE_ORDERS, MOVING_TO_KITCHEN, MOVING_TO_SERVE, COLLECTING_TIP, MOVING_TO_TRASH,MOVING_TO_QUEUE }
    public float speed;
    public GameObject kitchenCounter;
    public Transform handPosition;
    public GameObject gameController;
    public Transform trashPosition;
    public Transform waitingQueuePosition;
    public Animator playerAnimator;


    private int currentStatus;

    private GameObject foodInHandReference;
    private bool movingToTable = false;
    private Rigidbody rb;
    private float camRayLength = 100;
    private UnityEngine.AI.NavMeshAgent nav;
    private int floorMask;
    private Queue<menuItems.menuItem> orders;
    private menuItems.menuItem foodInHand;
    private GameObject currentApproachingTable = null;
    private bool handFull = false;
    private bool paused = false;
    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        currentStatus = (int)status.FREE;
        orders = new Queue<menuItems.menuItem>();
    }
    private void Update()
    {
        if (currentStatus == (int)status.MOVING_TO_TABLE && nav.remainingDistance < 8)
        {
            playerAnimator.SetBool("walking", false);
            tableScript tsRef = currentApproachingTable.GetComponent<tableScript>();
            if (!currentApproachingTable.GetComponent<tableScript>().isFree())
            {
                if(tutorial && !placeOrderTutorialDisplayed)
                {
                    placeOrderTutorialDisplayed = true;
                    tutorialController.Display("placeOrder", 500);
                }
                transform.position = tsRef.waitingPosition.position;
                transform.rotation = tsRef.waitingPosition.rotation;
                tsRef.arrow.enabled = false;
                tsRef.orderTaken(this);
                calculateArrows();
            }
            nav.enabled = false;
            currentStatus = (int)status.FREE;
        }
        else if (currentStatus == (int)status.MOVING_TO_KITCHEN && nav.remainingDistance < 5)
        {
            playerAnimator.SetBool("walking", false);
            currentStatus = (int)status.FREE;
            nav.enabled = false;
            
            kitchenScript ksRef = kitchenCounter.GetComponent<kitchenScript>();
            transform.position = ksRef.orderingPosition.position;
            transform.rotation = ksRef.orderingPosition.rotation;
            while (orders.Count != 0)
            {
                ksRef.addToCooking(orders.Dequeue());
            }
            if (ksRef.foodReady() && !handFull)
            {
                if (tutorial && !servingCustomerTutorialDisplayed)
                {
                    servingCustomerTutorialDisplayed = true;
                    tutorialController.Display("servingCustomer", 100);
                }
                foodInHand = ksRef.ReleaseFood();
                handFull = true;
                tableScript tsRef = currentApproachingTable.GetComponent<tableScript>();
                foodInHandReference = Instantiate(foodInHand.obj, handPosition);
                foodInHandReference.transform.localPosition = new Vector3(0, 0, 0);
                foodInHandReference.transform.localScale = Vector3.one;
                gameController.GetComponent<arrowManager>().ReCalculateArrows(foodInHand.name);
            }

        }
        else if (currentStatus == (int)status.MOVING_TO_SERVE && nav.remainingDistance < 8)
        {
            playerAnimator.SetBool("walking", false);
            nav.enabled = false;
            currentStatus = (int)status.FREE;
            if (!currentApproachingTable.GetComponent<tableScript>().isFree())
            {
                if(tutorial && !trashTutorialDisplayed)
                {
                    trashTutorialDisplayed = true;
                    tutorialController.Display("trash", 100);
                }
                handFull = false;
                tableScript tsRef = currentApproachingTable.GetComponent<tableScript>();
                Destroy(foodInHandReference);
                tsRef.serveFood();
                transform.position = tsRef.waitingPosition.position;
                transform.rotation = tsRef.waitingPosition.rotation;
                calculateArrows();
            }
        }
        else if (currentStatus == (int)status.COLLECTING_TIP && nav.remainingDistance < 8)
        {
            playerAnimator.SetBool("walking", false);
            tableScript tsRef = currentApproachingTable.GetComponent<tableScript>();
            currentStatus = (int)status.FREE;
            nav.enabled = false;
            transform.position = tsRef.waitingPosition.position;
            transform.rotation = tsRef.waitingPosition.rotation;
            int cashInHand = tsRef.CollectTip();
            gameController.GetComponent<MoneyManager>().AddScore(cashInHand);
            if(tutorial)
            {
                tutorialController.Display("end",100);
            }

        }
        else if (currentStatus == (int)status.MOVING_TO_TRASH && nav.remainingDistance < 5)
        {
            playerAnimator.SetBool("walking", false);
            nav.enabled = false;
            currentStatus = (int)status.FREE;
            Destroy(foodInHandReference);
            handFull = false;
            transform.position = trashPosition.position;
            transform.rotation = trashPosition.rotation;
            calculateArrows();
        }
        else if(currentStatus == (int)status.MOVING_TO_QUEUE && nav.remainingDistance < 5)
        {
            if(tutorial && !seating2CustomerTutorialDisplayed)
            {
                seating2CustomerTutorialDisplayed = true;
                tutorialController.Display("seating2", 100);
            }
            playerAnimator.SetBool("walking", false);
            nav.enabled = false;
            currentStatus = (int)status.FREE;
            transform.position = waitingQueuePosition.position;
            transform.rotation = trashPosition.rotation;
            gameController.GetComponent<CustomerSelector>().selectCustomer();
        }
    }
    public void MoveToTrash()
    {
        
        if (handFull && !gameController.GetComponent<arrowManager>().atleastOne)
        {
            playerAnimator.SetBool("walking", true);
            currentStatus = (int)status.MOVING_TO_TRASH;
            nav.enabled = true;
            nav.SetDestination(trashPosition.position);
            nav.speed = speed;
            Vector3 playerToMouse = trashPosition.position - transform.position;
            GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(playerToMouse));
        }
    }
    public void serveTable(GameObject table)
    {
        
        if (handFull && table.GetComponent<tableScript>().MenuItemOrdered() != null && table.GetComponent<tableScript>().MenuItemOrdered().name == foodInHand.name)
        {
            playerAnimator.SetBool("walking", true);
            currentStatus = (int)status.MOVING_TO_SERVE;
            nav.enabled = true;
            nav.SetDestination(table.transform.position);
            nav.speed = speed;
            currentApproachingTable = table;

            Vector3 playerToMouse = table.transform.position - transform.position;
            GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(playerToMouse));
            calculateArrows();
        }
    }
    public void collectTip(GameObject table)
    {
        playerAnimator.SetBool("walking", true);
        currentStatus = (int)status.COLLECTING_TIP;
        nav.enabled = true;
        nav.SetDestination(table.transform.position);
        nav.speed = speed;
        currentApproachingTable = table;
        Vector3 playerToMouse = table.transform.position - transform.position;
        GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(playerToMouse));
    }
    public void takeOrdersFromTable(GameObject table)
    {
        playerAnimator.SetBool("walking", true);
        nav.enabled = true;
        nav.speed = speed;
        nav.SetDestination(table.transform.position);
        currentApproachingTable = table;
        Vector3 playerToMouse = table.transform.position - transform.position;
        GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(playerToMouse));
        currentStatus = (int)status.MOVING_TO_TABLE;
    }
    public void addOrder(menuItems.menuItem item)
    {
        orders.Enqueue(item);
    }
    public void approachKitchen()
    {
       
        if (orders.Count != 0 || (kitchenCounter.GetComponent<kitchenScript>().foodReady() && !handFull))
        {
            playerAnimator.SetBool("walking", true);
            nav.enabled = true;
            nav.speed = speed;
            nav.SetDestination(kitchenCounter.GetComponent<kitchenScript>().orderingPosition.position);
            currentStatus = (int)status.MOVING_TO_KITCHEN;
            Vector3 playerToMouse = kitchenCounter.transform.position - transform.position;
            GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(playerToMouse));
        }
    }
    public void ApproachCustomerInQueue()
    {
        playerAnimator.SetBool("walking", true);
        nav.enabled = true;
        nav.speed = speed;
        nav.SetDestination(new Vector3(waitingQueuePosition.position.x,0,waitingQueuePosition.position.z));
        currentStatus = (int)status.MOVING_TO_QUEUE;
        Vector3 playerToMouse = waitingQueuePosition.transform.position - transform.position;
    }

    public void calculateArrows()
    {
        if(handFull == true)
        {
            gameController.GetComponent<arrowManager>().ReCalculateArrows(foodInHand.name);
        }
        else
        {
            gameController.GetComponent<arrowManager>().DisableAllArrows();
        }
    }

    public void Pause()
    {
        playerAnimator.enabled = false;
        paused = true;
        if(nav.enabled == true)
        {
            nav.isStopped = true;
        }
    }
    public void Resume()
    {
        playerAnimator.enabled = true;
        paused = false;
        if(nav.enabled == true)
        {
            nav.isStopped = false;
            nav.speed = speed;
        }
    }

}
