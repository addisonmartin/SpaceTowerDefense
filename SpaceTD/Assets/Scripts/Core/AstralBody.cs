using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Cullen
public class AstralBody : MonoBehaviour, ISelectable {

    //Cullen
    public static Image selectedImage;
    public List<Orbital> orbitals;
    public List<LineRenderer> lines = new List<LineRenderer>();
    public GameObject orbitLine;

    // Written by Addison
    public GridLayoutGroup orbitalPanel;

    public Image tower1Image;
    public Image tower2Image;
    public Text towerDetailsTextPrefab;

    // Written by Cullen
    public void Start() {
        selectedImage = GameObject.Find("SelectedAstralBodyDisplay").GetComponent<Image>();

        for (int i = 0; i < orbitals.Count; i++) {
            lines.Add(Instantiate(orbitLine, transform).GetComponent<LineRenderer>());
        }

        //selectedImage = FindObjectOfType<Image>();
    }

    public void Update() {
        foreach (Orbital o in orbitals) {
            o.UpdateOrbital(transform);
        }
    }

    public void display() {
        // Written by Cullen
        selectedImage.sprite = GetComponent<SpriteRenderer>().sprite;

        // Written by Addison
        foreach (Transform child in this.transform) {
            GameObject gameObject = child.gameObject;

            bool isTower1 = (gameObject.tag == "Tower1");
            bool isTower2 = (gameObject.tag == "Tower2");

            if (isTower1 || isTower2) {
                Image towerImage;

                if (isTower1) {
                    towerImage = Instantiate(tower1Image) as Image;
                } else {
                    towerImage = Instantiate(tower2Image) as Image;
                }

                towerImage.transform.SetParent(orbitalPanel.transform, false);
                towerImage.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                towerImage.transform.localPosition = Vector3.zero;

                Text towerDetails;
                towerDetails = Instantiate(towerDetailsTextPrefab) as Text;

                if (isTower1) {
                    towerDetails.text = "Speed: 3, Range: 25\nDamage: 25, Cooldown: 1";
                } else {
                    towerDetails.text = "Speed: 3, Range: 40\nDamage: 100, Cooldown: 3";
                }

                towerDetails.transform.SetParent(orbitalPanel.transform, false);
                towerDetails.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                towerDetails.transform.localPosition = Vector3.zero;
            }
        }

        //Cullen
        for (int i = 0; i < orbitals.Count; i++) {
            orbitals[i].drawOrbital(lines[i]);
        }


    }

    public void undisplay() {
        // Written by Cullen
        selectedImage.sprite = null;

        // Wrriten by Addison
        foreach (Transform child in orbitalPanel.transform) {
            GameObject gameObject = child.gameObject;
            Destroy(gameObject);
        }

        foreach (LineRenderer l in lines) {
            l.positionCount = 0;
        }
        //orbitalLine.positionCount = 0;

    }

    // Written by Cullen
    public bool addTower(int orbital, Tower t) {
        int section = Mathf.RoundToInt(Random.Range(0, orbitals[orbital].sections));
        //Debug.Log(section);
        return orbitals[orbital].addTower(t, section);

    }

}
