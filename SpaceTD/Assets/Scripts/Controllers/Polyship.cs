using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polyship : Enemy {

    public Projectile projectile;
    public float cooldown;
    protected float nextFire = 0f;
    public float stopDistance;
    protected float targetRad;

    // Start is called before the first frame update
    new void Start() {
        base.Start();
        targetRad = target.transform.lossyScale.x * target.GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame
    protected override void EUpdate() {
        //base.Update();
        //Cullen
        if (!Core.freeze) {

            if (Vector2.Distance(transform.position, target.transform.position) <=  targetRad + stopDistance) {
                //rb.velocity = Vector2.zero;

                //Cullen
                if (nextFire <= 0f) {
                    Projectile p = Instantiate(projectile, transform.position, Quaternion.identity);
                    Vector2 dir = target.transform.position - transform.position;
                    p.setBitMask(Projectile.PLAYER_ONLY);
                    p.setDirection(dir / dir.magnitude);
                    p.setDamage(damage);
                    p.GetComponent<SpriteRenderer>().color = Color.red;
                    Core.Laser();
                    nextFire = cooldown;
                } else {
                    nextFire -= Time.deltaTime;
                }
            } else {
                d = target.transform.position - transform.position;
                float angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 50f * Time.deltaTime);
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
        }


    }

    //Cullen
    public override void spawn(int count, Vector2 position, Enemy e, float scale) {
        bool left = true;
        Vector2 dir = Core.player.transform.position - (Vector3) position;
        dir.Normalize();
        Enemy enemy = Instantiate(e, position, Quaternion.identity);
        enemy.healthMult = scale;
        enemy.transform.localScale *= Mathf.Min(.99f + scale / 100f, 3f);
        enemy.speed = enemy.speed * (100 / (scale + 99));
        //Transform axis = enemy.transform;
        float deg = Vector2.SignedAngle(Vector2.up, dir);
        deg += 180;
        //Debug.Log(deg);
        //Debug.Log(enemy.transform.eulerAngles.z);
        float cos = Mathf.Cos(deg * Mathf.Deg2Rad);
        float sin = Mathf.Sin(deg * Mathf.Deg2Rad);

        for (int i = 1; i < count; i++) {
            float tx = (left ? -((i + 1) / 2 * 2f) : ((i + 1) / 2 * 2f));
            float ty = (i + 1) / 2 * 3f;
            Vector2 nextPos = new Vector3(cos * tx - sin * ty, sin * tx + cos * ty);
            enemy = Instantiate(e, position + nextPos, Quaternion.identity);
            enemy.healthMult = scale;
            enemy.transform.localScale *= Mathf.Min(.98f + scale / 50f, 3f);
            enemy.speed = enemy.speed * (100 / (scale + 99));
            //enemy.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            //enemy.transform.RotateAround(position, Vector3.forward, theta);
            left = !left;
        }

    }
}
