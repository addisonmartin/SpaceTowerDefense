using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Text scrapDisplay;

    private int scrap = 1000;
    private float hp = 100f;

    public Text gameOverText;

    public Tower selectedTower = null;

    // Start is called before the first frame update
    void Start() {
        scrapDisplay.text = "" + scrap;
        gameOverText.text = "";
    }

    // Written by Addison
    // Update is called once per frame
    void Update() {

        if (selectedTower == null) {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        if (Input.GetMouseButtonDown(1)) {
            selectTower(null);
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
            selectedTower = null;
            return;
        }

        if (scrap >= t.scrapCost) {
            // Changes the mouse's icon to the tower the user has selected
            SpriteRenderer sp = t.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            Texture2D texture = sp.sprite.texture;
            Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);

            selectedTower = t;
        } else {
            selectedTower = null;
        }
    }

    // Written by Addison
    public void addTower(AstralBody body, int orbital, int section) {

        // Selected Tower is checked to not be null in AstralBody's update, which calls this function.
        // This scrap cost check should be a redudant check, but can't hurt.
        if (scrap >= selectedTower.scrapCost) {

            // Written by Cullen
            Transform parent = body.gameObject.transform;
            float scaleAdjust = parent.GetComponent<CircleCollider2D>().radius * parent.lossyScale.x;
            Tower t = Instantiate(selectedTower, parent.position, Quaternion.identity) as Tower;
            t.transform.SetParent(parent, true);

            // Written by Addison
            if (body.orbitals[orbital].addTower(t, section)) {
                addScrap(-selectedTower.scrapCost);
            } else {
                Destroy(t.gameObject);
            }
        }

        if (!Input.GetButton("Shift")) {
            selectedTower = null;
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
