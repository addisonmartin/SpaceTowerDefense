using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Cullen
[System.Serializable]
public class AstralBody : MonoBehaviour, ISelectable {

    //Cullen
    public static Image selectedImage;
    public List<Orbital> orbitals;
    public List<LineRenderer> lines = new List<LineRenderer>();
    public GameObject blankLine;
    public GameObject highlightOb;

    // Written by Addison
    public GameObject orbitalPanel;
    public GameObject towerListItemPrefab;
    public GameObject detailedTowerView;
    public GameObject detailedTowerViewPanel;

    private LineRenderer selectedOrbitSectionLine;

    // Written by Cullen
    public void Start() {
        for (int i = 0; i < orbitals.Count; i++) {
            lines.Add(Instantiate(blankLine, transform).GetComponent<LineRenderer>());
        }

        selectedOrbitSectionLine = Instantiate(blankLine, transform).GetComponent<LineRenderer>();
        selectedOrbitSectionLine.startColor = Color.red;
        selectedOrbitSectionLine.endColor = Color.red;
    }

    public void Update() {
        //Cullen
        if (Core.freeze && !Core.buildMode) {
            return;
        }
        //if (!Core.freeze) {
        foreach (Orbital o in orbitals) {
            o.UpdateOrbital(transform);
        }
        //}

        // Written by Addison
        if (Selectable.selected == GetComponent<Selectable>()) {
            Vector2 orbitAndSection = displayClosestOrbitalSection();

            //Cullen
            if (Core.player == null) {
                Core.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }

            if (Core.player.towerToPlace != null && orbitAndSection.x >= 0 && orbitAndSection.y >= 0) {
                if (Input.GetMouseButtonDown(0) && Core.mainCam.pixelRect.Contains(Input.mousePosition) && !Selectable.overrideClick) {
                    Core.player.addTower(this, (int)orbitAndSection.x, (int)orbitAndSection.y);
                    undisplay();
                    display((int)orbitAndSection.x);
                }
            }
        }
    }

    public void display(int orbitNum = -1, int sectionNum = -1) {

        // Written by Addison with small tweaks and fixes by Cullen
        for (int orbitalIndex = 0; orbitalIndex < orbitals.Count; orbitalIndex++) {
            for (int towerIndex = 0; towerIndex < orbitals[orbitalIndex].towers.Count; towerIndex++) {
                Tower tower = orbitals[orbitalIndex].towers[towerIndex];

                if (tower != null) {
                    GameObject towerView = Instantiate(towerListItemPrefab) as GameObject;
                    towerView.transform.SetParent(orbitalPanel.transform, false);
                    towerView.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    towerView.transform.localPosition = Vector3.zero;

                    // Silly Unity requires these to be local, unique variables because they are stored by reference
                    // DON'T REMOVE THESE VARS!
                    int tempOrbitalIndex = orbitalIndex;
                    int tempTowerIndex = towerIndex;
                    Tower tempTower = tower;
                    //GameObject tempDetailedTowerViewPanel = detailedTowerViewPanel;

                    EventTrigger.Entry entry = new EventTrigger.Entry();
                    entry.eventID = EventTriggerType.PointerEnter;
                    entry.callback.AddListener((eventData) => {
                        orbitals[tempOrbitalIndex].highlightTower(tempTowerIndex, Player.selectedTowerLine);
                    });
                    EventTrigger.Entry exit = new EventTrigger.Entry();
                    exit.eventID = EventTriggerType.PointerExit;
                    exit.callback.AddListener((eventData) => {
                        orbitals[tempOrbitalIndex].unhighlightTower(tempTowerIndex, Player.selectedTowerLine);
                    });
                    EventTrigger.Entry click = new EventTrigger.Entry();
                    click.eventID = EventTriggerType.PointerClick;
                    click.callback.AddListener((eventData) => {
                        detailedTowerView.GetComponent<OnClickFillView>().onClick(tempOrbitalIndex, tempTowerIndex, detailedTowerViewPanel, tempTower, this);
                    });
                    towerView.gameObject.GetComponent<EventTrigger>().triggers.Add(entry);
                    towerView.gameObject.GetComponent<EventTrigger>().triggers.Add(exit);
                    towerView.gameObject.GetComponent<EventTrigger>().triggers.Add(click);

                    if (orbitalIndex == orbitNum && towerIndex == orbitals[orbitalIndex].towers.Count - 1) {
                        towerView.gameObject.GetComponent<EventTrigger>().OnPointerClick(null);
                    }

                    foreach (Transform child in towerView.transform) {
                        // Set the details text.
                        Text t = child.gameObject.GetComponent<Text>();
                        if (t != null) {
                            t.text = tower.getName() + ", Orbital: " + (orbitalIndex + 1) + "\n" + tower.stats();
                        }

                        Image im = child.gameObject.GetComponent<Image>();
                        if (im != null) {
                            im.sprite = tower.GetComponent<SpriteRenderer>().sprite;
                        }
                    }
                }
            }
        }

        displayOrbitals();

        //Cullen
        //Display first tower automatically if this body was just selected and it has towers
        if (orbitalPanel.transform.childCount > 0 && Selectable.selected != GetComponent<Selectable>()) {
            EventTrigger element = orbitalPanel.transform.GetChild(orbitalPanel.transform.childCount - 1).GetComponent<EventTrigger>();
            element.OnPointerClick(null);
            //element.OnPointerEnter(null);
        }
    }

    //Cullen
    public void displayLastTower() {
        //undisplay();
        //display();
        if (orbitalPanel.transform.childCount > 1) {
            //Debug.Log("has children");
            EventTrigger element = orbitalPanel.transform.GetChild(orbitalPanel.transform.childCount - 1).GetComponent<EventTrigger>();
            element.OnPointerClick(null);
            element.OnPointerEnter(null);
        } else {
            //Debug.Log("no children");
            undisplay();
            Selectable.selected = null;
        }
    }

    //Cullen
    public void displayOrbitals() {
        for (int i = 0; i < orbitals.Count; i++) {
            orbitals[i].drawOrbital(lines[i]);
        }
    }

    // Written by Addison
    public Vector2 displayClosestOrbitalSection() {
        Vector3 mousePosition = Core.mainCam.ScreenToWorldPoint(Input.mousePosition);

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

    public void undisplay(bool a = true) {
        // Written by Cullen
        //selectedImage.sprite = null;
        //selectedImage.color = new Color(0f, 60f / 255f, 109f / 255f);

        foreach (LineRenderer l in lines) {
            l.positionCount = 0;
        }

        // Wrriten by Addison
        foreach (Transform child in orbitalPanel.transform) {
            GameObject gameObject = child.gameObject;
            Destroy(gameObject);
        }

        if (a) {
            detailedTowerView.GetComponent<OnClickFillView>().clear();
            detailedTowerView.gameObject.SetActive(false);
        }
        selectedOrbitSectionLine.positionCount = 0;

        

    }

    public void highlight(bool h) {
        highlightOb.SetActive(h);
    }
}
