using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Written by Addison
public class OnClickFillView : MonoBehaviour
{
   public GameObject towerViewPrefab;

   public Text towerName;
   public Text towerStats;
   public Text towerDescription;
   public Button clockwiseButton;
   public Button counterClockwiseButton;
   public Image im;
   public Button upgradeButton;
   public Button sellButton;

   // Written by Addison
   public void onClick(int orbitalIndex, int towerIndex, GameObject towerViewPanel, Tower tower, AstralBody ab)
   {
      GameObject towerView = Instantiate(towerViewPrefab) as GameObject;
      towerView.transform.SetParent(towerViewPanel.gameObject.transform, false);
      towerView.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
      towerView.transform.localPosition = Vector3.zero;

      towerName.text = tower.getName();
      clockwiseButton.gameObject.GetComponent<Button>().onClick.AddListener(() => ab.orbitals[orbitalIndex].shiftTower(towerIndex, -1));
      counterClockwiseButton.gameObject.GetComponent<Button>().onClick.AddListener(() => ab.orbitals[orbitalIndex].shiftTower(towerIndex, 1));
      im.sprite = tower.GetComponent<SpriteRenderer>().sprite;
      towerStats.text = tower.getDetails() + " Orbital: " + (orbitalIndex + 1);
      towerDescription.text = tower.getDescription();
      upgradeButton.GetComponent<Button>().onClick.AddListener(() => {
         int cost = tower.upgrade(Core.player.getScrap());
         if (cost == 0) {
            Core.notEnoughScrap();
         }
         Core.player.addScrap(-cost);
         ab.orbitals[orbitalIndex].highlightTower(towerIndex, Player.selectedTowerLine);
         ab.undisplay();
         ab.display();
      });
      sellButton.onClick.AddListener(() => {
         Core.player.addScrap(tower.sellValue());
         ab.orbitals[orbitalIndex].unhighlightTower(towerIndex, Player.selectedTowerLine);
         ab.orbitals[orbitalIndex].removeTower(tower);
         Destroy(tower.gameObject);
         ab.undisplay();
         ab.display();
      });
   }
}
