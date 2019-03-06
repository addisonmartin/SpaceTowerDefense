using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setTower(Tower tower) {
        string newText = tower.getName() + " - " + tower.getStage() + "\nCost: " + tower.scrapCost + "\n" + tower.getDetails();
        gameObject.GetComponent<Text>().text = newText;
    }

    public void clear() {
        gameObject.GetComponent<Text>().text = "";
    }
}
