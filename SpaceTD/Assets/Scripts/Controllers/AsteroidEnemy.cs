using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEnemy : Enemy { 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Move()
    {
     /*   //Daniel
        if (!Core.freeze)
        {
            dir = new Vector2(-transform.right.y, transform.right.x);
            rb.velocity = dir * speed;

            d = target.transform.position - transform.position;
            float angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 1);
            if (Vector2.Distance(transform.position, target.transform.position) <= target.transform.lossyScale.x * target.GetComponent<CircleCollider2D>().radius + 5)
            {
                rb.velocity = Vector2.zero;

                //Cullen
                if (nextFire <= 0f)
                {
                    Projectile p = Instantiate(projectile, transform.position, Quaternion.identity);
                    Vector2 dir = target.transform.position - transform.position;
                    p.setBitMask(Projectile.PLAYER_ONLY);
                    p.setDirection(dir / dir.magnitude);
                    p.setDamage(damage);
                    p.GetComponent<SpriteRenderer>().color = Color.red;
                    nextFire = cooldown;
                }
                else
                {
                    nextFire -= Time.deltaTime;
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }*/
    }
}
