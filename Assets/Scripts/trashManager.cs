using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trashManager : MonoBehaviour {

    public Image arrow;
    public void ActivateArrow()
    {
        arrow.enabled = true;
    }
    public void DisableArrow()
    {
        arrow.enabled = false;
    }
}
