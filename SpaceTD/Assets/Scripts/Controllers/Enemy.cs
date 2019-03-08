using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    //Cullen
    public float speed;
    public int scrapValue = 6;
    public int scrapToEmit = 2;
    public float damage = 30;
    public float healthMult = 1f;
    public GameObject scrapPrefab;

    //Cullen
    protected GameObject target;
    protected Vector2 d;
    protected Rigidbody2D rb;
    protected Healthbar hb;
    protected float hp = 100f;

    protected void Start() {
        //Cullen
        target = Core.player.gameObject;
        d = target.transform.position - transform.position;
        d.Normalize();
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = d * speed;
        float zRot = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        hb = GetComponent<Healthbar>();
        transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90);
    }

    //Cullen
    public abstract void spawn(int count, Vector2 position, GameObject e);

    //Cullen
    public void takeDamage(float damage) {
        hp -= damage * (1/healthMult);
        if (hp <= 0) {
            Explode();
        }
        hb.setHealth(hp);
    }

    //Lukas
    protected void Explode() {
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

    //public Vector2 getVel() {
    //    return rb.velocity;
    //}

}

