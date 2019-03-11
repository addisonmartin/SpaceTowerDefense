﻿using System;
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
    private static float buildTime = 0f;
    private static AudioSource aud;
    public AudioClip[] theClips;
    private static AudioClip[] clips;

    public Text gameOverText;
    private static Text gameOver;
    private static System.Random rnd;


    // Start is called before the first frame update
    void Start() {
        freeze = true;

        //Cullen
        if (buildText != null) {
            buildMode = true;
            buildText.text = "BUILD INITIAL DEFENSES\nPRESS SPACE TO START";
        }
        buildTime = float.PositiveInfinity;

        levelNum = level;
        alert = alertText;
        alert.text = "";

        gameOver = gameOverText;
        gameOver.text = "";

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        uiCam = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        waveSpawner = GetComponent<WaveSpawner>();
        waveNum = 0;

        //Daniel
        clips = theClips;
        aud = GetComponent<AudioSource>();
        aud.loop = true;
        aud.Play();
        rnd = new System.Random();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            freeze = !freeze;
        }

        //Cullen
        if (alertTime > 0f) {
            alertTime -= Time.deltaTime;
            alert.color = new Color(alert.color.r, alert.color.g, alert.color.b, alertTime / 2f);
        } else {
            alert.text = "";
            //alert.transform.position = Vector2.zero;
        }

        //Cullen
        if (buildTime > 0f && !float.IsInfinity(buildTime) && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
            buildText.text = "BUILD: " + buildTime.ToString("F2") + "s\nPRESS SPACE TO SKIP";
            buildMode = true;
            freeze = true;
            buildTime -= Time.deltaTime;
        }
        if (buildMode && (buildTime <= 0f || Input.GetKeyDown(KeyCode.Space))) {
            buildMode = false;
            freeze = false;
            waveSpawner.enabled = true;
            buildText.text = "";
            buildTime = 0f;
        }

        //Cullen
        if (buildText != null && waveNum >= waveSpawner.waves.Length && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
            buildText.text = "LEVEL COMPLETE!\nPRESS SPACE TO RETURN TO MENU";
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)) {
                SceneManager.LoadScene(0);
            }
        }
    }

    //Cullen
    public static void orbitalFull() {
        alert.text = "This orbital is full!";
        alert.transform.SetParent(mainCam.transform.GetChild(0));
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

    //Cullen
    public static bool inWorld(Vector3 position) {
        return mainCam.pixelRect.Contains(mainCam.WorldToScreenPoint(position));
    }

    public static void buildPhase(float buildTime) {
        Core.buildTime = buildTime;
    }

    public static IEnumerator gameOverWait() {
        gameOver.text = "Mission failed\nPress Spacebar to try again\nPress escape to return to menu";
        while (!Input.GetKeyDown(KeyCode.Space) && !Input.GetKeyDown(KeyCode.Escape)) {
            yield return null;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
        } else {
            SceneManager.LoadScene(Core.levelNum);
        }
    }


    //Daniel
    public static void Boom() {
        playSound(rnd.Next(0, 5));
    }

    public static void deepBoom() {
        playSound(rnd.Next(5, 8));
    }

    public static void EnemyDeath() {
        playSound(rnd.Next(17, 20));
    }

    public static void playSound(int i) {
        aud.PlayOneShot(clips[i]);
    }

    public static void Laser() {
        playSound(rnd.Next(12, 17));
    }
}
