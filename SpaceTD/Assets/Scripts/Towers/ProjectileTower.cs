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
    }


    protected override void fire(GameObject nearestEnemy) {
        //Cullen
        Projectile p = Instantiate(projectile, transform.position, Quaternion.identity);
        Vector2 dir = nearestEnemy.transform.position - transform.position;
        p.setBitMask(Projectile.ENEMY_ONLY);
        p.setDirection(dir / dir.magnitude);
        p.setDamage(damage);

    }
}
