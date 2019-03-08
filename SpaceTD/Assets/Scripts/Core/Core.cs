using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Core : MonoBehaviour {
    public static bool freeze;
    public int levelNum;
    public static int waveNum;
    public static Camera mainCam;
    public static Camera uiCam;
    public static Player player;
    public static bool buildMode = true;
    public Text buildText;
    public Text alertText;
    private static Text alert;
    private static float alertTime = 0f;

    // Start is called before the first frame update
    void Start() {
        freeze = true;
        buildMode = true;
        buildText.text = "BUILD INITIAL DEFENSES\nPRESS SPACE TO START";
        alert = alertText;
        alert.text = "";
        //Cullen
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        uiCam = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        waveNum = 0;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            freeze = !freeze;
        }
        if (buildMode && Input.GetKeyDown(KeyCode.Space)) {
            buildMode = false;
            freeze = false;
        }
        if (alertTime > 0f) {
            alertTime -= Time.deltaTime;
            alert.color = new Color(alert.color.r, alert.color.g, alert.color.b, alertTime / 2f);
        } else {
            alert.text = "";
        }
    }

    public static void orbitalFull() {
        alert.text = "This orbital is full!";
        alert.transform.position = (Vector2) mainCam.ScreenToWorldPoint(Input.mousePosition);
        alertTime = 2f;
    }

    public static void notEnoughScrap() {
        alert.text = "Not enough scrap!";
        if (mainCam.pixelRect.Contains(Input.mousePosition)) {
            alert.transform.position = (Vector2) mainCam.ScreenToWorldPoint(Input.mousePosition);
            alert.transform.SetParent(mainCam.transform.GetChild(0));
        } else {
            //Debug.Log("ui");
            alert.transform.position = (Vector2) uiCam.ScreenToWorldPoint(Input.mousePosition);
            alert.transform.SetParent(uiCam.transform.GetChild(0));
        }
        alertTime = 2f;
    }

    public static void waveComplete(int j)
    {
        waveNum = j;
    }
}
