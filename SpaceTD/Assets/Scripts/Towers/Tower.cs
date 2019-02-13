using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tower : MonoBehaviour {

    //Cullen
    protected float cooldown;
    protected float range;
    public Tower tower;
    protected static Player player;
    private float timeToNextFire;
    protected float damage;
    protected int scrapCost;

    protected Button button;

    // Start is called before the first frame update
    protected void Start() {
        timeToNextFire = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {

        transform.localScale = transform.localScale;

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

    //Cullen
    private GameObject findClosestEnemy() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    //Cullen
    public void newTower() {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        //-Selectable.selected.gameObject.transform.position
        if (Selectable.selected == null || player.getScrap() < scrapCost) {
            return;
        }

        AstralBody ab = Selectable.selected.gameObject.GetComponent<AstralBody>();
        if (ab != null) {
            Transform parent = ab.gameObject.transform;
            GameObject t = Instantiate(gameObject, parent.position +
                new Vector3(Random.Range(4, 6),
                Random.Range(4, 6)), Quaternion.identity);
            t.transform.SetParent(parent, true);

            if (ab.addTower(0, t.GetComponent<Tower>())) {
                player.addScrap(-scrapCost);
                if (player.getScrap() < scrapCost) {
                    //button.interactable = false;
                }
            } else if (ab.addTower(1, t.GetComponent<Tower>())) {
                player.addScrap(-scrapCost);
                if (player.getScrap() < scrapCost) {
                    //button.interactable = false;
                }
            } else {
                Destroy(t);
            }

        }

    }

    protected abstract void fire(GameObject nearestEnemy);

    //Cullen
    public void setOrbital(int o) {
        GetComponent<Orbit>().setOrbital(o);
    }
}
