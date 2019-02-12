using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapController : MonoBehaviour
{
    //Lukas
    private Vector2 initialDir;
    private Vector2 CentralBodyLocation;
    private float CentralBodyRadius;
    private Rigidbody2D rb;
    private GameObject CentralBody;
    public int ScrapValue;
    public float pullForce;


    void Start()
    {
        //Lukas
        initialDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        gameObject.GetComponent<Rigidbody2D>().AddForce(initialDir * 100);
        CentralBody = GameObject.Find("Central Object");
        CentralBodyLocation = CentralBody.GetComponent<Transform>().position;
        CentralBodyRadius = CentralBody.GetComponent<CircleCollider2D>().radius;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Lukas
        Vector2 ScrapLocation = GetComponent<Transform>().position;
        Vector2 direction = CentralBodyLocation - ScrapLocation;
        if (direction.sqrMagnitude < CentralBodyRadius * CentralBodyRadius) 
        {
            //increment resource counter in central body "Resource" script
            CentralBody.GetComponent<Resources>().AddScrap(ScrapValue);
            Destroy(gameObject);
        }
        direction.Normalize();
        rb.AddForce(direction * pullForce);

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //Collection is in the main planet script to 
        //increment resource Counter.
    }
}
