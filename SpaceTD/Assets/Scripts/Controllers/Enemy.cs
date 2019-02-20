using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //Cullen
    public float speed;
    private GameObject target;
    private Vector2 d;
    private Rigidbody2D rb;
    private int scrapValue = 3;
    private float hp = 100f;
    private Vector2 dir;

    private int scrapToEmit = 4;
    private float damage = 30;
    public GameObject scrapPrefab;

    //public GameObject EnemyDeath;

    // Start is called before the first frame update
    void Start()
    {
        //Cullen
        target = GameObject.FindGameObjectWithTag("Player");
        d = target.transform.position - transform.position;
        d /= d.magnitude;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = d * speed;
        float zRot = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90);
    }

    void Update()
    {
        //Daniel
        dir = new Vector2(-transform.right.y, transform.right.x);
        rb.velocity = dir * speed;
        /*bool boop = CheckPath();
        if (boop)
        {
            if (goRight())
            {
                transform.Rotate(new Vector3(0, 0, -1));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 1));
            }
        }
        else
        {*/
        d = target.transform.position - transform.position;
        float angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 1);
        //}
        if (Vector2.Distance(transform.position, target.transform.position) <= 5 * target.transform.lossyScale.x)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    public void takeDamage(float damage) {
        hp -= damage;
        if (hp <= 0) {
            Explode();
        }
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

