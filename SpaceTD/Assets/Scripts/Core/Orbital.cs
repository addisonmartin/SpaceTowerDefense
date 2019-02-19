using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        /*Debug.Log("============");
        Debug.Log(ratio);
        Debug.Log(p);
        Debug.Log(secondsPerRotation);
        Debug.Log(sections);
        Debug.Log(MAX_TOWERS);
        Debug.Log(speed);
        Debug.Log(phase);*/
    }

    public bool addTower(Tower t, int section) {
        if (towers.Count >= MAX_TOWERS) {
            return false;
        }

        towers.Add(t);

        if (section == 0)
        {
           towerPhaseAndRadius.Add(new Vector2(0f, 0f));
        }
        else
        {
           towerPhaseAndRadius.Add(new Vector2(Mathf.PI * 2f / section, 0f));
        }

        return true;
    }

    public void UpdateOrbital() {
        speed = 2f * Mathf.PI / secondsPerRotation;
        phase += Time.deltaTime * speed;
        phase %= Mathf.PI * 2f;

        for (int i = 0; i < towers.Count; i++) {
            if (towerPhaseAndRadius[i].y < p) {
                float ph = towerPhaseAndRadius[i].x + phase;
                //approach limit (p + .05f)(ph)^2 / (ph (ph + 8))
                towerPhaseAndRadius[i] = new Vector2(towerPhaseAndRadius[i].x, ((p + .05f) * ph * ph) / (ph * (ph + 8)));
            } else if (towerPhaseAndRadius[i].y != p) {
                towerPhaseAndRadius[i] = new Vector2(towerPhaseAndRadius[i].x, p);
            }
            float x = towerPhaseAndRadius[i].y * Mathf.Cos(phase + towerPhaseAndRadius[i].x);
            float y = towerPhaseAndRadius[i].y * ratio * Mathf.Sin(phase + towerPhaseAndRadius[i].x);
            Debug.Log(towerPhaseAndRadius[i]);
            Debug.Log(y);
            Debug.Log("============");
            towers[i].transform.position = new Vector2(x, y);
        }

        /*Debug.Log("============");
        Debug.Log(ratio);
        Debug.Log(p);
        Debug.Log(secondsPerRotation);
        Debug.Log(sections);
        Debug.Log(MAX_TOWERS);
        Debug.Log(speed);
        Debug.Log(phase);*/

    }

}
