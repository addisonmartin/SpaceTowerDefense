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

        //transform.position - transform.localPostion is equivalent to the position of the parent
        transform.RotateAround(transform.position - transform.localPosition, Vector3.forward, speed * Time.deltaTime);
    }
}
