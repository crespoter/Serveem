using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowManager : MonoBehaviour {
    public bool atleastOne;
    public GameObject trashcan;
    
    public void ReCalculateArrows(string foodInHand)
    {
        GameObject[] tables = GetComponent<CustomerSelector>().tables;
        atleastOne = false;
        for(int i=0;i<tables.Length;i++)
        {
            if (tables[i].GetComponent<tableScript>().itemOrderedName() != null && tables[i].GetComponent<tableScript>().itemOrderedName().Equals(foodInHand))
            {
                tables[i].GetComponent<tableScript>().ActivateArrow();
                atleastOne = true;
            }
        }
        if(!atleastOne)
        {
            trashcan.GetComponent<trashManager>().ActivateArrow();
        }
    }
    public void DisableAllArrows()
    {
        GameObject[] tables = GetComponent<CustomerSelector>().tables;
        for (int i = 0; i < tables.Length; i++)
        {
            tables[i].GetComponent<tableScript>().arrow.enabled = false;
        }
        trashcan.GetComponent<trashManager>().DisableArrow();
    }
}
