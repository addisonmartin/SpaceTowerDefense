using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trilobite : Enemy {

    public float cooldown;
    private float nextFire = 0f;
    public float stopDistance = 0f;

    private bool turnRight = false;
    public float turnAngle = 15f;
    private float turn = 0f;

    // Start is called before the first frame update
    new void Start() {
        base.Start();
    }

    // Update is called once per frame
    protected override void EUpdate() {
        //base.Update();
        //Cullen
        if (!Core.freeze) {

            //Cullen
            d = target.transform.position - transform.position;
            float angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 50f * Time.deltaTime);
            doWave();

            //Cullen
            if (Vector2.Distance(transform.position, target.transform.position) <= target.transform.lossyScale.x * target.GetComponent<CircleCollider2D>().radius + stopDistance) {

                //Cullen
                if (nextFire <= 0f) {
                    Core.player.takeDamage(damage);
                    nextFire = cooldown;
                } else {
                    nextFire -= Time.deltaTime;
                }
            } else {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }


        } 

    }

    //Cullen
    private void doWave() {
        Quaternion q = Quaternion.AngleAxis(transform.eulerAngles.z + (turnRight ? -turnAngle : turnAngle), Vector3.forward);
        Quaternion q2 = Quaternion.RotateTowards(transform.rotation, q, 200f * Time.deltaTime);
        turn += Mathf.Abs(q2.eulerAngles.z - transform.eulerAngles.z);
        //Debug.Log(turn);
        transform.rotation = q2;
        if (turn >= turnAngle) {
            turnRight = !turnRight;
            turn = 0;
        }


    }

    public override void spawn(int count, Vector2 position, GameObject e) {
        GameObject enemy = Instantiate(e, position, Quaternion.identity);
    }
}
