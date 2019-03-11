using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polyship : Enemy {

    public Projectile projectile;
    public float cooldown;
    private float nextFire = 0f;
    public float stopDistance = 5f;

    // Start is called before the first frame update
    new void Start() {
        base.Start();
    }

    // Update is called once per frame
    protected override void EUpdate() {
        //base.Update();
        //Cullen
        if (!Core.freeze) {

            if (Vector2.Distance(transform.position, target.transform.position) <= target.transform.lossyScale.x * target.GetComponent<CircleCollider2D>().radius + stopDistance) {
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
        Enemy enemy = Instantiate<Enemy>(e, position, Quaternion.identity);
        enemy.healthMult = scale;
        enemy.transform.localScale *= Mathf.Min(1 + scale / 100f, 3f);
        //float theta = enemy.transform.eulerAngles.z;
        for (int i = 1; i < count; i++) {
            Vector3 nextPos = new Vector3(position.x + - dir.x * (left ? -(i * .5f) : (i * .5f)), position.y - dir.y * (i + 1) / 2 * .5f, 0f);
            enemy = Instantiate(e, nextPos, Quaternion.identity);
            enemy.healthMult = scale;
            enemy.transform.localScale *= Mathf.Min(1 + scale / 100f, 3f);
            //enemy.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            //enemy.transform.RotateAround(position, Vector3.forward, theta);
            left = !left;
        }

    }
}
