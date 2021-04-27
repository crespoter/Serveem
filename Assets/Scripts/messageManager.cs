using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class messageManager : MonoBehaviour {
    public Image messageImage;
    public void AddImage(Sprite imageToSet)
    {
        
        messageImage.enabled = true;
        messageImage.overrideSprite = imageToSet;
    }
    public void deactivateImage()
    {
        messageImage.enabled = false;
    }
}
