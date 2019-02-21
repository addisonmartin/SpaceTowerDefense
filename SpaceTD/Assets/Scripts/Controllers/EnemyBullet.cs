using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{ //Lukas
    private Rigidbody2D rb;

    //Cullen
    public float speed;
    private static CameraController camControl;
    private Vector2 d;
    private float damage;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (camControl == null)
        {
            camControl = GameObject.Find("Camera Rig").GetComponent<CameraController>();
        }
        damage = 2;
    }

    //Cullen
    public void setDamage(float d)
    {
        damage = d;
    }

    private void Update()
    {


        transform.Translate(Vector3.up * Time.deltaTime * 1);
        if (!camControl.inWorld(transform.position))
        {
            Destroy(gameObject);
        }
    }

    //Cullen
    public void setDirection(Vector2 dir)
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Cullen
        if (collision.gameObject.CompareTag("Player"))
        {

            target.GetComponent<Player>().takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
