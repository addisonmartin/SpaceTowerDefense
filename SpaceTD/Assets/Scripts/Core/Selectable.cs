using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Cullen
public class Selectable : MonoBehaviour {

    //Cullen
    public static Selectable selected = null;
    public static Selectable lastSelected;
    public static bool overrideClick = false;
    private Vector2 mousePos;
    private bool s;
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
            selectable.display();
            selected = this;
        } else {
            selected = null;
        }

    }

    //Cullen
    public void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            mousePos = Input.mousePosition;
            s = Core.player.towerToPlace == null;
            if (!s) {
                overrideClick = selectable != null;
            }
            //select();
        }
    }

    //Cullen
    public void OnMouseUp() {
        if (Input.GetMouseButtonUp(0) && (s || overrideClick) && selected != this && ((Vector2)Input.mousePosition - mousePos).sqrMagnitude < 80f) {
            select();
        }
    }

    //Cullen
    public void OnMouseEnter() {
        if (selectable != null) {
            selectable.highlight(true);
        }
    }

    //Cullen
    public void OnMouseExit() {
        if (selectable != null) {
            selectable.highlight(false);
        }
    }

}

//Cullen
public interface ISelectable {
    void display(int x = -1, int y = -1);
    void undisplay(bool a = true);
    void highlight(bool h);
}