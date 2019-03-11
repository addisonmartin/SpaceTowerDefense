using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cullen
[System.Serializable]
public class Orbital {

    //Cullen
    public float ratio; // Represents b/a
    public float p; // The a radius of oval
    public float secondsPerRotation;
    public int sections;
    public int MAX_TOWERS;

    //calculated based on secondsPerRotation, omega value
    private float speed;
    private float phase = 0f;

    //Cullen
    public List<Tower> towers = new List<Tower>();
    private List<Vector3> towerPhaseAndRadius = new List<Vector3>();
    
    //Cullen
    public Orbital() {
        phase = 0f;
        speed = 2f * Mathf.PI / secondsPerRotation;

    }

    //Cullen
    public bool addTower(Tower t, int section) {
        if (towers.Count >= MAX_TOWERS) {
            Core.orbitalFull();
            return false;
        }

        float phase = (((float)section) / sections) * Mathf.PI * 2f + (Mathf.PI) / sections - this.phase;
        if (Core.buildMode) {
            towerPhaseAndRadius.Add(new Vector3(phase, p, phase));
            float x = p * Mathf.Cos(phase + this.phase);
            float y = p * ratio * Mathf.Sin(phase + this.phase);
            t.transform.position = t.transform.parent.position + new Vector3(x, y);
        } else {
            towerPhaseAndRadius.Add(new Vector3(phase, 0f, phase));
        }
        towers.Add(t);

        return true;
    }

    //Cullen
    public void shiftTower(int tower, int shift) {
        float newPhase = towerPhaseAndRadius[tower].z + (2f * Mathf.PI * shift) / sections;
        towerPhaseAndRadius[tower] = new Vector3(towerPhaseAndRadius[tower].x, towerPhaseAndRadius[tower].y, newPhase);

    }

    //Cullen
    public void removeTower(Tower tower) {
        removeTowerAt(towers.IndexOf(tower));
    }

    //Cullen
    public void removeTowerAt(int tower) {
        towers.RemoveAt(tower);
        towerPhaseAndRadius.RemoveAt(tower);
    }

    //Cullen
    public void highlightTower(int tower, LineRenderer line) {
        line.positionCount = 60;
        line.loop = true;
        line.alignment = LineAlignment.View;
        line.startColor = Color.yellow;
        line.endColor = Color.yellow;

        //line.transform.position = towers[tower].transform.position;
        line.transform.SetParent(towers[tower].transform, false);

        float angle = 0f;
        float x = 0f, y = 0f;

        for (int i = 0; i < line.positionCount; i++) {
            x = Mathf.Cos(Mathf.Deg2Rad * angle) * towers[tower].range / line.transform.lossyScale.x;
            y = Mathf.Sin(Mathf.Deg2Rad * angle) * towers[tower].range / line.transform.lossyScale.x;

            line.SetPosition(i, new Vector3(x, y, -1f));

            angle += (360f / 60);
        }

        Player.selectedTowerHL.transform.SetParent(towers[tower].transform, false);
        Core.player.selectedTowerHighlight.SetActive(true);

    }

    //Cullen
    public void unhighlightTower(int tower, LineRenderer line) {
        line.transform.SetParent(Core.player.transform, false);
        line.positionCount = 0;
        towers[tower].transform.GetChild(0).transform.SetParent(Core.player.transform, false);
        Core.player.selectedTowerHighlight.SetActive(false);
    }

    //Cullen
    public void UpdateOrbital(Transform parent) {
        speed = 2f * Mathf.PI / secondsPerRotation;
        phase += Time.deltaTime * speed;
        //phase %= Mathf.PI * 2f;

        //Cullen
        for (int i = 0; i < towers.Count; i++) {
            if (towerPhaseAndRadius[i].z != towerPhaseAndRadius[i].x) {
                towerPhaseAndRadius[i] = new Vector3(towerPhaseAndRadius[i].x + (towerPhaseAndRadius[i].z - towerPhaseAndRadius[i].x) * 1f * Time.deltaTime, towerPhaseAndRadius[i].y, towerPhaseAndRadius[i].z);
            }
            if (towerPhaseAndRadius[i].y < p) {
                towerPhaseAndRadius[i] = new Vector3(towerPhaseAndRadius[i].x, towerPhaseAndRadius[i].y + (p - towerPhaseAndRadius[i].y) * 5f * Time.deltaTime, towerPhaseAndRadius[i].z);
            } else if (towerPhaseAndRadius[i].y != p) {
                towerPhaseAndRadius[i] = new Vector3(towerPhaseAndRadius[i].x, p, towerPhaseAndRadius[i].z);
            }
            float x = towerPhaseAndRadius[i].y * Mathf.Cos(phase + towerPhaseAndRadius[i].x);
            float y = towerPhaseAndRadius[i].y * ratio * Mathf.Sin(phase + towerPhaseAndRadius[i].x);
            towers[i].transform.position = parent.position + new Vector3(x, y);
        }
    }

    //Cullen
    public void drawOrbital(LineRenderer orbitLine) {
        if (orbitLine != null) {
            orbitLine.positionCount = 60;
            orbitLine.loop = true;
            orbitLine.alignment = LineAlignment.View;
            speed = 2f * Mathf.PI / secondsPerRotation;
            if (speed > 0) {
                orbitLine.startColor = Color.green;
                orbitLine.endColor = Color.green;
            } else {
                orbitLine.startColor = Color.cyan;
                orbitLine.endColor = Color.cyan;
            }

            float angle = 0f;
            float x = 0f, y = 0f;

            for (int i = 0; i < orbitLine.positionCount; i++) {
                x = Mathf.Cos(Mathf.Deg2Rad * angle) * p / orbitLine.transform.lossyScale.x;
                y = Mathf.Sin(Mathf.Deg2Rad * angle) * p / orbitLine.transform.lossyScale.x * ratio;

                orbitLine.SetPosition(i, new Vector3(x, y, -1f));

                angle += (360f / 60);
            }
        }
    }

    // Originally written by Cullen
    // Updated by Addison to only draw a porition of the orbital.
    public void drawSelectedOrbital(LineRenderer orbitLine, float startPhase, float endPhase) {
        if (orbitLine != null) {
            orbitLine.positionCount = 20;
            orbitLine.loop = false;
            orbitLine.startWidth = .5f;
            orbitLine.endWidth = .5f;
            orbitLine.widthMultiplier = .5f;
            orbitLine.alignment = LineAlignment.View;

            float angle = startPhase;
            float x = 0f, y = 0f;

            for (int i = 0; i < orbitLine.positionCount; i++) {

                x = Mathf.Cos(angle) * p / orbitLine.transform.lossyScale.x;
                y = Mathf.Sin(angle) * p / orbitLine.transform.lossyScale.x * ratio;

                orbitLine.SetPosition(i, new Vector3(x, y, -1f));

                angle += ((endPhase - startPhase) / orbitLine.positionCount);

                if (angle > endPhase) {
                    break;
                }
            }
        }
    }


}
