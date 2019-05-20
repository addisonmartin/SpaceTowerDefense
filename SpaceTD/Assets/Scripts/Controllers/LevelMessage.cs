using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Cullen
public class LevelMessage : MonoBehaviour {
    private int step = 0;
    //public Player player;

    public Text text;
    public Text press;
    [TextArea]
    public string[] messages;

    void Start() {
        if (messages.Length <= 0) {
            press.text = "";
            text.text = "";
            Core.preLevel = false;
            enabled = false;
            return;
        }
        text.text = messages[0];
        press.text = "Press space to dismiss";
    }

    // Update is called once per frame
    void Update() {
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (++step >= messages.Length) {
                press.text = "";
                text.text = "";
                Input.ResetInputAxes();
                Core.preLevel = false;
                enabled = false;
                return;
            }
            text.text = messages[step];
        }
    }
}
