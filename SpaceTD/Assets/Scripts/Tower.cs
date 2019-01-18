using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    //Cullen
    public Projectile projectile;
    public float cooldown;
    public float range;
    private float timeToNextFire;

    // Start is called before the first frame update
    void Start()
    {
        timeToNextFire = 0;
    }

    // Update is called once per frame
    void Update() {
        
        //Cullen
        if (timeToNextFire <= 0) { 
            GameObject nearestEnemy = findClosestEnemy();
            if (nearestEnemy != null && (nearestEnemy.transform.position - transform.position).magnitude <= range) {
                Projectile p = Instantiate(projectile, transform.position, Quaternion.identity);
                Vector2 dir = nearestEnemy.transform.position - transform.position;
                p.setDirection(dir / dir.magnitude);
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
}
