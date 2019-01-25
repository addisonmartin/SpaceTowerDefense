using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cullen
public class AstralBody : MonoBehaviour, ISelectable
{
    public void display() {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void undisplay() {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

}
