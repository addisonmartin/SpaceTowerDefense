using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Cullen
public class AstralBody : MonoBehaviour, ISelectable {

    public static Image selectedImage;
    private const int ORBITAL_0_MAX = 4;
    private List<Tower> orbital0 = new List<Tower>();
    private const int ORBITAL_1_MAX = 8;
    private List<Tower> orbital1 = new List<Tower>();


    public void Start() {
        selectedImage = GameObject.Find("SelectedTowerDisplay").GetComponent<Image>();
        //selectedImage = FindObjectOfType<Image>();
    }

    public void display() {
        selectedImage.sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void undisplay() {
        selectedImage.sprite = null;
    }

    public bool addTower(int orbital, Tower t) {
        switch (orbital) {
            case 0:
                if (orbital0.Count >= ORBITAL_0_MAX) {
                    return false;
                }
                t.setOrbital(0);
                orbital0.Add(t);
                return true;
            case 1:
                if (orbital1.Count >= ORBITAL_1_MAX) {
                    return false;
                }
                t.setOrbital(1);
                orbital1.Add(t);
                return true;
            default:
                return false;
        }
    }

}
