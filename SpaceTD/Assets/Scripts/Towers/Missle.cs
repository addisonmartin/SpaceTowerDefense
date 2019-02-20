using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour {
    //Lukas
    private Rigidbody2D rb;

    //Cullen
    public float speed;
    public float acceleration = 1f;
    private static CameraController camControl;
    private GameObject target;
    private Vector2 d;
    private float damage;

    // Written by Addison
    public float explosionRadius = 10f;

    // Start is called before the first frame update
    void Start() {
        if (camControl == null) {
            camControl = GameObject.Find("Camera Rig").GetComponent<CameraController>();
        }
        d = new Vector2(Random.Range(-3, 3), Random.Range(-3, 3));
    }

    //Cullen
    public void setDamage(float d) {
        damage = d;
    }

    void Update() {
        
        bool shouldDestroySelf = false;
        //speed += Time.deltaTime * acceleration;
        Vector2 diff = Vector2.zero;

        if (target == null) {
            shouldDestroySelf = true;
        } else if ((diff = target.transform.position - transform.position).sqrMagnitude <= 3) {
            RaycastHit2D[] r = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero, speed * Time.deltaTime);
            foreach (RaycastHit2D rh in r) {
                if (rh.collider.gameObject.CompareTag("Enemy")) {
                    rh.collider.gameObject.GetComponent<Enemy>().takeDamage(damage);
                    shouldDestroySelf = true;
                }
            }
        }

        if (shouldDestroySelf) {
            Destroy(gameObject);
        } else {
            //Vector3 towards = Vector3.MoveTowards(transform.position, new Vector3(d.x, d.y, transform.position.z), Time.deltaTime * speed);
            //Vector2 d = target.transform.position - transform.position;
            diff.Normalize();
            d += diff * acceleration * Time.deltaTime;
            d *= .999f;
            float zRot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90f);

            transform.position = new Vector3(transform.position.x + d.x * Time.deltaTime * speed, transform.position.y + d.y * Time.deltaTime * speed);

            //Cullen
            //Destroy projectile if it leaves the screen
            if (!camControl.inWorld(transform.position)) {
                Destroy(gameObject);
            }
        }
    }

    //Cullen
    public void setTarget(GameObject target) {
        this.target = target;
        //rb.velocity = d * speed;
    }
}
