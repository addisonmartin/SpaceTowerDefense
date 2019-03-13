using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEnemy : Enemy {

    //Cullen
    const float ASTEROID_SPAWN_OFFSET = 10;
    const float ASTEROID_GRAVITY = 8;

    private Vector3 pausedVelocity;
    private float pausedAngularVelocity;

    new void Start() {
        //Cullen
        base.Start();
        rb.velocity = Vector2.zero;

        //Calculate some offset for the asteroid to move towards
        float xVOffset = Random.Range(ASTEROID_GRAVITY * 2, ASTEROID_GRAVITY * 5);
        float yVOffset = Random.Range(ASTEROID_GRAVITY * 2, ASTEROID_GRAVITY * 5);
        float useX = Random.Range(0f, 1f);
        xVOffset = (target.transform.position.x - transform.position.x > 0) ? -xVOffset : xVOffset;
        yVOffset = (target.transform.position.y - transform.position.y > 0) ? -yVOffset : yVOffset;
        rb.velocity = (target.transform.position + new Vector3(useX > .5f ? xVOffset : 0, useX <= .5f ? yVOffset : 0) - transform.position).normalized * (speed + Random.Range(-3, 3));
        pausedVelocity = rb.velocity;
        pausedAngularVelocity = rb.angularVelocity;
    }

    //Cullen
    private void FixedUpdate() {
        if (rb.velocity != Vector2.zero && Core.freeze) {
            pausedVelocity = rb.velocity;
            pausedAngularVelocity = rb.angularVelocity;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            return;
        }
        if (Core.freeze) {
            return;
        }
        if (rb.velocity == Vector2.zero) {
            rb.velocity = pausedVelocity;
            rb.angularVelocity = pausedAngularVelocity;
        }

        Vector3 direction = target.transform.position - transform.position;
        rb.AddForce(direction.normalized * 20f * ASTEROID_GRAVITY / (direction.magnitude));
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        //Cullen
        if (collision.gameObject.CompareTag("Player")) {

            target.GetComponent<Player>().takeDamage(damage);
            Destroy(gameObject);
        }
    }

    protected override void Explode() {
        EmitScrap();
        Core.Boom();
        Destroy(gameObject);
    }

    //Cullen
    public override void spawn(int count, Vector2 position, Enemy e, float scale) {
        //create field of asteroids
        for (int i = 0; i < count; i++) {
            float offX = Random.Range(-ASTEROID_SPAWN_OFFSET, ASTEROID_SPAWN_OFFSET);
            float offY = Random.Range(-ASTEROID_SPAWN_OFFSET, ASTEROID_SPAWN_OFFSET);
            Instantiate<Enemy>(e, position + new Vector2(offX, offY), Quaternion.identity);
        }
    }

    protected override void EUpdate() {
    }
}
