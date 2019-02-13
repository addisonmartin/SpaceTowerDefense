using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {
    //Cullen
    public float speed;
    public int orbital;
    private float[] A = { 5, 10, 20, 33, 48 };
    private float[] B = { 5, 8, 15, 25, 33 };
    private float time;
    private float iX, iY, a, b, p;
    private LineRenderer orbitLine;

    //Cullen
    void Start() {
        iX = transform.position.x - transform.parent.position.x;
        iY = transform.position.y - transform.parent.position.y;
        speed = 2 * Mathf.PI / speed;

        //percentage of actual orbital
        p = Mathf.Max(Mathf.Abs(iY / B[orbital]), Mathf.Abs(iX / A[orbital]));

        a = p * A[orbital];
        b = p * B[orbital];
        //phase shift
        time = Mathf.Acos(iX / a) * (iY >= 0 ? 1 : -1);
        if (float.IsNaN(time)) {
            time = Mathf.Asin(iY / b) * (iX >= 0 ? 1 : -1);
        }

        //orbitLine = GetComponent<LineRenderer>();
        drawOrbital();
    }

    // Update is called once per frame
    void Update() {
        //Cullen
        time += speed * Time.deltaTime;
        float x = a * Mathf.Cos(time);
        float y = b * Mathf.Sin(time);
        transform.position = transform.parent.position + new Vector3(x, y);
        drawOrbital();
        //Cullen
        //transform.RotateAround(transform.parent.position, Vector3.forward, speed * Time.deltaTime);
    }

    //Cullen
    void drawOrbital() {
        if (orbitLine != null) {
            orbitLine.positionCount = 60;
            orbitLine.loop = true;
            orbitLine.startWidth = .15f / transform.localScale.x;
            orbitLine.endWidth = .15f / transform.localScale.x;
            //orbitLine.widthMultiplier = .5f;
            orbitLine.alignment = LineAlignment.TransformZ;
            float angle = 0f;
            float x, y;
            for (int i = 0; i < 60; i++) {
                x = Mathf.Sin(Mathf.Deg2Rad * angle) * A[orbital];
                y = Mathf.Cos(Mathf.Deg2Rad * angle) * B[orbital];

                orbitLine.SetPosition(i, transform.parent.position + new Vector3(x, y, -1));

                angle += (360f / 60);
            }
        }
    }

    //Cullen
    public void setOrbital(int o) {
        iX = transform.position.x - transform.parent.position.x;
        iY = transform.position.y - transform.parent.position.y;
        p = Mathf.Max(Mathf.Abs(iY / B[orbital]), Mathf.Abs(iX / A[orbital]));
        //Debug.Log(p);

        transform.position = transform.parent.position + new Vector3(p * A[o], 0);
        speed *= A[o] / A[orbital] * (o % 2 == 1 ? -1 : 1);
        orbital = o;
        a = p * A[orbital];
        b = p * B[orbital];
    }
}
