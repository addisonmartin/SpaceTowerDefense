using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileTower : Tower {

    //Cullen
    public Projectile projectile;

    // Start is called before the first frame update
    //new void Start() {
    //    //Cullen
    //    base.Start();
    //}

    protected override void fire(GameObject nearestEnemy) {
        //Cullen
        Projectile p = Instantiate(projectile, transform.position, Quaternion.identity);
        Vector2 dir = nearestEnemy.transform.position - transform.position;
        p.setBitMask(Projectile.ENEMY_ONLY);
        p.setDirection(dir / dir.magnitude);
        p.setDamage(damage);
        PlayAudio();
    }

    //Cullen
    public override int upgrade(int scrap) {
        if (scrap >= (stage + 1) * scrapCost/4 && stage < maxStage) {
            //range += 5;
            damage += 10;
            cooldown -= .1f;
            stage++;
            return (stage) * scrapCost / 4;
        }
        return 0;
    }

    public override string getDescription() {
        return "A basic tower that sends mass-based projectiles at enemies at high speed.\n";
    }

    public override string nextStats() {
        if (stage >= maxStage) {
            return stats();
        }
        return "Range: " + range + ", Damage: " + (damage + 10) + "\nCooldown: " + (cooldown - .1f) + "s";
    }
}
