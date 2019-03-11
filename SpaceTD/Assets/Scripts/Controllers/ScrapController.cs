using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapController : MonoBehaviour {
    //Lukas
    private Vector2 initialDir;
    //private Vector3 centralBodyPos;
    private float collectorRadius;
    private Rigidbody2D rb;
    //private Player player;
    private int scrapValue;
    public float pullForce;
    public Vector2 target;


    void Start() {
        //Lukas
        initialDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(initialDir * 100);
        //centralBody = GameObject.Find("Central Object");
        //centralBodyPos = centralBody.transform.position;
        //collectorRadius = Core.player.GetComponent<CircleCollider2D>().radius * Core.player.transform.lossyScale.x;
        collectorRadius = 5f;
    }

    public void setValue(int value) {
        scrapValue = value;
    }

    private void FixedUpdate() {
        if (!Core.freeze || Core.buildMode) {
            //Lukas
            Vector3 direction = (Vector3) target - transform.position;
            if (direction.sqrMagnitude < collectorRadius * collectorRadius) {
                Core.player.addScrap(scrapValue);
                Destroy(gameObject);
            }
            direction.Normalize();
            rb.AddForce(direction * pullForce);
        } else {
            rb.velocity = Vector3.zero;
        }

    }
}
