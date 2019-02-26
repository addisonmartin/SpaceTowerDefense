using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cullen
[System.Serializable]
public class Orbital {

    //represents b/a
    public float ratio;
    public float p;
    public float secondsPerRotation;
    public int sections;
    public int MAX_TOWERS;
    private float speed;
    private float phase = 0f;
    private List<Tower> towers = new List<Tower>();
    private List<Vector2> towerPhaseAndRadius = new List<Vector2>();

    public Orbital() {
        //secondsPerRotation = 5;
        phase = 0f;
        speed = 2f * Mathf.PI / secondsPerRotation;

    }

    public bool addTower(Tower t, int section) {
        if (towers.Count >= MAX_TOWERS) {
            return false;
        }

        towers.Add(t);
        towerPhaseAndRadius.Add(new Vector2((((float) section) / sections) * Mathf.PI * 2f, 0f));

        return true;
    }

    public void UpdateOrbital(Transform parent) {
        speed = 2f * Mathf.PI / secondsPerRotation;
        phase += Time.deltaTime * speed;
        //phase %= Mathf.PI * 2f;

        //Cullen
        for (int i = 0; i < towers.Count; i++) {
            if (towerPhaseAndRadius[i].y < p) {
                float ph = towerPhaseAndRadius[i].x + phase;
                //approach limit (p + .05f)(ph)^2 / (ph (ph + 8))
                towerPhaseAndRadius[i] = new Vector2(towerPhaseAndRadius[i].x, towerPhaseAndRadius[i].y + (p - towerPhaseAndRadius[i].y) * 1f * Time.deltaTime);
            } else if (towerPhaseAndRadius[i].y != p) {
                towerPhaseAndRadius[i] = new Vector2(towerPhaseAndRadius[i].x, p);
            }
            float x = towerPhaseAndRadius[i].y * Mathf.Cos(phase + towerPhaseAndRadius[i].x);
            float y = towerPhaseAndRadius[i].y * ratio * Mathf.Sin(phase + towerPhaseAndRadius[i].x);
            towers[i].transform.position = parent.position + new Vector3(x, y);
        }
    }

    //Cullen
    public void drawOrbital(LineRenderer orbitLine) {
        if (orbitLine != null) {
            //int prevPositions = orbitLine.positionCount;
            orbitLine.positionCount = 60;
            orbitLine.loop = true;
            //orbitLine.startWidth = .25f ;
            //orbitLine.endWidth = .25f;
            //orbitLine.widthMultiplier = .5f;
            orbitLine.alignment = LineAlignment.View;
            float angle = 0f;
            float x = 0f, y = 0f;

            //orbitLine.SetPosition(prevPositions, parent.position + new Vector3(0f, p * ratio, 10f));

            for (int i = 0; i < orbitLine.positionCount; i++) {
                x = Mathf.Cos(Mathf.Deg2Rad * angle) * p / orbitLine.transform.lossyScale.x;
                y = Mathf.Sin(Mathf.Deg2Rad * angle) * p / orbitLine.transform.lossyScale.x * ratio;

                orbitLine.SetPosition(i, new Vector3(x, y, -1f));

                angle += (360f / 60);
            }
            //orbitLine.SetPosition(orbitLine.positionCount - 2, parent.position + new Vector3(0f, p * ratio, -1f));
            //orbitLine.SetPosition(orbitLine.positionCount - 1, parent.position + new Vector3(0f, p * ratio, 10f));

        }
    }

}
