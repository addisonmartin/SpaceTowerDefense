using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LazerTower : Tower {

    //Cullen
    private LineRenderer lazer;
    private float fallOff = .9f;
    private float lazerDuration = .1f;
    private float lazerTime;
    private Vector3[] NO_LAZER = { Vector3.zero, Vector3.forward };

    // Start is called before the first frame update
    new void Start() {
        //Cullen
        base.Start();
       // button = GameObject.Find("Tower2").GetComponent<Button>();
        range = 40f;
        damage = 100f;
        scrapCost = 300;
        cooldown = 3f;

        lazer = GetComponent<LineRenderer>();

        lazer.startWidth = .5f;
        lazer.endWidth = .3f;
        lazer.widthMultiplier = .15f;

        lazer.positionCount = 2;
        lazer.SetPositions(NO_LAZER);
    }

    private void FixedUpdate() {
        if (!Core.freeze)
        {
            lazerTime -= Time.deltaTime;
            lazer.material.color = new Color(1f, 0f, 0f, lazerTime / lazerDuration);
            //lazer.startColor = new Color(1f, 0f, 0f, lazerTime/lazerDuration);
            //lazer.endColor = new Color(1f, 0f, 0f, .8f * lazerTime / lazerDuration);

            if (lazerTime <= 0)
            {
                lazer.SetPositions(NO_LAZER);
            }
        }
    }


    protected override void fire(GameObject nearestEnemy) {
        //Cullen
        //Projectile p = Instantiate(projectile, transform.position, Quaternion.identity);

        Vector3 dir = nearestEnemy.transform.position - transform.position;
        float nextDamage = damage;
        dir.z = 0;
        dir.Normalize();
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, range);
        foreach (RaycastHit2D r in hits) {
            if (r.collider != null && r.collider.CompareTag("Enemy")) {
                r.collider.gameObject.GetComponent<Enemy>().takeDamage(nextDamage);
                nextDamage *= fallOff;
            }
        }
        lazer.SetPosition(0, transform.position + dir/3f);
        lazer.SetPosition(1, transform.position + dir * range);
        lazerTime = lazerDuration;
        lazer.material.color = new Color(1f, 0f, 0f, 1f);
        //lazer.startColor 
        //lazer.endColor = new Color(1f, 0f, 0f, .8f);
        
    }
}
