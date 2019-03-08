using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillPreview : MonoBehaviour {

    public Image image;
    public Text tName;
    public Text stats;
    public Text description;
    public Text cost;

    public void fillPreview(Tower t) {
        image.sprite = t.GetComponent<SpriteRenderer>().sprite;
        tName.text = t.tName;
        stats.text = "Range: " + t.range + " Damage: " + t.damage + "\nCooldown: " + t.cooldown + "s";
        description.text = t.getDescription();
        cost.text = "Cost: " + t.scrapCost;

    }
}
