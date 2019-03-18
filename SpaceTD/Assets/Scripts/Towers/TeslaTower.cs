using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cullen
public class TeslaTower : Tower {

    //Cullen
    private int maxBolts = 2;
    public LightningBoltScript lightning;
    private CircleCollider2D cc;
    private List<Enemy> targets = new List<Enemy>();
    private List<LightningBoltScript> bolts = new List<LightningBoltScript>();

    //Cullen
    new void Start() {
        base.Start();
        lightning.gameObject.SetActive(false);
        for (int i = 0; i < maxBolts; i++) {
            bolts.Add(Instantiate(lightning));
        }
        cc = GetComponent<CircleCollider2D>();
        cc.radius = range / transform.lossyScale.x;

    }

    //Cullen
    public override string getDescription() {
        return "Electrifies enemies that enter its radius with bolts of manufactured lightning. Does 5x damage to moths. Thanks Tesla!";
    }

    //Cullen
    public override string stats() {
        return "Range: " + range + ", Damage: " + damage + "/s\nMax Bolts: " + maxBolts;
    }

    //Cullen
    public override string nextStats() {
        if (stage >= maxStage) {
            return stats();
        }
        return "Range: " + (range + (((stage + 2) % 5) == 0 ? 5 : 0)) + ", Damage: " + (damage + 15 * ((stage + 3) / 3f)) + "/s\nMax Bolts: " + (maxBolts + 1);
    }

    //Cullen
    protected override void Update() {
        if (Core.freeze) {
            return;
        }
        for (int i = 0; i < maxBolts; i++) {
            if (i < targets.Count) {
                if (targets[i] == null) {
                    bolts[i].gameObject.SetActive(false);
                    targets.RemoveAt(i);
                    continue;
                }
                Vector3 dir = targets[i].transform.position - transform.position;
                dir.Normalize();
                targets[i].takeDamage(damage * Time.deltaTime, DAMAGE.LIGHTNING);
                //check if target destroyed
                if (i >= targets.Count || targets[i] == null) {
                    bolts[i].gameObject.SetActive(false);
                    if (i < targets.Count) {
                        targets.RemoveAt(i);
                    }
                    continue;
                }
                bolts[i].StartObject.transform.position = transform.position + dir / 3f;
                bolts[i].EndObject.transform.position = targets[i].transform.position;
                bolts[i].gameObject.SetActive(true);
                if (!AudioPlaying()) {
                    PlayAudio();
                }
            } else {
                bolts[i].gameObject.SetActive(false);
            }
        }
        if (targets.Count == 0) {
            StopAudio();
        }
    }

    //Cullen
    public override int upgrade(int scrap) {
        if (scrap >= (stage + 1) * scrapCost / 4 && stage < maxStage) {
            range += (stage + 2) % 5 == 0 ? 5 : 0;
            damage += 15 * ((stage + 3) / 3f);
            maxBolts += 1;
            bolts.Add(Instantiate(lightning));
            cc.radius = range / transform.lossyScale.x;
            stage++;
            return (stage) * scrapCost / 4;
        }
        return 0;
    }

    protected override void fire(GameObject nearestEnemy) {
        //do nothing
    }

    //Cullen
    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Enemy")) {
            return;
        }
        //Debug.Log("entered range");
        Vector3 dir = collision.transform.position - transform.position;
        dir.Normalize();
        targets.Add(collision.GetComponent<Enemy>());
    }

    //Cullen
    private void OnTriggerExit2D(Collider2D collision) {
        if (!collision.CompareTag("Enemy")) {
            return;
        }
        int i = targets.IndexOf(collision.GetComponent<Enemy>());
        targets.RemoveAt(i);
    }
}
