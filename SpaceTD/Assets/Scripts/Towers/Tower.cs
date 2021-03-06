﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tower : MonoBehaviour {

    //Cullen
    public string tName = "";

    //Cullen
    public float cooldown;
    public float range;
    public Tower tower;
    public static Player player;
    private float timeToNextFire;
    public float damage;
    public int scrapCost;
    protected int stage = 0;
    protected int maxStage = 4;
    public enum DAMAGE {
        MASS,
        LIGHTNING,
        FIRE,
        RAY
    }

    protected Button button;
    private AudioSource aud;


    // Start is called before the first frame update
    protected void Start() {
        timeToNextFire = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected virtual void Update() {

        //this piece of code is necessary, weird unity interactions
        transform.localScale = transform.localScale;
        if (!Core.freeze) {
            //Cullen
            if (timeToNextFire <= 0) {
                GameObject nearestEnemy = findClosestEnemy();
                if (nearestEnemy != null && (nearestEnemy.transform.position - transform.position).sqrMagnitude <= range * range) {
                    fire(nearestEnemy);
                    timeToNextFire = cooldown;
                }

            } else {
                timeToNextFire -= Time.deltaTime;
            }
        }

    }

    public void PlayAudio() {
        if (aud != null) {
            aud.Play();
        }
    }

    public void StopAudio() {
        if (aud != null) {
            aud.Stop();
        }
    }

    public bool AudioPlaying() {
        return aud.isPlaying;
    }

    public int getStage() {
        return stage;
    }

    public int getMaxStage() {
        return maxStage;
    }

    public abstract int upgrade(int scrap);

    public virtual string stats() {
        return "Range: " + range + ", Damage: " + damage + "\nCooldown: " + cooldown + "s";
    }

    public abstract string nextStats();

    public abstract string getDescription();

    public int sellValue() {
        return scrapCost / 2 + (stage + 1) * scrapCost / 5;
    }

    //Written by Addison
    public int upgradeCost() {
        return (stage + 1) * scrapCost / 4;
    }

    public string getName() {
        return tName;
    }

    public string getDetails() {
        return "Range: " + range + " Damage: " + damage + "\nCooldown: " + cooldown + "s";
    }

    public string getRange() {
        return "" + range;
    }

    public string getDamage() {
        return "" + damage;
    }

    public string getCooldown() {
        return "" + cooldown;
    }

    //Cullen
    protected virtual GameObject findClosestEnemy() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            if (Core.inWorld(go.transform.position)) {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                //ensure target is not obstructed, bitmask indicates to check in all layers except enemy, background, and ignore raycast layer for a collision
                Collider2D interference = Physics2D.Raycast(position, diff, diff.magnitude, ~((3 << 8) | (1 << 2) | (1 << 11))).collider;
                if (curDistance < distance && (interference == null)) {
                    closest = go;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }

    //Cullen
    protected abstract void fire(GameObject nearestEnemy);

}
