using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Cullen
public class AstralBody : MonoBehaviour, ISelectable {

    public static Image selectedImage;
    public List<Orbital> orbitals;
    //private const int ORBITAL_0_MAX = 4;
    //private List<Tower> orbital0 = new List<Tower>();
    //private const int ORBITAL_1_MAX = 8;
    //private List<Tower> orbital1 = new List<Tower>();

    // Written by Addison
    public GridLayoutGroup orbitalPanel;

    public Image tower1Image;
    public Image tower2Image;
    public Text towerDetailsTextPrefab;

    // Written by Cullen
    public void Start() {
        selectedImage = GameObject.Find("SelectedAstralBodyDisplay").GetComponent<Image>();
        //selectedImage = FindObjectOfType<Image>();
    }

    public void Update() {
        foreach (Orbital o in orbitals) {
            o.Update();
        }
    }

    public void display() {
         // Written by Cullen
         selectedImage.sprite = GetComponent<SpriteRenderer>().sprite;

         // Written by Addison
         foreach (Transform child in this.transform)
         {
            GameObject gameObject = child.gameObject;

            bool isTower1 = (gameObject.tag == "Tower1");
            bool isTower2 = (gameObject.tag == "Tower2");

            if (isTower1 || isTower2)
            {
               Image towerImage;

               if (isTower1)
               {
                  towerImage = Instantiate(tower1Image) as Image;
               }
               else
               {
                  towerImage = Instantiate(tower2Image) as Image;
               }

               towerImage.transform.SetParent(orbitalPanel.transform, false);
               towerImage.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
               towerImage.transform.localPosition = Vector3.zero;

               Text towerDetails;
               towerDetails = Instantiate(towerDetailsTextPrefab) as Text;

               if (isTower1)
               {
                  towerDetails.text = "Speed: 3, Range: 25\nDamage: 25, Cooldown: 1";
               }
               else
               {
                  towerDetails.text = "Speed: 3, Range: 40\nDamage: 100, Cooldown: 3";
               }

               towerDetails.transform.SetParent(orbitalPanel.transform, false);
               towerDetails.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
               towerDetails.transform.localPosition = Vector3.zero;
            }
         }
    }

    public void undisplay() {
         // Written by Cullen
         selectedImage.sprite = null;

         // Wrriten by Addison
         foreach (Transform child in orbitalPanel.transform)
         {
            GameObject gameObject = child.gameObject;
            Destroy(gameObject);
         }
    }

    // Written by Cullen
    public bool addTower(int orbital, Tower t) {

        return orbitals[orbital].addTower(t, 0);

        //switch (orbital) {
        //    case 0:
        //        if (orbital0.Count >= ORBITAL_0_MAX) {
        //            return false;
        //        }
        //        t.setOrbital(0);
        //        orbital0.Add(t);
        //        undisplay();
        //        display();
        //        return true;
        //    case 1:
        //        if (orbital1.Count >= ORBITAL_1_MAX) {
        //            return false;
        //        }
        //        t.setOrbital(1);
        //        orbital1.Add(t);
        //        undisplay();
        //        display();
        //        return true;
        //    default:
        //        return false;
        //}

    }

}
