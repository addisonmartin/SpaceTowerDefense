﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Written by Addison
public class OnClickFillView : MonoBehaviour
{
   public GameObject towerViewPrefab;

   // Written by Addison
   public void onClick(int orbitalIndex, int towerIndex, GameObject towerViewPanel, Tower tower, AstralBody ab)
   {
      GameObject towerView = Instantiate(towerViewPrefab) as GameObject;
      towerView.transform.SetParent(towerViewPanel.gameObject.transform, false);
      towerView.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
      towerView.transform.localPosition = Vector3.zero;

      foreach (Transform child in towerView.transform)
      {
         // Set the tower move clockwise buttons to move the correct tower.
         if (child.CompareTag("ClockwiseButton")) {
             child.gameObject.GetComponent<Button>().onClick.AddListener(() => ab.orbitals[orbitalIndex].shiftTower(towerIndex, -1));
         }
         // Set the tower move cclockwise button to move the correct tower.
         else if (child.CompareTag("CounterClockwise")) {
             child.gameObject.GetComponent<Button>().onClick.AddListener(() => ab.orbitals[orbitalIndex].shiftTower(towerIndex, 1));
         }

         // Set the tower image to the correct tower.
         Image im = child.gameObject.GetComponent<Image>();
         if (im != null && im.gameObject.tag == "Tower") {
             im.sprite = tower.GetComponent<SpriteRenderer>().sprite;
         }

         // Set the details text.
         Text t = child.gameObject.GetComponent<Text>();
         if (t != null) {
             t.text = tower.getName() + "\n" + tower.getDetails() + " Orbital: " + (orbitalIndex + 1);
         }

         // Link the upgrade and sell buttons.
         if (child.CompareTag("UpgradeButton")) {

             child.GetChild(0).GetComponent<Button>().onClick.AddListener(() => {
                 int cost = tower.upgrade(Core.player.getScrap());
                 if (cost == 0) {
                     Core.notEnoughScrap();
                 }
                 Core.player.addScrap(-cost);
                 ab.orbitals[orbitalIndex].highlightTower(towerIndex, Player.selectedTowerLine);
                 ab.undisplay();
                 ab.display();
             });
             child.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                 Core.player.addScrap(tower.sellValue());
                 ab.orbitals[orbitalIndex].unhighlightTower(towerIndex, Player.selectedTowerLine);
                 ab.orbitals[orbitalIndex].removeTower(tower);
                 Destroy(tower.gameObject);
                 ab.undisplay();
                 ab.display();
             });
         }
      }
   }
}
