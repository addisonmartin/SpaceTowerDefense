using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
    public static bool freeze;
    public int levelNum;
    public static int waveNum;
    // Start is called before the first frame update
    void Start() {
        freeze = false;
        waveNum = 0;
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
}
