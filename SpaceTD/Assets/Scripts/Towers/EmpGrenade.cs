using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cullen
public class EmpGrenade : MonoBehaviour {

    //Cullen
    public float duration;
    public float speed;
    public float radius = 1f;
    //public float explosionRadius;
    private Vector2 d;

    //// Start is called before the first frame update
    void Start() {
        transform.localScale = new Vector3(radius, radius, 1);
    }

    // Update is called once per frame
    void Update() {
        if (Core.freeze) {
            return;
        }

        //Cullen
        RaycastHit2D[] r = Physics2D.CircleCastAll(transform.position, radius, d, speed * Time.deltaTime, Projectile.ENEMY_ONLY);
        if (r.Length > 0) {
             //r = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero, speed * Time.deltaTime, Projectile.ENEMY_ONLY);
            foreach (RaycastHit2D rh in r) {
                if (rh.collider.gameObject.CompareTag("Enemy")) {
                    rh.collider.gameObject.GetComponent<Enemy>().emp(duration);
                }
            }
            //Destroy(gameObject);
        }

        //Cullen
        transform.position = new Vector3(transform.position.x + d.x * speed * Time.deltaTime, transform.position.y + d.y * speed * Time.deltaTime, transform.position.z);
        //Destroy projectile if it leaves the screen
        if (!Core.inWorld(transform.position)) {
            Destroy(gameObject);
        }
    }

    public void setDuration(float d) {
        duration = d;
    }

    //Cullen
    public void setDirection(Vector2 dir) {
        this.d = dir;
        float zRot = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90f);
        //rb.velocity = d * speed;
    }

    public void setRadius(float radius) {
        //explosionRadius = radius;
        this.radius = radius;
        transform.localScale = new Vector3(radius, radius, 1);
    }
}
