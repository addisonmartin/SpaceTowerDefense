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

    // Start is called before the first frame update
    void Start() {
        scrapDisplay.text = "" + scrap;
        gameOverText.text = "";
    }

    // Update is called once per frame
    void Update() {

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
        if (hp <= 0f) {
            gameOver();
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

        SceneManager.LoadScene(2);

    }

}
