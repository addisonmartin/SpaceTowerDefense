using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
   //Lukas
   private Rigidbody2D rb;

   //Cullen
   public float speed;
   private static CameraController camControl;
   private Vector2 d;
   private float damage;

   // Written by Addison
   public float explosionRadius = 10f;

   // Start is called before the first frame update
   void Start()
   {
      if (camControl == null)
      {
         camControl = GameObject.Find("Camera Rig").GetComponent<CameraController>();
      }
   }

   //Cullen
   public void setDamage(float d)
   {
      damage = d;
   }

   void Update()
   {
      // Written by Addison
      RaycastHit2D[] r = Physics2D.CircleCastAll(transform.position, explosionRadius, d, speed * Time.deltaTime);
      bool shouldDestroySelf = false;

      foreach (RaycastHit2D rh in r)
      {
         if (rh.collider.gameObject.CompareTag("Enemy"))
         {
            rh.collider.gameObject.GetComponent<Enemy>().takeDamage(damage);
            shouldDestroySelf = true;
         }
      }

      if (shouldDestroySelf)
      {
         Destroy(gameObject);
      }
      else
      {
         Vector3 towards = Vector3.MoveTowards(transform.position, new Vector3(d.x, d.y, transform.position.z), Time.deltaTime);
         transform.position = new Vector3(towards.x, towards.y, transform.position.z);

         //Cullen
         //Destroy projectile if it leaves the screen
         if (!camControl.inWorld(transform.position))
         {
            Destroy(gameObject);
         }
      }
   }

   //Cullen
   public void setDirection(Vector2 dir)
   {
      this.d = dir;
      float zRot = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90f);
      //rb.velocity = d * speed;
   }
}
