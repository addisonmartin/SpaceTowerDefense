﻿using System.Collections;
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
    public GameObject orbitLine;

    // Written by Addison
    public GameObject orbitalPanel;
    public GameObject towerViewPrefab;

    private LineRenderer selectedOrbitSectionLine;
    protected static Player player = null;

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
        if (!Core.freeze) {
            foreach (Orbital o in orbitals) {
                o.UpdateOrbital(transform);
            }
        }

        // Written by Addison
        if (Selectable.selected == GetComponent<Selectable>()) {
            Vector2 orbitAndSection = displayClosestOrbitalSection();

            if (player == null) {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>() as Player;
            }

            if (player.towerToPlace != null && orbitAndSection.x >= 0 && orbitAndSection.y >= 0) {
                if (Input.GetMouseButtonDown(0) && Core.mainCam.pixelRect.Contains(Input.mousePosition)) {
                    player.addTower(this, (int)orbitAndSection.x, (int)orbitAndSection.y);
                    undisplay();
                    display();
                }
            }
        }
    }

    // Written by Addison
    //void upgradeTower(Tower t) {
    //    if (player == null) {
    //        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>() as Player;
    //    }
    //    Debug.Log(player.scrap);
    //    player.scrap -= t.upgrade();
    //    Debug.Log(player.scrap);
    //}

    // Written by Cullen
    void shiftTower(int orbitalIndex, int towerIndex, int shift) {
        orbitals[orbitalIndex].shiftTower(towerIndex, shift);
    }

    public void display() {
        // Written by Cullen
        selectedImage.sprite = GetComponent<SpriteRenderer>().sprite;
        selectedImage.color = Color.white;
        
        // Written by Addison
        for (int orbitalIndex = 0; orbitalIndex < orbitals.Count; orbitalIndex++) {
            for (int towerIndex = 0; towerIndex < orbitals[orbitalIndex].towers.Count; towerIndex++) {
                Tower tower = orbitals[orbitalIndex].towers[towerIndex];

                if (tower != null) {
                    GameObject towerView = Instantiate(towerViewPrefab) as GameObject;
                    towerView.transform.SetParent(orbitalPanel.transform, false);
                    towerView.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    towerView.transform.localPosition = Vector3.zero;

                    // Silly Unity requires these to be local, unique variables or they get garbage collected I think?
                    // Idk why, JUST DON'T REMOVE THESE FOUR VARS!
                    int tempOrbitalIndexA = orbitalIndex;
                    int tempTowerIndexA = towerIndex;
                    int tempOrbitalIndexB = orbitalIndex;
                    int tempTowerIndexB = towerIndex;
                    //Tower tempTower = tower; //TODO Does this need to be a copy?

                    EventTrigger.Entry entry = new EventTrigger.Entry();
                    entry.eventID = EventTriggerType.PointerEnter;
                    entry.callback.AddListener((eventData) => {
                        orbitals[tempOrbitalIndexA].highlightTower(tempTowerIndexA, Player.selectedTowerLine);
                        Player.selectedTowerHL.transform.SetParent(orbitals[tempOrbitalIndexA].towers[tempTowerIndexA].transform, false);
                    });
                    EventTrigger.Entry exit = new EventTrigger.Entry();
                    exit.eventID = EventTriggerType.PointerExit;
                    exit.callback.AddListener((eventData) => {
                        orbitals[tempOrbitalIndexA].unhighlightTower(tempTowerIndexA, Player.selectedTowerLine, player);
                    });
                    towerView.gameObject.GetComponent<EventTrigger>().triggers.Add(entry);
                    towerView.gameObject.GetComponent<EventTrigger>().triggers.Add(exit);

                    foreach (Transform child in towerView.transform) {

                        // Set the tower move clockwise buttons to move the correct tower.
                        if (child.CompareTag("ClockwiseButton")) {
                            child.gameObject.GetComponent<Button>().onClick.AddListener(() => shiftTower(tempOrbitalIndexA, tempTowerIndexA, -1));
                        }
                        // Set the tower move cclockwise button to move the correct tower.
                        else if (child.CompareTag("CounterClockwise")) {
                            child.gameObject.GetComponent<Button>().onClick.AddListener(() => shiftTower(tempOrbitalIndexB, tempTowerIndexB, 1));
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

                        // Link the upgrade button.
                        if (child.CompareTag("UpgradeButton")) {
                            Debug.Log("Button found");

                            child.GetChild(0).GetComponent<Button>().onClick.AddListener(() => {
                                player.addScrap(-tower.upgrade(player.getScrap()));
                                orbitals[tempOrbitalIndexA].highlightTower(tempTowerIndexA, Player.selectedTowerLine);
                                undisplay();
                                display();
                                });
                            child.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                                player.addScrap(tower.sellValue());
                                orbitals[tempOrbitalIndexA].unhighlightTower(tempTowerIndexA, Player.selectedTowerLine, player);
                                orbitals[tempOrbitalIndexA].Remove(tower);
                                Destroy(tower.gameObject);
                                undisplay();
                                display();
                            });
                        }
                    }
                }
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

    public void undisplay() {
        // Written by Cullen
        selectedImage.sprite = null;
        selectedImage.color = new Color(0f, 60f / 255f, 109f / 255f);

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
