using System.Collections;
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
        clockwiseButton.gameObject.GetComponent<Button>().onClick.AddListener(() => ab.orbitals[orbitalIndex].shiftTower(towerIndex, -1));
        counterClockwiseButton.gameObject.GetComponent<Button>().onClick.AddListener(() => ab.orbitals[orbitalIndex].shiftTower(towerIndex, 1));
        im.sprite = tower.GetComponent<SpriteRenderer>().sprite;
        towerStats.text = tower.stats();
        towerDescription.text = tower.getDescription();
        // Written by Cullen
        upgradeButton.onClick.AddListener(() => {
            int cost = tower.upgrade(Core.player.getScrap());
            if (cost == 0) {
                Core.notEnoughScrap();
            }
            Core.player.addScrap(-cost);
            ab.orbitals[orbitalIndex].highlightTower(towerIndex, Player.selectedTowerLine);
            ab.undisplay();
            ab.display();
            towerStats.text = tower.nextStats();
            towerDescription.text = tower.getDescription();
            gameObject.SetActive(true);
        });
        //Cullen
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => {
            towerStats.text = tower.nextStats();
        });
        upgradeButton.GetComponent<EventTrigger>().triggers.Add(entry);
        //Cullen
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((eventData) => {
            towerStats.text = tower.stats(); ;
        });
        upgradeButton.GetComponent<EventTrigger>().triggers.Add(entry);

        // Written by Cullen
        sellButton.onClick.AddListener(() => {
            Core.player.addScrap(tower.sellValue());
            ab.orbitals[orbitalIndex].unhighlightTower(towerIndex, Player.selectedTowerLine);
            ab.orbitals[orbitalIndex].removeTower(tower);
            Destroy(tower.gameObject);
            ab.undisplay();
            ab.display();
            clear();
        });

        // Written by Addison
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => {
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
    private void clear() {
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

    public void enable() {
        if (valid) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }
}
