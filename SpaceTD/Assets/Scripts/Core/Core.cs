using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
    public static bool freeze;
    public int levelNum;
    public static int waveNum;
    public static Camera mainCam;
    public static Player player;
    public static AudioSource aud;
    public AudioClip[] theClips;
    public static AudioClip[] clips;
    // Start is called before the first frame update
    void Start() {
        freeze = false;
        //Cullen
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        waveNum = 0;
        aud = GetComponent<AudioSource>();
        aud.Play();
        clips = theClips;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            freeze = !freeze;
        }
    }

    public static void waveComplete(int j)
    {
        waveNum = j;
    }

    public static void sound(int i)
    {
        aud.PlayOneShot(clips[i]);
    }
}
