﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Cullen
public class Selectable : MonoBehaviour {

    //Cullen
    public static Selectable selected = null;
    public static Selectable lastSelected;
    private Vector2 mousePos;
    ISelectable selectable;

    //Cullen
    public void Start() {
        selectable = GetComponent<ISelectable>();
        if (lastSelected == null) {
            lastSelected = Core.player.GetComponent<Selectable>();
        }
    }

    //Cullen
    public void select() {
        //Undisplay current selected
        if (selected != null && selected.selectable != null) {
            selected.selectable.undisplay();
        }
        //display new selection
        if (selectable != null) {
            lastSelected = this;
            selected = this;
            selectable.display();
        } else {
            selected = null;
        }

    }

    //Cullen
    public void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            mousePos = Input.mousePosition;
            //select();
        }
    }

    //Cullen
    public void OnMouseUp() {
        if (Input.GetMouseButtonUp(0) && !Input.GetKey(KeyCode.LeftShift) && ((Vector2)Input.mousePosition - mousePos).sqrMagnitude < 80f) {
            select();
        }
    }

    public void OnMouseEnter() {
        if (selectable != null) {
            selectable.highlight(true);
        }
    }

    public void OnMouseExit() {
        if (selectable != null) {
            selectable.highlight(false);
        }
    }

}

//Cullen
public interface ISelectable {
    void display();
    void undisplay();
    void highlight(bool h);
}