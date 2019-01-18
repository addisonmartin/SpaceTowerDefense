using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    //Cullen
    public float speed;
    private Camera cam;
    private Vector2 d;

    // Start is called before the first frame update
    void Start()
    {
        cam = (Camera)GameObject.FindObjectOfType(typeof(Camera));
    }

   
    void FixedUpdate()
    {
        //Cullen
        transform.position = new Vector3(transform.position.x + d.x * speed * Time.deltaTime, transform.position.y + d.y * speed * Time.deltaTime, transform.position.z);
        //Destroy projectile if it leaves the screen
        Vector3 vP = cam.WorldToViewportPoint(transform.position);
        if(vP.x < 0 || vP.x > 1 || vP.y < 0 || vP.y > 1) {
            Destroy(gameObject);
        }
    }

    //Cullen
    public void setDirection(Vector2 dir) {
        this.d = dir;
    }

    //Cullen
    //void OnTriggerEnter2D(Collider2D collision) {
    //    if (collision.gameObject.CompareTag("Enemy")) {
    //        Destroy(collision.gameObject);
    //        Destroy(gameObject);
    //    }
    //}

}
