using System.Collections;
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

    public Image towerImage;
    public Text towerDetailsTextPrefab;

    private LineRenderer selectedOrbitSectionLine;
    protected static Player player = null;

    public Button counterClockwiseButtonPrefab;
    public Button clockwiseButtonPrefab;

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

        // Written by Addison
        if (Selectable.selected == GetComponent<Selectable>()) {
            Vector2 orbitAndSection = displayClosestOrbitalSection();

            if (player == null)
            {
               player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>() as Player;
            }

            if (player.selectedTower != null && orbitAndSection.x >= 0 && orbitAndSection.y >= 0)
            {
               if (Input.GetMouseButtonDown(0)) {
                  player.addTower(this, (int)orbitAndSection.x, (int)orbitAndSection.y);
               }
            }
        }
    }

    public void display() {
        // Written by Cullen
        selectedImage.sprite = GetComponent<SpriteRenderer>().sprite;

        // Written by Addison
        foreach (Transform child in this.transform) {
            Tower tower = child.gameObject.GetComponent<Tower>();

            if (tower != null) {

                Button counterClockwiseButton = Instantiate(counterClockwiseButtonPrefab) as Button;
                counterClockwiseButton.transform.SetParent(orbitalPanel.transform, false);
                counterClockwiseButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                counterClockwiseButton.transform.localPosition = Vector3.zero;

                Image tImage;
                tImage = Instantiate(towerImage) as Image;
                tImage.sprite = tower.GetComponent<SpriteRenderer>().sprite;

                tImage.transform.SetParent(orbitalPanel.transform, false);
                tImage.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                tImage.transform.localPosition = Vector3.zero;

                Button clockwiseButton = Instantiate(clockwiseButtonPrefab) as Button;
                clockwiseButton.transform.SetParent(orbitalPanel.transform, false);
                clockwiseButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                clockwiseButton.transform.localPosition = Vector3.zero;

                Text towerDetails;
                Text towerName;
                towerDetails = Instantiate(towerDetailsTextPrefab) as Text;
                towerName = Instantiate(towerDetailsTextPrefab) as Text;
                towerDetails.lineSpacing = .5f;
                towerDetails.alignment = (TextAnchor)TextAlignment.Right;

                towerDetails.text = tower.getDetails();
                towerName.text = tower.getName();

                towerName.transform.SetParent(orbitalPanel.transform, false);
                towerName.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                towerName.transform.localPosition = Vector3.zero;

                towerDetails.transform.SetParent(orbitalPanel.transform, false);
                towerDetails.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                towerDetails.transform.localPosition = Vector3.zero;
            }
        }

        displayOrbitals();
    }

    //Cullen
    public void displayOrbitals() {
        for (int i = 0; i < orbitals.Count; i++) {
            orbitals[i].drawOrbital(lines[i]);
        }
    }

    // Written by Addison
    public Vector2 displayClosestOrbitalSection() {
        Vector3 mousePosition = Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition);

        float minDistance = Mathf.Infinity;
        int orbitalNum = -1;
        int sectionNum = -1;

        for (int orbitalIndex = 0; orbitalIndex < orbitals.Count; orbitalIndex++) {
            for (int sectionIndex = 0; sectionIndex < orbitals[orbitalIndex].sections; sectionIndex++) {
                float sectionPhase = ((2 * Mathf.PI) / orbitals[orbitalIndex].sections) * sectionIndex;
                float halfSectionPhase = Mathf.PI / orbitals[orbitalIndex].sections;
                float x = transform.position.x + (orbitals[orbitalIndex].p * Mathf.Cos(sectionPhase + halfSectionPhase));
                float y = transform.position.y + (orbitals[orbitalIndex].p * orbitals[orbitalIndex].ratio * Mathf.Sin(sectionPhase + halfSectionPhase));

                float distance = Vector3.Distance(mousePosition, new Vector3(x, y, 0));
                if (distance < minDistance) {
                    minDistance = distance;
                    orbitalNum = orbitalIndex;
                    sectionNum = sectionIndex;
                }
            }
        }

        float startPhase = ((2 * Mathf.PI) / orbitals[orbitalNum].sections) * sectionNum;
        float endPhase = ((2 * Mathf.PI) / orbitals[orbitalNum].sections) * (sectionNum + 1);

        orbitals[orbitalNum].drawSelectedOrbital(selectedOrbitSectionLine, startPhase, endPhase);

        return new Vector2((float)orbitalNum, (float)sectionNum);

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
}
