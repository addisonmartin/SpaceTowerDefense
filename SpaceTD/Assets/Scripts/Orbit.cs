using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(transform.position - transform.localPosition, Vector3.forward, speed * Time.deltaTime);
        transform.RotateAround(transform.parent.position, Vector3.forward, speed * Time.deltaTime);
    }
}
