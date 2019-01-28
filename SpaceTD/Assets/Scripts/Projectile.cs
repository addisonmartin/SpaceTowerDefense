using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody2D rb;

    //Cullen
    public float speed;
    private Camera cam;
    private Vector2 d;

    // Start is called before the first frame update
    void Start()
    {
        cam = (Camera) GameObject.FindObjectOfType(typeof(Camera));
    }
    
    //Cullen
    //private void Awake() {
    //    rb = gameObject.GetComponent<Rigidbody2D>();
    //}


 

    private void Update() {

        //Cullen
        RaycastHit2D[] r = Physics2D.CircleCastAll(transform.position, .5f, Vector2.up, speed * speed * Time.deltaTime);
        foreach(RaycastHit2D rh in r) {
            if (rh.collider.gameObject.CompareTag("Enemy")) {
                Destroy(rh.collider.gameObject);
                Destroy(gameObject);
                break;
            }
        }
           
        //Cullen
        transform.position = new Vector3(transform.position.x + d.x * speed * Time.deltaTime, transform.position.y + d.y * speed * Time.deltaTime, transform.position.z);
        //Destroy projectile if it leaves the screen
        Vector3 vP = cam.WorldToViewportPoint(transform.position);
        if (vP.x < 0 || vP.x > 1 || vP.y < 0 || vP.y > 1) {
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

    //Cullen
    //void OnTriggerEnter2D(Collider2D collision) {
    //    if (collision.gameObject.CompareTag("Enemy")) {
    //        Destroy(collision.gameObject);
    //        Destroy(gameObject);
    //    }
    //}

}
