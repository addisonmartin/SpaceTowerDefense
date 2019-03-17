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

    protected float empTime = 0f;
    protected float tilNextEmp;
    protected float empSpinRate;
    public bool isEmpAble;

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

    public void Update() {
        if (empTime > 0f && !Core.freeze) {
            empTime -= Time.deltaTime;
            transform.Rotate(Vector3.forward * empSpinRate * 360f * Time.deltaTime);
        } else {
            if (!Core.freeze) {
                tilNextEmp -= Time.deltaTime;
            }
            EUpdate();
        }
    }

    protected abstract void EUpdate();

    public void emp(float duration) {
        if (isEmpAble && tilNextEmp <= 0f) {
            empSpinRate = 3f / duration;
            empTime = duration;
            tilNextEmp = 5f;
        }
    }

    public bool canEMP() {
        return isEmpAble && tilNextEmp <= 0f;
    }

    //Cullen
    public abstract void spawn(int count, Vector2 position, Enemy e, float scale);

    //Cullen
    public virtual void takeDamage(float damage, Tower.DAMAGE damageType) {
        hp -= damage * (1 / healthMult);
        if (hp <= 0) {
            Explode();
        }
        if (hb == null) {
            hb = GetComponent<Healthbar>();
        }
        hb.setHealth(hp);
    }

    //Lukas
    protected virtual void Explode() {
        EmitScrap();
        Core.EnemyDeath();
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
