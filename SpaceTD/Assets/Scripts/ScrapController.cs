using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapController : MonoBehaviour
{
    private Vector2 initialDir;
    // Start is called before the first frame update
    void Start()
    {
        initialDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        gameObject.GetComponent<Rigidbody2D>().AddForce(initialDir * 100);
        //Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    public void PullForce()
    {

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //Collection is in the main planet script to 
        //increment resource Counter.
    }
}
