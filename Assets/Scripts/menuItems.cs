using UnityEngine;

public class menuItems : MonoBehaviour {

    [System.Serializable]
    public class menuItem
    {
        public int priceBonus;
        public GameObject obj;
        public string name;
        public Sprite foodImage;
    };
    public menuItem[] menu;
}
