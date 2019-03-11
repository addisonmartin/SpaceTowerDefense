using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Text scrapDisplay;

    public int scrap = 1000;
    private float hp = 100f;

    public static LineRenderer selectedTowerLine;
    public GameObject selectedTowerLineObject;
    public GameObject selectedTowerHighlight;
    public static GameObject selectedTowerHL;

    // Written by Addison
    public Tower towerToPlace = null;
    //public GameObject hoveredTowerView = null;
    private Texture2D cursor;
    private AudioSource aud;

    // Start is called before the first frame update
    void Start() {
        Cursor.visible = true;
        if (selectedTowerLine == null) {
            selectedTowerLine = selectedTowerLineObject.GetComponent<LineRenderer>();
            selectedTowerHL = selectedTowerHighlight;
        }
        scrapDisplay.text = "" + scrap;
        aud = GetComponent<AudioSource>();
    }

    public int getNumTowers() {
        int towers = 0;
        AstralBody[] abs = GetComponents<AstralBody>();
        foreach (AstralBody ab in abs) {
            for (int i = 0; i < ab.orbitals.Count; i++) {
                towers += ab.orbitals[i].towers.Count;
            }
        }
        return towers;
    }

    public bool hasUpgradedTower() {
        AstralBody[] abs = GetComponents<AstralBody>();
        foreach (AstralBody ab in abs) {
            for (int i = 0; i < ab.orbitals.Count; i++) {
                for (int j = 0; j < ab.orbitals[i].towers.Count; j++) {
                    if (ab.orbitals[i].towers[j].getStage() > 0) {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    // Written by Addison
    // Update is called once per frame
    void Update() {

        //if (towerToPlace == null) {
        //    //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        //    Cursor.visible = true;
        //}
        if (Input.GetMouseButtonDown(1)) {
            selectTower(null);
        }
    }

    //Cullen
    private void OnGUI() {
        if (!Cursor.visible) {
            GUI.DrawTexture(new Rect(Input.mousePosition.x - cursor.width / 2, Screen.height - Input.mousePosition.y - cursor.height / 2, cursor.width, cursor.height), cursor);
        }
    }

    //Cullen
    public void addScrap(int s) {
        scrap += s;
        if (s > 0) {
            aud.Play();
        }
        scrapDisplay.text = "" + scrap;
    }

    public int getScrap() {
        return scrap;
    }

    //Cullen
    public void takeDamage(float d) {
        hp -= d;
        GetComponent<Healthbar>().setHealth(hp);
        if (hp <= 0f) {
            StartCoroutine(Core.gameOverWait());
        }
    }

    // Written by Addison
    public void selectTower(Tower t) {

        if (t == null) {
            towerToPlace = null;
            Cursor.visible = true;
            return;
        }

        if (Selectable.selected == null) {
            Selectable.lastSelected.select();
        }

        if (scrap >= t.scrapCost) {
            cursor = t.GetComponent<SpriteRenderer>().sprite.texture;
            Cursor.visible = false;
            towerToPlace = t;
        } else {
            Core.notEnoughScrap();
            towerToPlace = null;
        }
    }

    // Written by Addison
    public void addTower(AstralBody body, int orbital, int section) {

        // Selected Tower is checked to not be null in AstralBody's update, which calls this function.
        // This scrap cost check should be a redudant check, but can't hurt.
        if (scrap >= towerToPlace.scrapCost) {

            // Written by Cullen
            Transform parent = body.gameObject.transform;
            float scaleAdjust = parent.GetComponent<CircleCollider2D>().radius * parent.lossyScale.x;
            Tower t = Instantiate(towerToPlace, parent.position, Quaternion.identity) as Tower;
            t.transform.SetParent(parent, true);

            // Written by Addison
            if (body.orbitals[orbital].addTower(t, section)) {
                addScrap(-towerToPlace.scrapCost);
            } else {
                Destroy(t.gameObject);
            }
        } else {
            Core.notEnoughScrap();
        }

        if (!Input.GetKey(KeyCode.LeftShift)) {
            selectTower(null);
        }
    }

}
