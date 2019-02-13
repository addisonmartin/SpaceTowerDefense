using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Text scrapDisplay;

    private int scrap = 1000;
    private float hp = 100f;

    // Start is called before the first frame update
    void Start() {
        scrapDisplay.text = "" + scrap;
    }

    // Update is called once per frame
    void Update() {

    }

    public void addScrap(int s) {
        scrap += s;
        scrapDisplay.text = "" + scrap;
    }

    public int getScrap() {
        return scrap;
    }

    public void takeDamage(float d) {
        hp -= d;
    }

}
