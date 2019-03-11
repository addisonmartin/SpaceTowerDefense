using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LazerTower : Tower {

    //Cullen
    private LineRenderer lazer;
    private float fallOff = .9f;
    private float lazerDuration = .25f;
    private float lazerTime;

    // Start is called before the first frame update
    new void Start() {
        //Cullen
        base.Start();

        lazer = GetComponent<LineRenderer>();

        lazer.startWidth = .6f;
        lazer.endWidth = .1f;
        lazer.widthMultiplier = .5f;

        lazer.positionCount = 0;
        //lazer.positionCount = 2;
        //lazer.SetPositions(NO_LAZER);
    }

    private void FixedUpdate() {
        if (Core.freeze) {
            return;
        }
        if (lazerTime <= 0) {
            lazer.positionCount = 0;
        } else {
            lazerTime -= Time.deltaTime;
            lazer.startColor = new Color(1f, 0f, 0f, lazerTime / lazerDuration);
            lazer.endColor = new Color(1f, 0f, 0f, .8f * lazerTime / lazerDuration);
        }
    }

    protected override void fire(GameObject nearestEnemy) {
        //Cullen
        lazer.positionCount = 2;
        Vector3 dir = nearestEnemy.transform.position - transform.position;
        float nextDamage = damage;
        dir.z = 0;
        dir.Normalize();
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, range, (1 << 9));
        foreach (RaycastHit2D r in hits) {
            if (r.collider != null /*&& r.collider.CompareTag("Enemy")*/) {
                r.collider.gameObject.GetComponent<Enemy>().takeDamage(nextDamage);
                nextDamage *= fallOff;
            }
        }
        PlayAudio();
        lazer.SetPosition(0, transform.position + dir / 3f);
        lazer.SetPosition(1, transform.position + dir * range);
        lazerTime = lazerDuration;
        lazer.startColor = new Color(1f, 0f, 0f, 1f);
        lazer.endColor = new Color(1f, 0f, 0f, .8f);
    }

    //Cullen
    public override int upgrade(int scrap) {
        if (scrap >= (stage + 1) * scrapCost / 4 && stage < maxStage) {
            range += 5;
            damage += 10;
            cooldown -= .5f;
            stage++;
            return (stage) * scrapCost/4;
        }
        return 0;
    }

    public override string getDescription() {
        return "Fires a long ranged beam of light that pierces through enemies, dealing reduced damage to subsequent enemies\n";
    }

    public override string nextStats() {
        if (stage >= maxStage) {
            return stats();
        }
        return "Range: " + (range + 5) + ", Damage: " + (damage + 10) + "\nCooldown: " + (cooldown - .5f);
    }
}
