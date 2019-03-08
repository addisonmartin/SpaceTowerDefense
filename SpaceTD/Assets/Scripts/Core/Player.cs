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

    public Text gameOverText;

    // Written by Addison
    public Tower towerToPlace = null;
    public GameObject hoveredTowerView = null;
    private Texture2D cursor;

    // Start is called before the first frame update
    void Start() {
        if (selectedTowerLine == null) {
            selectedTowerLine = selectedTowerLineObject.GetComponent<LineRenderer>();
            selectedTowerHL = selectedTowerHighlight;
        }
        scrapDisplay.text = "" + scrap;
        gameOverText.text = "";
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

    private void OnGUI() {
        if (!Cursor.visible) {
            GUI.DrawTexture(new Rect(Input.mousePosition.x - cursor.width/2, Screen.height - Input.mousePosition.y - cursor.height/2, cursor.width, cursor.height), cursor);
        }
    }

    public void addScrap(int s) {
        scrap += s;
        scrapDisplay.text = "" + scrap;
    }

    public int getScrap() {
        return scrap;
    }

    public void takeDamage(float d) {
        hp -= d;
        GetComponent<Healthbar>().setHealth(hp);
        if (hp <= 0f) {
            gameOver();
        }
    }

    // Written by Addison
    public void selectTower(Tower t) {

        if (t == null) {
            towerToPlace = null;
            Cursor.visible = true;
            return;
        }

        if (scrap >= t.scrapCost) {
            // Changes the mouse's icon to the tower the user has selected
            SpriteRenderer sp = t.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            cursor = sp.sprite.texture;
            //Texture2D texture = new Texture2D(sp.sprite.texture.width, sp.sprite.texture.height, TextureFormat.RGBA32, false);
            //texture.LoadRawTextureData(sp.sprite.texture.GetRawTextureData());
            //texture.Apply();
            Cursor.visible = false;
            //texture.ClearRequestedMipmapLevel();
            //Cursor.SetCursor(texture, new Vector2(texture.width/2, texture.height/2), CursorMode.Auto);

            towerToPlace = t;
        } else {
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
        }

        if (!Input.GetKey(KeyCode.LeftShift)) {
            selectTower(null);
        }
    }

    public void gameOver() {
        StartCoroutine(gameOverWait());
    }

    IEnumerator gameOverWait() {

        gameOverText.text = "Game Over!\nPress Spacebar";

        while (!Input.GetKeyDown(KeyCode.Space) && !Input.GetKeyDown(KeyCode.Escape)) {
            yield return null;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit(0);
        }

        SceneManager.LoadScene(1);

    }

}
