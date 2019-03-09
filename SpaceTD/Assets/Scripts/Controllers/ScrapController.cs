using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapController : MonoBehaviour {
    //Lukas
    private Vector2 initialDir;
    //private Vector3 centralBodyPos;
    private float centralBodyRadius;
    private Rigidbody2D rb;
    //private Player player;
    private int scrapValue;
    public float pullForce;


    void Start() {
        //Lukas
        initialDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(initialDir * 100);
        //centralBody = GameObject.Find("Central Object");
        //centralBodyPos = centralBody.transform.position;
        centralBodyRadius = Core.player.GetComponent<CircleCollider2D>().radius * Core.player.transform.lossyScale.x;
    }

    public void setValue(int value) {
        scrapValue = value;
    }

    private void FixedUpdate() {
        if (!Core.freeze)
        {
            //Lukas
            Vector3 direction = Core.player.transform.position - transform.position;
            if (direction.sqrMagnitude < centralBodyRadius * centralBodyRadius)
            {
                //increment resource counter in central body "Resource" script
                Core.player.addScrap(scrapValue);
                Destroy(gameObject);
            }
            direction.Normalize();
            rb.AddForce(direction * pullForce);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

    }
}
