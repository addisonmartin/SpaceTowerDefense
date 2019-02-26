﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Cullen
[System.Serializable]
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

    private LineRenderer selectedOrbitSectionLine;

    // Written by Cullen
    public void Start() {
        selectedImage = GameObject.Find("SelectedAstralBodyDisplay").GetComponent<Image>();

        for (int i = 0; i < orbitals.Count; i++) {
            lines.Add(Instantiate(orbitLine, transform).GetComponent<LineRenderer>());
        }

        selectedOrbitSectionLine = Instantiate(orbitLine, transform).GetComponent<LineRenderer>();
        selectedOrbitSectionLine.startColor = Color.red;
        selectedOrbitSectionLine.endColor = Color.red;

        //selectedImage = FindObjectOfType<Image>();
    }

    public void Update() {
        foreach (Orbital o in orbitals) {
            o.UpdateOrbital(transform);
        }

        if (Selectable.selected == GetComponent<Selectable>()) {
           displayClosestOrbitalSection();
        }
    }

    public void display() {
        // Written by Cullen
        selectedImage.sprite = GetComponent<SpriteRenderer>().sprite;

        displayTowersInOrbitals();
        displayOrbitals();
        displayClosestOrbitalSection();
    }

    // Written by Addison
    public void displayTowersInOrbitals() {
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
   }

   //Cullen
   public void displayOrbitals()
   {
      for (int i = 0; i < orbitals.Count; i++) {
          orbitals[i].drawOrbital(lines[i]);
      }
   }

   // Written by Addison
   public void displayClosestOrbitalSection()
   {
      Vector3 mousePosition = Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition);

      float minDistance = Mathf.Infinity;
      int orbitalNum = -1;
      int sectionNum = -1;

      for (int orbitalIndex = 0; orbitalIndex < orbitals.Count; orbitalIndex++)
      {
         for (int sectionIndex = 0; sectionIndex < orbitals[orbitalIndex].sections; sectionIndex++)
         {
            float sectionPhase = ((2 * Mathf.PI) / orbitals[orbitalIndex].sections) * sectionIndex;
            float halfSectionPhase = Mathf.PI / orbitals[orbitalIndex].sections;
            float x = transform.position.x + (orbitals[orbitalIndex].p * Mathf.Cos(sectionPhase + halfSectionPhase));
            float y = transform.position.y + (orbitals[orbitalIndex].p * orbitals[orbitalIndex].ratio * Mathf.Sin(sectionPhase + halfSectionPhase));

            float distance = Vector3.Distance(mousePosition, new Vector3(x, y, 0));
            if (distance < minDistance)
            {
               minDistance = distance;
               orbitalNum = orbitalIndex;
               sectionNum = sectionIndex;
            }
         }
      }

      float startPhase = ((2 * Mathf.PI) / orbitals[orbitalNum].sections) * sectionNum;
      float endPhase = ((2 * Mathf.PI) / orbitals[orbitalNum].sections) * (sectionNum + 1);

      orbitals[orbitalNum].drawSelectedOrbital(selectedOrbitSectionLine, startPhase, endPhase);

   }

    public void undisplay() {
        // Written by Cullen
        selectedImage.sprite = null;

        foreach (LineRenderer l in lines) {
            l.positionCount = 0;
        }

        // Wrriten by Addison
        foreach (Transform child in orbitalPanel.transform) {
            GameObject gameObject = child.gameObject;
            Destroy(gameObject);
        }

        selectedOrbitSectionLine.positionCount = 0;

    }

    // Written by Cullen
    public bool addTower(int orbital, Tower t) {
        int section = Mathf.RoundToInt(Random.Range(0, orbitals[orbital].sections));
        return orbitals[orbital].addTower(t, section);

    }

}
