using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileTower : Tower {

    public Missile misslePrefab;

    // Start is called before the first frame update
    new void Start() {
        //Cullen
        base.Start();
        range = 200f;
        damage = 50f;
        scrapCost = 400;
        cooldown = 5f;
    }

    // Written by Addison
    protected override void fire(GameObject nearestEnemy) {
        Missile missle = Instantiate(misslePrefab, transform.position, Quaternion.identity);
        Vector2 dir = nearestEnemy.transform.position - transform.position;
        missle.setTarget(nearestEnemy);
        missle.setDamage(damage);
    }
}
