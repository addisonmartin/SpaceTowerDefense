using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Cullen
public class AstralBody : MonoBehaviour, ISelectable
{

    public static Image selectedImage;

    public void Start() {
        selectedImage = GameObject.Find("SelectedDisplay").GetComponent<Image>();
        //selectedImage = FindObjectOfType<Image>();
    }

    public void display() {
        selectedImage.sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void undisplay() {
        selectedImage.sprite = null;
    }

}
