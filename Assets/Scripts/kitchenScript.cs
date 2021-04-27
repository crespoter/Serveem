using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kitchenScript : MonoBehaviour
{
    private enum status { FREE, COOKING, WAITING_FOR_COUNTER };
    public bool tutorial = false;
    public TutorialController tutorialController;
    private bool foodReadyTutorialDisplayed = false;
    public float cookingTime;
    public Transform orderingPosition;
    public Transform orderReleasePosition;

    private Queue<menuItems.menuItem> cookingQueue;
    private menuItems.menuItem currentlyCooking;
    private menuItems.menuItem menuItemInCounter;
    private status currentStatus;
    private bool foodInCounter;
    private float currentTimer;
    private GameObject spawnedFood;
    private bool paused = false;
    public bool foodReady()
    {
        return foodInCounter;
    }

    public menuItems.menuItem ReleaseFood()
    {
        Destroy(spawnedFood);
        foodInCounter = false;
        if(currentStatus == status.WAITING_FOR_COUNTER)
        {
            currentStatus = status.COOKING;
        }
        return menuItemInCounter;
    }
    public void addToCooking(menuItems.menuItem item)
    {
        cookingQueue.Enqueue(item);
    }

   
    private void Awake()
    {
        cookingQueue = new Queue<menuItems.menuItem>();
        currentStatus = status.FREE;
        currentTimer = 0.0f;
        foodInCounter = false;
    }
    private void Update()
    {
        if (!paused)
        {
            if (currentStatus == status.FREE && cookingQueue.Count != 0)
            {
                currentlyCooking = cookingQueue.Dequeue();
                currentStatus = status.COOKING;
            }
            if (currentStatus == status.COOKING)
            {
                currentTimer += Time.deltaTime * 1000;
                if (currentTimer > cookingTime)
                {
                    currentTimer = 0;
                    if (!foodInCounter)
                    {
                        if(tutorial && !foodReadyTutorialDisplayed)
                        {
                            foodReadyTutorialDisplayed = true;
                            tutorialController.Display("foodReady", 1000);
                        }
                        spawnedFood = Instantiate(currentlyCooking.obj);
                        spawnedFood.tag = "food_in_kitchen";
                        menuItemInCounter = currentlyCooking;
                        spawnedFood.transform.parent = orderReleasePosition;
                        spawnedFood.transform.localPosition = Vector3.zero;
                        spawnedFood.transform.localScale = Vector3.one;
                        currentStatus = status.FREE;
                        currentlyCooking = null;
                        foodInCounter = true;
                    }
                    else
                    {
                        currentStatus = status.WAITING_FOR_COUNTER;
                    }
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
