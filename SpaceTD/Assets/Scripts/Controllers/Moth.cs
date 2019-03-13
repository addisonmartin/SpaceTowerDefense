using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cullen
public class Moth : Polyship {

    //Cullen
    private float spiralRate;
    private float radius;
    private float phase;

    //Cullen
    new void Start() {
        base.Start();

        //calculate phase
        radius = Vector2.Distance(transform.position, target.transform.position);
        phase = Vector2.SignedAngle(Vector2.right, transform.position) * Mathf.Deg2Rad;
        spiralRate = 10f * Mathf.Abs(speed);
        //Debug.Log(phase * Mathf.Rad2Deg);
        //phase = Mathf.Acos(transform.position.x / radius) + Mathf.PI/2;

    }

    //Cullen
    protected override void EUpdate() {
        if (!Core.freeze) {

            phase += Time.deltaTime * speed;
            Vector2 dir = target.transform.position - transform.position;
            float x = radius * Mathf.Cos(phase);
            float y = radius * Mathf.Sin(phase);
            transform.position = new Vector3(x, y);
            transform.rotation *= Quaternion.FromToRotation(speed > 0 ? -transform.right : transform.right, dir);
            phase %= Mathf.PI * 2f;


            if (radius <= target.transform.lossyScale.x * target.GetComponent<CircleCollider2D>().radius + stopDistance) {
                //Cullen
                if (nextFire <= 0f) {
                    Projectile p = Instantiate(projectile, transform.position, Quaternion.identity);
                    
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
                radius -= Time.deltaTime * spiralRate;
            }

        }
    }

    public override void spawn(int count, Vector2 position, Enemy e, float scale) {
        float separation = 360f / count;
        float angle = Vector2.SignedAngle(Vector2.right, position);
        float radius = Vector2.Distance(position, Core.player.transform.position);
        for (int i = 0; i < count; i++) {
            Enemy enemy = Instantiate(e, new Vector2(radius * Mathf.Cos(angle * Mathf.Deg2Rad), radius * Mathf.Sin(angle * Mathf.Deg2Rad)), Quaternion.identity);
            if (scale < 0) {
                enemy.speed = -enemy.speed;
            }
            enemy.healthMult = Mathf.Abs(scale);
            enemy.transform.localScale *= Mathf.Min(.98f + Mathf.Abs(scale) / 50f, 3f);
            enemy.speed = enemy.speed * (50 / (Mathf.Abs(scale) + 49));
            angle -= separation;
        }
        
    }

}
