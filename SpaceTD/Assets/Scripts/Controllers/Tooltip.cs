﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
   public Tower tower;

    // Start is called before the first frame update
    void Start()
    {
        string newText = tower.getName() + " - " + tower.getStage() + "\n Cost: " + tower.scrapCost + "\n" + tower.getDetails();
        gameObject.GetComponent<Text>().text = newText;
    }
}
