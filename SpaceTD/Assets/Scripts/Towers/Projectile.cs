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
    private int bitMask;

    public static readonly int ENEMY_ONLY = (1 << 9);
    public static readonly int PLAYER_ONLY = (1 << 10);


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

    public void setBitMask(int mask) {
        bitMask = mask;
    }

    private void Update() {

        if (Core.freeze) {
            return;
        }

        //Cullen
        RaycastHit2D[] r = Physics2D.CircleCastAll(transform.position, .5f, d, speed * Time.deltaTime, bitMask);
        foreach (RaycastHit2D rh in r) {
            if (bitMask == ENEMY_ONLY) {
                rh.collider.gameObject.GetComponent<Enemy>().takeDamage(damage);
            } else if (bitMask == PLAYER_ONLY) {
                rh.collider.gameObject.GetComponent<Player>().takeDamage(damage);
            }
            Destroy(gameObject);
            break;
        }

        //Cullen
        transform.position = new Vector3(transform.position.x + d.x * speed * Time.deltaTime, transform.position.y + d.y * speed * Time.deltaTime, transform.position.z);
        //Destroy projectile if it leaves the screen
        if (!Core.inWorld(transform.position)) {
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
