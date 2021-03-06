﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileTower : Tower {

    public Missile misslePrefab;
    private float radius = 7f;

    // Start is called before the first frame update
    new void Start() {
        //Cullen
        base.Start();
    }

    // Written by Addison
    protected override void fire(GameObject nearestEnemy) {
        Missile missle = Instantiate(misslePrefab, transform.position, Quaternion.identity);
        Vector2 dir = nearestEnemy.transform.position - transform.position;
        missle.setExplosionRadius(radius);
        missle.setTarget(nearestEnemy);
        missle.setDamage(damage);
        Core.deepBoom();
    }

    //Cullen
    public override int upgrade(int scrap) {
        if (scrap >= (stage + 1) * scrapCost / 4 && stage < maxStage) {
            radius += 2;
            damage += 20;
            cooldown -= .35f;
            stage++;
            return (stage) * scrapCost / 4;
        }
        return 0;
    }

    public override string getDescription() {
        return "Fires a homing missile with a large explosion radius\n";
    }

    public override string stats() {
        return "Range: " + range + ", Damage: " + damage + "\nCooldown: " + cooldown + "s, Blast Radius: " + radius;
    }

    public override string nextStats() {
        if (stage >= maxStage) {
            return stats();
        }
        return "Range: " + range + ", Damage: " + (damage + 20) + "\nCooldown: " + (cooldown - .35f) + "s, Blast Radius: " + (radius + 2);
    }
}
