﻿using System.Collections;
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
                message.text = "To deploy a Total Overhead Warning Eradication Rail, or T.O.W.E.R, click on the earth, then select the tower you want from the menu on the right. There's only one variety for now, a mass driver, but the boys in the lab assure me they're working on more. Then click where you want it to be deployed.";
                press.text = "Deploy a T.O.W.E.R. to continue";
                tutorialStep = 2;
            }
        }
        else if (tutorialStep == 2)
        {
            if (Player.numTowers > 0)
            {
                message.text = "Nicely done, commander. As I'm sure you've noticed, that tower didn't stay in place once it got up there. The lab boys tell me that thanks to our good friend gravity, anything we send up has got to keep moving or else it'll fall right out of the sky. There's apparently only a few orbitals stable enough for the towers to revolve at.";
                press.text = "Press space to continue";
                tutorialStep = 3;
            }
        }
        else if (tutorialStep == 3)
        {
            if (Input.GetKeyDown("space"))
            {
                message.text = "Here comes the first set of asteroids. We have enough resources for one more tower- if the first one you sent up looks like it's going to orbit away from your targets, you can send up a second one for extra coverage.";
                press.text = "Press space to dismiss";
                tutorialStep = 4;
            }
        }
        else if (tutorialStep == 4)
        {
            if (Input.GetKeyDown("space"))
            {
                message.text = "";
                press.text = "";
                tutorialStep = 5;
            }
        }
        else if (tutorialStep == 5)
        {
            if (Core.waveNum > 0)
            {
                message.text = "The lab boys are telling me that some of the leftover material from those asteroids contains the rare metals we need to construct more towers. Good thing too- it looks like another set of asteroids is inbound. You might want to deploy a tower or two around the moon, too.";
                press.text = "Press space to dismiss.";
                tutorialStep = 6;
            }
        }
        else if (tutorialStep == 6)
        {
            if (Input.GetKeyDown("space"))
            {
                message.text = "";
                press.text = "";
                tutorialStep = 7;
            }
        }
    }
}