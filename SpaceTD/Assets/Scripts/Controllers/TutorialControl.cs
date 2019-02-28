using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialControl : MonoBehaviour
{
    private int tutorialStep = 0;

    public Text message;
    public Text press;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(tutorialStep == 0)
        {
            message.text = "Greetings, Commander. Our long-range telescopes recently picked up an abnormally dense cloud of asteroids on a direct collision course with Earth. Our greatest minds have devised an automated defense system to destroy them before they hit the planet. You have been chosen to oversee the deployment and operation of this system. Good luck, Commander.";
            Core.freeze = true;
            tutorialStep = 1;
        }
        else if(tutorialStep == 1)
        {
            if (Input.GetKeyDown("space"))
            {
                Core.freeze = false;
                tutorialStep = 2;
                message.text = "";
                press.enabled = false;
                tutorialStep = 2;
            }
        }
        else if (tutorialStep == 2)
        {
            if (Input.GetKeyDown("space"))
            {
                Core.freeze = false;
                tutorialStep = 2;
                message.text = "";
                press.enabled = false;
                tutorialStep = 2;
            }
        }
        else if (tutorialStep == 3)
        {
            if (Input.GetKeyDown("space"))
            {
                Core.freeze = false;
                tutorialStep = 2;
                message.text = "";
                press.enabled = false;
                tutorialStep = 2;
            }
        }
    }
}
