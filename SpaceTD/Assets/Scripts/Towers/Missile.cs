using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
    //Lukas
    private Rigidbody2D rb;

    //Cullen
    public float speed;
    public float maxSpeed;
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

        //bool shouldDestroySelf = false;
        //speed += Time.deltaTime * acceleration;

        //Cullen
        if (target != null) {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
            Quaternion temp = transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
            if (Mathf.Abs(temp.eulerAngles.z - transform.rotation.eulerAngles.z) > Time.deltaTime * 50f) {
                speed *= (1f - .1f * Time.deltaTime);
            } else {
                if (speed <= maxSpeed) {
                    speed += acceleration * Time.deltaTime;
                } else speed = maxSpeed;
            }
        } else {
            target = findClosestEnemy();
        }

        transform.Translate(Vector3.up * Time.deltaTime * speed);

        //Cullen
        if (target != null) {
            if ((target.transform.position - transform.position).sqrMagnitude <= 2f) {
                RaycastHit2D[] r = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero, speed * Time.deltaTime);
                foreach (RaycastHit2D rh in r) {
                    if (rh.collider.gameObject.CompareTag("Enemy")) {
                        rh.collider.gameObject.GetComponent<Enemy>().takeDamage(damage);
                        //shouldDestroySelf = true;
                    }
                }
                Destroy(gameObject);
            }
        }

        //if (target == null) {
        //    shouldDestroySelf = true;
        //} else {
        //    //calculate diff between projected target position and current pos
        //    float timeToTarget = (target.transform.position - transform.position).magnitude / 10f;
        //    diff = (Vector3) target.GetComponent<Rigidbody2D>().velocity * timeToTarget + target.transform.position - transform.position;

        //    if (diff.sqrMagnitude <= 3) {
        //        RaycastHit2D[] r = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero, speed * Time.deltaTime);
        //        foreach (RaycastHit2D rh in r) {
        //            if (rh.collider.gameObject.CompareTag("Enemy")) {
        //                rh.collider.gameObject.GetComponent<Enemy>().takeDamage(damage);
        //                shouldDestroySelf = true;
        //            }
        //        }
        //    }
        //}
        //if (shouldDestroySelf) {
        //    Destroy(gameObject);
        //} else {
        //    //Vector3 towards = Vector3.MoveTowards(transform.position, new Vector3(d.x, d.y, transform.position.z), Time.deltaTime * speed);
        //    //Vector2 d = target.transform.position - transform.position;
        //    diff.Normalize();
        //    d += diff * acceleration * Time.deltaTime;
        //    //d *= .999f;
        //    float zRot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90f);

        //    transform.position = new Vector3(transform.position.x + d.x * Time.deltaTime * speed, transform.position.y + d.y * Time.deltaTime * speed);

        //    //Cullen
        //    //Destroy projectile if it leaves the screen
        //    if (!camControl.inWorld(transform.position)) {
        //        Destroy(gameObject);
        //    }
        //}
    }

    //Cullen
    public void setTarget(GameObject target) {
        this.target = target;
        //rb.velocity = d * speed;
    }

    private GameObject findClosestEnemy() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            //ensure target is not obstructed, bitmask indicates to check in all layers except enemy, background, and ignore raycast layer for a collision
            Collider2D interference = Physics2D.Raycast(position, diff, diff.magnitude, ~((3 << 8) + (1 << 2))).collider;
            if (curDistance < distance && (interference == null)) {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
