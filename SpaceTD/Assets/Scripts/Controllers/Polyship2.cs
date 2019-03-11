using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polyship2 : Polyship {

    //Cullen
    public override void spawn(int count, Vector2 position, Enemy e, float scale) {
        bool left = true;
        Vector2 dir = Core.player.transform.position - (Vector3) position;
        dir.Normalize();
        Enemy enemy = Instantiate(e, position, Quaternion.identity);
        enemy.healthMult = scale;
        enemy.transform.localScale *= Mathf.Min(.99f + scale / 100f, 3f);
        //Transform axis = enemy.transform;
        float deg = Vector2.Angle(Vector2.up, dir);
        //Debug.Log(enemy.transform.eulerAngles.z);
        float cos = Mathf.Cos(deg * Mathf.Deg2Rad);
        float sin = Mathf.Sin(deg * Mathf.Deg2Rad);

        //spawn in a line
        for (int i = 1; i < count; i++) {
            float tx = (left ? -((i + 1) / 2 * 3f) : ((i + 1) / 2 * 3f));
            Vector2 nextPos = new Vector3(cos * tx, sin * tx);
            enemy = Instantiate(e, position + nextPos, Quaternion.identity);
            enemy.healthMult = scale;
            enemy.transform.localScale *= Mathf.Min(.99f + scale / 100f, 3f);
            //enemy.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            //enemy.transform.RotateAround(position, Vector3.forward, theta);
            left = !left;
        }

    }
}
