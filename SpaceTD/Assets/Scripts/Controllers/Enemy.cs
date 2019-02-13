using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //Cullen
    public float speed;
    private GameObject target;
    private Vector2 d;
    private Rigidbody2D rb;
    private int scrapValue = 12;
    private float hp = 100f;

    private int scrapToEmit = 1;
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
        transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90);
    }

    void Uppdate() {
        //Cullen

        //transform.position = new Vector3(transform.position.x + d.x * speed * Time.deltaTime, transform.position.y + d.y * speed * Time.deltaTime, transform.position.z);
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
            Destroy(gameObject);
        }
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

}

