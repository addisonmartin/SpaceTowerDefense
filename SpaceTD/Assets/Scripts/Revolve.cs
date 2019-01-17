using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolve : MonoBehaviour
{

    public float omega;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.forward, omega * Time.deltaTime);
        //correct children so rotation is independent
        foreach (Transform child in transform){
            child.RotateAround(transform.position, Vector3.forward, -omega * Time.deltaTime);
        }
    }
}
