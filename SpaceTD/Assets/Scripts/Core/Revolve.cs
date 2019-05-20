using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolve : MonoBehaviour {

    public float omega;

    // Update is called once per frame
    void Update() {
        //if (!Core.freeze) {
            //Cullen
            transform.RotateAround(transform.position, Vector3.forward, omega * Time.deltaTime);

            //correct children so rotation is independent
            foreach (Transform child in transform) {
                child.RotateAround(transform.position, Vector3.forward, -omega * Time.deltaTime);
            }
        //}

    }
}
