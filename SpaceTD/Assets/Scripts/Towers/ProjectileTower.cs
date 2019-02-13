using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileTower : Tower {

    //Cullen
    public Projectile projectile;

    // Start is called before the first frame update
    new void Start() {
        //Cullen
        base.Start();
        button = GameObject.Find("Tower1B").GetComponent<Button>();
        range = 25f;
        damage = 50f;
        scrapCost = 150;
        cooldown = 1f;
    }


    protected override void fire(GameObject nearestEnemy) {
        //Cullen
        Projectile p = Instantiate(projectile, transform.position, Quaternion.identity);
        Vector2 dir = nearestEnemy.transform.position - transform.position;
        p.setDirection(dir / dir.magnitude);
        p.setDamage(damage);

    }
}
