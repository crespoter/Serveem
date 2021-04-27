using UnityEngine;
using UnityEngine.UI;
public class customerManager : MonoBehaviour {
    private enum Status { WAITING_IN_QUEUE,WAITING_FOR_ORDER,ORDERING,MOVING_TO_TABLE,EATING,LEAVING};
    private Status status;
    private UnityEngine.AI.NavMeshAgent nav;
    private bool movingToTable = false;
    [HideInInspector]
    public GameObject myTable;
    [HideInInspector]
    public Transform exit;
    [SerializeField] GameObject slider;
    SliderScript ssRef;
    public Vector2 tipRange;
    [HideInInspector]
    public int tip = 0;
    private bool paused;
    public float waitingInQueueTimeMultiplier;
    public float tableWaitingTimeBonus;
    public Animator customerAnimator;
    void Start ()
    {
        paused = false;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        ssRef = slider.GetComponent<SliderScript>();
        status = Status.WAITING_IN_QUEUE;
        tip = (int)Random.Range(tipRange.x, tipRange.y);
    }
    public void calculateTip()  
    {
        slider.SetActive(false);
        slider.GetComponent<SliderScript>().speed = 0;
        Debug.Log("Tip for waiting in queue : " + slider.GetComponent<WaitingSliderScript>().remaininTime / 1000 * waitingInQueueTimeMultiplier);
        Debug.Log("Tip for waitign in table " + (myTable.GetComponent<tableScript>().patienceBar.GetComponent<SliderScript>().slider.value / 1000 * tableWaitingTimeBonus));
        tip += (int)(slider.GetComponent<WaitingSliderScript>().remaininTime / 1000 * waitingInQueueTimeMultiplier);
        tip += (int)myTable.GetComponent<tableScript>().itemCost();
        tip += (int)(myTable.GetComponent<tableScript>().patienceBar.GetComponent<SliderScript>().slider.value/1000 * tableWaitingTimeBonus);
    }
    public void moveToTable(GameObject table)
    {
        customerAnimator.SetBool("walking", true);
        nav.enabled = true;     
        movingToTable = true;
        status = Status.MOVING_TO_TABLE;
        myTable = table;
        table.GetComponent<tableScript>().occupy();
        nav.SetDestination(table.transform.position);
        Vector3 playerToMouse = table.transform.position - transform.position;
        GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(playerToMouse));
        slider.gameObject.SetActive(false);
    }
	void Update ()
    {
        
        if(movingToTable)
        {
            if(nav.enabled && nav.remainingDistance<10)
            {
                customerAnimator.SetBool("walking", false);
                nav.enabled = false;
                myTable.GetComponent<tableScript>().sit(this.gameObject);
                ssRef.toggleSlider();

            }
        }
        if(!movingToTable && nav.enabled && status!=Status.LEAVING && nav.remainingDistance<2)
        {
            nav.enabled = false;
            customerAnimator.SetBool("walking", false);
        }
		
	}
    public GameObject customerTable()
    {
        return myTable;
    }
    public void LeaveTable()
    {
        if (status != Status.LEAVING)
        {
            status = Status.LEAVING;
            customerAnimator.SetBool("walking", true);
            transform.parent = null;
            transform.position = myTable.GetComponent<tableScript>().waitingPosition.position;
            myTable.GetComponent<tableScript>().CustomerLeaving();
            tag = "customer_leaving";
            nav.enabled = true;
            nav.speed = 25;
            movingToTable = false;
            nav.SetDestination(exit.position);        }
    }
    public void Pause()
    {
        paused = true;
        customerAnimator.enabled = false;
        if(nav.enabled)
        {
            nav.isStopped = true;
        }
        slider.GetComponent<WaitingSliderScript>().speed = 0;
    }
    public void Resume()
    {
        customerAnimator.enabled = true;
        paused = false;
        if(nav.enabled)
        {
            nav.isStopped = false;
            nav.speed = 25;
        }
        slider.GetComponent<WaitingSliderScript>().speed = 1;
    }
}
