using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHover : MonoBehaviour
{
   public void StoreSelf()
   {
      //Debug.Log("YYAYAYAYAYAY");
      GetComponent<Player>().hoveredTowerView = gameObject;
   }
}
