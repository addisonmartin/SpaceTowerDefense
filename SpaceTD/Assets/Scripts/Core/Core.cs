using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Core : MonoBehaviour {
    public static bool freeze;
    public static int levelNum;
    public int level;
    public static int waveNum;
    public static Camera mainCam;
    public static Camera uiCam;
    public static Player player;
    public static WaveSpawner waveSpawner;
    public static bool buildMode = true;
    public Text buildText;
    public Text alertText;
    private static Text alert;
    private static float alertTime = 0f;

    // Start is called before the first frame update
    void Start() {
        freeze = true;

        //Cullen
        if (buildText != null) {
            buildMode = true;
            buildText.text = "BUILD INITIAL DEFENSES\nPRESS SPACE TO START";
        }
        levelNum = level;
        alert = alertText;
        alert.text = "";
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        uiCam = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        waveSpawner = GetComponent<WaveSpawner>();
        waveNum = 0;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            freeze = !freeze;
        }
        //Cullen
        if (buildMode && Input.GetKeyDown(KeyCode.Space)) {
            buildMode = false;
            freeze = false;
            buildText.text = "";
        }
        //Cullen
        if (alertTime > 0f) {
            alertTime -= Time.deltaTime;
            alert.color = new Color(alert.color.r, alert.color.g, alert.color.b, alertTime / 2f);
        } else {
            alert.text = "";
        }

        if (buildText != null && waveNum >= waveSpawner.waves.Length && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
            buildText.text = "LEVEL COMPLETE!\nPRESS SPACE TO RETURN TO MENU";
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(0);
            }
        }
    }

    //Cullen
    public static void orbitalFull() {
        alert.text = "This orbital is full!";
        alert.transform.position = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition);
        alertTime = 2f;
    }

    //Cullen
    public static void notEnoughScrap() {
        alert.text = "Not enough scrap!";
        if (mainCam.pixelRect.Contains(Input.mousePosition)) {
            alert.transform.position = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition);
            alert.transform.SetParent(mainCam.transform.GetChild(0));
        } else {
            //Debug.Log("ui");
            alert.transform.position = (Vector2)uiCam.ScreenToWorldPoint(Input.mousePosition);
            alert.transform.SetParent(uiCam.transform.GetChild(0));
        }
        alertTime = 2f;
    }

    public static void waveComplete() {
        waveNum++;
    }
}
