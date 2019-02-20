﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {
    //Cullen
    public float secondsPerRotation;
    private float speed;
    public float a, b;
    public float phase;

    //Cullen
    void Start() {
        speed = 2f * Mathf.PI / secondsPerRotation;
    }

    // Update is called once per frame
    void Update() {
        //Cullen
        phase += Time.deltaTime * speed;
        float x = a * Mathf.Cos(phase);
        float y = b * Mathf.Sin(phase);
        transform.position = new Vector2(x, y);
        phase %= Mathf.PI * 2f;
    }

}
