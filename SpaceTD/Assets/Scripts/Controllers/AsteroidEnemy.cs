using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEnemy : Enemy {

    const float ASTEROID_SPAWN_OFFSET = 10;
    const float ASTEROID_GRAVITY = 10;

    // Start is called before the first frame update
    new void Start() {
        //Cullen
        base.Start();
        rb.velocity = Vector2.zero;
        float xVOffset = Random.Range(0, ASTEROID_GRAVITY * 5);
        float yVOffset = Random.Range(0, ASTEROID_GRAVITY * 5);
        xVOffset = (target.transform.position.x - transform.position.x > 0) ? -xVOffset : xVOffset;
        yVOffset = (target.transform.position.y - transform.position.y > 0) ? -yVOffset : yVOffset;
        rb.velocity = (target.transform.position + new Vector3(xVOffset, yVOffset) - transform.position).normalized * (speed + Random.Range(-3, 3));
        //rb.rotation = 0;
        //Debug.Log(target.transform.position);

    }

    //Cullen
    private void FixedUpdate() {
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();
        rb.AddForce(direction * ASTEROID_GRAVITY);
        //Debug.Log("asteroid update!");
    }

    //void Update() {
    //    //Daniel
    //    if (!Core.freeze) {
    //        rb.velocity = d * speed;
    //        transform.Rotate(0, 0, 1);
    //    } else {
    //        rb.velocity = Vector2.zero;
    //    }
    //}

    void OnTriggerEnter2D(Collider2D collision) {
        //Cullen
        if (collision.gameObject.CompareTag("Player")) {

            target.GetComponent<Player>().takeDamage(damage);
            Destroy(gameObject);
        }
    }

    //Cullen
    public override void spawn(int count, Vector2 position, GameObject e) {
        //create field of asteroids
        for (int i = 0; i < count; i++) {
            float offX = Random.Range(-ASTEROID_SPAWN_OFFSET, ASTEROID_SPAWN_OFFSET);
            float offY = Random.Range(-ASTEROID_SPAWN_OFFSET, ASTEROID_SPAWN_OFFSET);
            Instantiate(e, position + new Vector2(offX, offY), Quaternion.identity);
        }
    }
}
