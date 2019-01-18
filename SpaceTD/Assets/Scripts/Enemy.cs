﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //Cullen
    public float speed;
    private GameObject target;
    private Vector2 d;

    // Start is called before the first frame update
    void Start()
    {
        //Cullen
        target = GameObject.FindGameObjectWithTag("Player");
        d = target.transform.position - transform.position;
        d /= d.magnitude;
    }

    void FixedUpdate()
    {
        //Cullen
        float zRot = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90);
        transform.position = new Vector3(transform.position.x + d.x * speed * Time.deltaTime, transform.position.y + d.y * speed * Time.deltaTime, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }else if (collision.gameObject.CompareTag("Projectile")) {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}