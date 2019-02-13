using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    //Lukas
    private Rigidbody2D rb;

    //Cullen
    public float speed;
    private static CameraController camControl;
    private Vector2 d;
    private float damage;


    // Start is called before the first frame update
    void Start() {
        if (camControl == null) {
            camControl = GameObject.Find("Camera Rig").GetComponent<CameraController>();
        }
    }

    //Cullen
    public void setDamage(float d) {
        damage = d;
    }

    private void Update() {

        //Cullen
        RaycastHit2D[] r = Physics2D.CircleCastAll(transform.position, .5f, d, speed * Time.deltaTime);
        foreach (RaycastHit2D rh in r) {
            if (rh.collider.gameObject.CompareTag("Enemy")) {
                rh.collider.gameObject.GetComponent<Enemy>().takeDamage(damage);
                Destroy(gameObject);
                break;
            }
        }

        //Cullen
        transform.position = new Vector3(transform.position.x + d.x * speed * Time.deltaTime, transform.position.y + d.y * speed * Time.deltaTime, transform.position.z);
        //Destroy projectile if it leaves the screen
        if (!camControl.inWorld(transform.position)) {
            Destroy(gameObject);
        }
    }

    //Cullen
    public void setDirection(Vector2 dir) {
        this.d = dir;
        float zRot = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90f);
        //rb.velocity = d * speed;
    }

}
