using System.Collections;
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

    ////Cullen
    //void drawOrbital() {
    //    if (orbitLine != null) {
    //        orbitLine.positionCount = 60;
    //        orbitLine.loop = true;
    //        orbitLine.startWidth = .15f / transform.localScale.x;
    //        orbitLine.endWidth = .15f / transform.localScale.x;
    //        orbitLine.widthMultiplier = .5f;
    //        orbitLine.alignment = LineAlignment.TransformZ;
    //        float angle = 0f;
    //        float x, y;
    //        for (int i = 0; i < 60; i++) {
    //            x = Mathf.Sin(Mathf.Deg2Rad * angle) * A[orbital];
    //            y = Mathf.Cos(Mathf.Deg2Rad * angle) * B[orbital];

    //            orbitLine.SetPosition(i, transform.parent.position + new Vector3(x, y, -1));

    //            angle += (360f / 60);
    //        }
    //    }
    //}

}
