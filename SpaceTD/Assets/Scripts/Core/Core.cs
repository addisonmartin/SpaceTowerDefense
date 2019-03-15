using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Core : MonoBehaviour {
    public static bool freeze;

    //Cullen
    public static int levelNum;
    public int level;
    public int levelOffset; // (level-levelOffset) = level number. So, level's 3 level = 5, so its level offset would be 2, since 5 - 2 == 3.
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
    public Text gameOverText;
    private static Text gameOver;
    public Image scrapIcon;
    public static Image scrapIco;
    public static float scrapIcoSizeTarget;
    private static bool isGameOver = false;
    public static bool isHaungsMode = false;

    // Written by Addison
    public static int endlessModeHighScore = 0;

    public static bool levelOneCompleted = false;
    public static bool levelOneUnlocked = true;
    public static int levelOneEndlessModeHighScore = 0;
    public static bool levelTwoCompleted = false;
    public static bool levelTwoUnlocked = false;
    public static int levelTwoEndlessModeHighScore = 0;
    public static bool levelThreeCompleted = false;
    public static bool levelThreeUnlocked = false;
    public static int levelThreeEndlessModeHighScore = 0;

    private static AudioSource aud;
    public AudioClip[] theClips;
    private static AudioClip[] clips;

    private static System.Random rnd;

    // Written by Addison
    public static bool endlessMode = false;
    public bool endlessModeNS;
    public static int totalScrapCollected = 0;

    // Awake is called first
    void Awake() {
        freeze = true;
        isGameOver = false;

        //Debug.Log(AudioListener.volume);

        //Cullen
        if (buildText != null) {
            buildMode = true;
            buildText.text = "BUILD INITIAL DEFENSES\nPRESS SPACE TO START";
        }
        buildTime = float.PositiveInfinity;

        levelNum = level;
        alert = alertText;
        alert.text = "";

        //endlessMode = endlessModeNS;
        totalScrapCollected = 0;

        gameOver = gameOverText;
        gameOver.text = "";

        scrapIco = scrapIcon;
        scrapIcoSizeTarget = scrapIco.transform.localScale.x;

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
        if (isGameOver && !freeze) {
            freeze = true;
            if (buildText != null) {
                buildText.text = "";
            }
            return;
        } else if (isGameOver) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.P)) {
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
        if (scrapIco.transform.localScale.x > scrapIcoSizeTarget) {
            float diff = scrapIco.transform.localScale.x - scrapIcoSizeTarget;
            scrapIco.transform.localScale -= new Vector3(diff * 10f * Time.deltaTime, diff * 10f * Time.deltaTime);
            //scrapIco.transform.localScale *= .95f / (1-Time.deltaTime);
        }

        // Written by Addison
        if (buildText != null && endlessMode == true && waveNum >= waveSpawner.waves.Length && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
            waveNum = 0;

            if (endlessModeHighScore < totalScrapCollected) {
               endlessModeHighScore = totalScrapCollected;
            }

            waveSpawner.loopWaves();
        }
        //Cullen
        if (buildText != null && waveNum >= waveSpawner.waves.Length && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
            buildText.text = "LEVEL COMPLETE!\nPRESS SPACE TO RETURN TO MENU";

            // Written by Addison
            if (level - levelOffset == 1) {
               levelOneCompleted = true;
               levelTwoUnlocked = true;

               if (endlessMode) {
                  if (totalScrapCollected > levelOneEndlessModeHighScore) {
                     levelOneEndlessModeHighScore = totalScrapCollected;
                  }
               }
            }
            else if (level - levelOffset == 2) {
               levelTwoCompleted = true;
               levelThreeUnlocked = true;

               if (endlessMode) {
                  if (totalScrapCollected > levelTwoEndlessModeHighScore) {
                     levelTwoEndlessModeHighScore = totalScrapCollected;
                  }
               }
            }
            else if (level - levelOffset == 3) {
               levelThreeCompleted = true;
               //levelFourUnlocked = true;

               if (endlessMode) {
                  if (totalScrapCollected > levelThreeEndlessModeHighScore) {
                     levelThreeEndlessModeHighScore = totalScrapCollected;
                  }
               }
            }

            //Cullen
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)) {
                SceneManager.LoadScene(0);
            }
        }
    }

    //Cullen
    public static void Alert(string a) {
        alert.text = a;
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
    public static void bounceScrap() {
        scrapIco.transform.localScale *= 1.5f;
        scrapIco.transform.localScale.Set(scrapIco.transform.localScale.x, scrapIco.transform.localScale.y, 1f);

        // Written by Addison
        if (scrapIco.transform.localScale.x >= 3.0f || scrapIco.transform.localScale.y >= 3.0f) {
            scrapIco.transform.localScale = new Vector3(3.0f, 3.0f, 0.0f);
        }
    }

    //Cullen
    public static bool inWorld(Vector3 position) {
        return mainCam.pixelRect.Contains(mainCam.WorldToScreenPoint(position));
    }

    //Cullen
    public static void buildPhase(float buildTime) {
        Core.buildTime = buildTime;
    }

    //Cullen
    public static IEnumerator gameOverWait() {

        isGameOver = true;

        if (endlessMode == true) {
            gameOver.text = "Mission failed\nTotal Scrap Collected: " + totalScrapCollected + "\nPress Spacebar to try again\nPress escape to return to menu";
        } else {
            gameOver.text = "Mission failed\nPress Spacebar to try again\nPress escape to return to menu";
        }

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
