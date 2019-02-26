using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    private int step;
    public Text tutorialText;
    // Start is called before the first frame update
    void Start()
    {
        step = 0;
        //Core.freeze = true;
        tutorialText.text = "Greetings, commander. Our sensors have detected a rogue asteroid cloud on a direct path for Earth. In order to protect the planet, our best scientists have developed an automated defense system to destroy the asteroids before impact. You have been chosen to oversee the deployment and operation of these defenses.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
