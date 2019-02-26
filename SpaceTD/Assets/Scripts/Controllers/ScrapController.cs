using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapController : MonoBehaviour {
    //Lukas
    private Vector2 initialDir;
    private Vector3 centralBodyPos;
    private float centralBodyRadius;
    private Rigidbody2D rb;
    private GameObject centralBody;
    private int scrapValue;
    public float pullForce;


    void Start() {
        //Lukas
        initialDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(initialDir * 100);
        centralBody = GameObject.Find("Central Object");
        centralBodyPos = centralBody.transform.position;
        centralBodyRadius = centralBody.GetComponent<CircleCollider2D>().radius * centralBody.transform.lossyScale.x;
        
    }

    void Update() {

    }

    public void setValue(int value) {
        scrapValue = value;
    }

    private void FixedUpdate() {
        if (!Core.freeze)
        {
            //Lukas
            Vector3 direction = centralBodyPos - transform.position;
            if (direction.sqrMagnitude < centralBodyRadius * centralBodyRadius)
            {
                //increment resource counter in central body "Resource" script
                centralBody.GetComponent<Player>().addScrap(scrapValue);
                Destroy(gameObject);
            }
            direction.Normalize();
            rb.AddForce(direction * pullForce);
        }
        if (Core.freeze)
        {
            rb.velocity = Vector3.zero;
        }

    }
}
