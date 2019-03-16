using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cullen
public class EMPTower : Tower {
    // Start is called before the first frame update
    //void Start() {

    //}

    //Cullen
    private float disableTime = 3f;
    public EmpGrenade empGren;
    public float fieldRadius = 2f;

    // Update is called once per frame
    //void Update() {

    //}

    //Cullen
    public override string getDescription() {
        return "Lobs a large slow ball of raw electricity, temporarily jamming the circuits of any enemy it passes through";
    }

    //Cullen
    public override int upgrade(int scrap) {
        if (scrap >= (stage + 1) * scrapCost / 4 && stage < maxStage) {
            range += 5;
            //damage += 10;
            cooldown -= .25f;
            disableTime += .5f;
            fieldRadius += .5f;
            stage++;
            return (stage) * scrapCost / 4;
        }
        return 0;
    }

    protected override void fire(GameObject nearestEnemy) {
        //Cullen
        EmpGrenade gren = Instantiate(empGren, transform.position, Quaternion.identity);
        Vector2 dir = nearestEnemy.transform.position - transform.position;
        //gren.setBitMask(Projectile.ENEMY_ONLY);
        gren.setDirection(dir / dir.magnitude);
        gren.setDuration(disableTime);
        gren.setRadius(fieldRadius);
        PlayAudio();

    }

    //Cullen
    protected override GameObject findClosestEnemy() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            if (Core.inWorld(go.transform.position) && go.GetComponent<Enemy>().canEMP()) {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                //ensure target is not obstructed, bitmask indicates to check in all layers except enemy, background, and ignore raycast layer for a collision
                Collider2D interference = Physics2D.Raycast(position, diff, diff.magnitude, ~((3 << 8) + (1 << 2))).collider;
                if (curDistance < distance && (interference == null)) {
                    closest = go;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }

    public override string stats() {
        return "Range: " + range + ", Stun: " + disableTime + "s\nCooldown: " + cooldown + "s, Radius: " + fieldRadius;
    }

    public override string nextStats() {
        if (stage >= maxStage) {
            return stats();
        }
        return "Range: " + (range + 5) + ", Stun: " + (disableTime + .5f) + "s\nCooldown: " + (cooldown - .5f) + "s, Radius: " + (fieldRadius + .5f);
    }

}
