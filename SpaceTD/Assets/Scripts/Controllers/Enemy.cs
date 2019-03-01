using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //Cullen
    public float speed;
    private GameObject target;
    private Vector2 d;
    private Rigidbody2D rb;
    private int scrapValue = 6;
    private float hp = 100f;
    private Vector2 dir;

    private int scrapToEmit = 2;
    private float damage = 30;
    public Projectile projectile;
    public float cooldown;
    private float nextFire = 0f;

    public GameObject scrapPrefab;

    //public GameObject EnemyDeath;

    // Start is called before the first frame update
    void Start() {
        //Cullen
        target = GameObject.FindGameObjectWithTag("Player");
        d = target.transform.position - transform.position;
        d /= d.magnitude;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = d * speed;
        float zRot = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90);
    }

        
    void Update() {
        //Daniel
        if (!Core.freeze) {
            dir = new Vector2(-transform.right.y, transform.right.x);
            rb.velocity = dir * speed;

            d = target.transform.position - transform.position;
            float angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 1);
            if (Vector2.Distance(transform.position, target.transform.position) <= target.transform.lossyScale.x * target.GetComponent<CircleCollider2D>().radius + 5) {
                rb.velocity = Vector2.zero;

                //Cullen
                if (nextFire <= 0f) {
                    Projectile p = Instantiate(projectile, transform.position, Quaternion.identity);
                    Vector2 dir = target.transform.position - transform.position;
                    p.setBitMask(Projectile.PLAYER_ONLY);
                    p.setDirection(dir / dir.magnitude);
                    p.setDamage(damage);
                    p.GetComponent<SpriteRenderer>().color = Color.red;
                    nextFire = cooldown;
                } else {
                    nextFire -= Time.deltaTime;
                }
            }
        } else {
            rb.velocity = Vector2.zero;
        }
    }

    public void takeDamage(float damage) {
        hp -= damage;
        if (hp <= 0) {
            Explode();
        }
        GetComponent<Healthbar>().setHealth(hp);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        //Cullen
        if (collision.gameObject.CompareTag("Player")) {

            target.GetComponent<Player>().takeDamage(damage);
            Destroy(gameObject);
        }/*else if (collision.gameObject.CompareTag("Projectile")) {
            Destroy(collision.gameObject);
            Explode();
            //Destroy(gameObject);
        }*/
    }

    //Lukas
    void Explode() {
        /*Vector3 position = enemy.transform.position;
        GameObject scrap = Instantiate(EnemyDeath, position, Quaternion.identity);
        scrap.GetComponent<ParticleSystem>().Play();*/

        EmitScrap();
        Destroy(gameObject);
    }

    //Lukas
    public void EmitScrap() {
        for (int i = 0; i < scrapToEmit; i++) {
            Quaternion randRotation = Quaternion.Euler(0, 0, Random.Range(-360.0f, 360.0f));
            GameObject scr = Instantiate(scrapPrefab, transform.position, randRotation);
            scr.GetComponent<ScrapController>().setValue(scrapValue);
        }
    }

    public Vector2 getVel() {
        return rb.velocity;
    }

}

