using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{

    //Cullen
    public Projectile projectile;
    public float cooldown;
    public float range;
    public Tower tower;
    private static Player player;
    private float timeToNextFire;
    private float damage = 50f;
    private static int scrapCost = 150;


    //public static Button button;

    // Start is called before the first frame update
    void Start()
    {
        timeToNextFire = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //button = GameObject.Find("Tower1").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update() {

        transform.localScale = transform.localScale;

        //Cullen
        if (timeToNextFire <= 0) {
            GameObject nearestEnemy = findClosestEnemy();
            if (nearestEnemy != null && (nearestEnemy.transform.position - transform.position).magnitude <= range) {
                Projectile p = Instantiate(projectile, transform.position, Quaternion.identity);
                Vector2 dir = nearestEnemy.transform.position - transform.position;
                p.setDirection(dir / dir.magnitude);
                p.setDamage(damage);
            }
            timeToNextFire = cooldown;
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
        if(Selectable.selected == null || player.getScrap() < scrapCost) {
            return;
        }

        AstralBody ab = Selectable.selected.gameObject.GetComponent<AstralBody>();
        if (ab != null) {
            GameObject parent = Selectable.selected.gameObject;
            float parentScale = parent.transform.localScale.x;
            float parentColliderRadius = parent.GetComponent<CircleCollider2D>().radius;
            //This is used to give buffer to orbital placement INCOMPLETE
            float scalingBufferAreaLow = parentScale * parentColliderRadius * 0.2f;
            float scalingBufferAreaHigh = parentScale * parentColliderRadius * 0.4f;

            GameObject t = Instantiate(gameObject, parent.transform.position +
                new Vector3(Random.Range(parentScale * parentColliderRadius + 2.0f, parentScale * parentColliderRadius + 3.0f),
                Random.Range(parentScale * parentColliderRadius + 2.0f, parentScale * parentColliderRadius + 3.0f)), Quaternion.identity);
            t.transform.SetParent(parent.transform, true);
            
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

    //Cullen
    public void setOrbital(int o) {
        GetComponent<Orbit>().setOrbital(o);
    }
}
