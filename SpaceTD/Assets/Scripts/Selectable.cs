using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cullen
public class Selectable : MonoBehaviour{

    //Cullen
    public static Selectable selected = null;
    ISelectable selectable;

    //Cullen
    public void Start() {
        selectable = GetComponent<ISelectable>();
    }

    public void Update() {
        
    }
    
    //Cullen
    public void select() {
        //Undisplay current selected
        if (selected!=null && selected.selectable != null) {
            selected.selectable.undisplay();
        }
        //display new selection
        if (selectable != null) {
            selected = this;
            selectable.display();
        } else {
            selected = null;
        }
        
    }

    //Cullen
    public void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            select();
        }
    }

}

//Cullen
public interface ISelectable {
    void display();
    void undisplay();
}