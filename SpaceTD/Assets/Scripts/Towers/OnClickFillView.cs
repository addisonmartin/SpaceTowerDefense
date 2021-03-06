﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Written by Addison
public class OnClickFillView : MonoBehaviour {
    public Text towerName;
    public Text towerStats;
    public Text towerDescription;
    public Button clockwiseButton;
    public Button counterClockwiseButton;
    public Image im;
    public Button upgradeButton;
    public Button sellButton;
    public Text upgradeText;
    public Text sellText;

    private GameObject towerViewPanel;
    private bool valid = false;

    // Written by Addison with small tweaks by Cullen
    public void onClick(int orbitalIndex, int towerIndex, GameObject towerViewPanel, Tower tower, AstralBody ab) {
        clear();
        valid = true;
        gameObject.SetActive(true);
        transform.SetParent(towerViewPanel.gameObject.transform, false);
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        transform.localPosition = Vector3.zero;

        this.towerViewPanel = towerViewPanel;

        towerName.text = tower.getName();
        clockwiseButton.gameObject.GetComponent<Button>().onClick.AddListener(() => {
            if (Core.paused) {
                return;
            }
            ab.orbitals[orbitalIndex].shiftTower(towerIndex, -1);
        });
        counterClockwiseButton.gameObject.GetComponent<Button>().onClick.AddListener(() => {
            if (Core.paused) {
                return;
            }
            ab.orbitals[orbitalIndex].shiftTower(towerIndex, 1);
        });
        im.sprite = tower.GetComponent<SpriteRenderer>().sprite;
        towerStats.text = tower.stats();
        towerDescription.text = tower.getDescription();

        if (tower.getStage() >= tower.getMaxStage()) {
            upgradeText.text = "  MAXED";
        } else {
            upgradeText.text = "Upgrade: -" + tower.upgradeCost();
        }
        sellText.text = "Sell: +" + tower.sellValue();

        // Written by Cullen
        upgradeButton.onClick.AddListener(() => {
            if (Core.paused) {
                return;
            }
            if (tower.getStage() >= tower.getMaxStage()) {
                Core.Alert("Cannot be upgraded further!");
                return;
            }
            int cost = tower.upgrade(Core.player.getScrap());
            if (cost == 0) {
                Core.Alert("Not enough scrap!");
            }
            Core.player.addScrap(-cost);
            ab.orbitals[orbitalIndex].highlightTower(towerIndex, Player.selectedTowerLine);
            ab.undisplay(false);
            ab.display();
            towerStats.text = tower.nextStats();
            towerDescription.text = tower.getDescription();
            if (tower.getStage() >= tower.getMaxStage()) {
                upgradeText.text = "Upgrade: MAXED";
            } else {
                upgradeText.text = "Upgrade: -" + tower.upgradeCost();
            }
            sellText.text = "Sell: +" + tower.sellValue();
            //gameObject.SetActive(true);
            //onClick(orbitalIndex, towerIndex, towerViewPanel, tower, ab);
        });
        //Cullen
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => {
            if (Core.paused) {
                return;
            }
            towerStats.text = tower.nextStats();
        });
        upgradeButton.GetComponent<EventTrigger>().triggers.Add(entry);
        //Cullen
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((eventData) => {
            towerStats.text = tower.stats();
        });
        upgradeButton.GetComponent<EventTrigger>().triggers.Add(entry);

        // Written by Cullen
        sellButton.onClick.AddListener(() => {
            if (Core.paused) {
                return;
            }
            Core.player.addScrap(tower.sellValue());
            ab.orbitals[orbitalIndex].unhighlightTower(towerIndex, Player.selectedTowerLine);
            ab.orbitals[orbitalIndex].removeTower(tower);
            Destroy(tower.gameObject);
            ab.undisplay();
            ab.display();
            clear();
            ab.displayLastTower();
        });

        // Written by Addison
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => {
            if (Core.paused) {
                return;
            }
            ab.orbitals[orbitalIndex].highlightTower(towerIndex, Player.selectedTowerLine);
        });
        towerViewPanel.gameObject.GetComponent<EventTrigger>().triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((eventData) => {
            ab.orbitals[orbitalIndex].unhighlightTower(towerIndex, Player.selectedTowerLine);
        });
        towerViewPanel.gameObject.GetComponent<EventTrigger>().triggers.Add(entry);

    }

    //Cullen
    public void clear() {
        clockwiseButton.onClick.RemoveAllListeners();
        counterClockwiseButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.RemoveAllListeners();
        sellButton.onClick.RemoveAllListeners();

        if (towerViewPanel != null) {
            towerViewPanel.GetComponent<EventTrigger>().triggers.Clear();
        }
        valid = false;
        gameObject.SetActive(false);
    }

    //Cullen
    public void enable() {
        if (valid) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }
}
