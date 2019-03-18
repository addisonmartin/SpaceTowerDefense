using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialControl : MonoBehaviour {
    private int tutorialStep = 0;
    //public Player player;

    public Text message;
    public Text press;
    public AstralBody moon;

    // Update is called once per frame
    void Update() {
        if (tutorialStep == 0) {
            message.text = "Greetings, Commander. Our long-range telescopes recently picked up an abnormally dense cloud of asteroids on a direct collision course with Earth. Our greatest scientists have devised an automated defense system to destroy them before they hit our home! You have been chosen to oversee the deployment and operation of this system. Good luck, Commander.";
            press.text = "Press space to continue";
            Core.freeze = true;
            Core.buildMode = false;
            tutorialStep = 1;
        } else if (tutorialStep == 1) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                Core.freeze = false;
                Core.waveSpawner.enabled = false;
                message.text = "To deploy a Total Overhead Warning Eradication Rail, or T.O.W.E.R, click on the earth, then select the tower you want from the menu on the right. There's only one variety for now, a mass driver, but the scientists in the lab assure me they're working on more. Then click where you want it to be deployed.";
                press.text = "Deploy a T.O.W.E.R. to continue";
                tutorialStep = 2;
            }
        } else if (tutorialStep == 2) {
            if (Core.player.getNumTowers() > 0) {
                message.text = "Nicely done, commander. As I'm sure you've noticed, that tower didn't stay in place once it got up there. The scientists tell me that thanks to our good friend gravity, anything we send up has got to keep moving or else it'll fall right out of the sky. There's apparently only a few orbitals stable enough for the towers to revolve at.";
                press.text = "Press space to continue";
                tutorialStep = 3;
            }
        } else if (tutorialStep == 3) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                message.text = "We still have more resources (lower right). You can deploy towers quickly by pressing shift when you place them. You can keep placing as long as you hold shift!";
                press.text = "Deploy two more T.O.W.E.Rs to continue";
                tutorialStep = 4;
            }
        } else if (tutorialStep == 4) {
            if (Core.player.getNumTowers() > 2) {
                message.text = "Now let's upgrade one of your towers. Click on the Earth, then in the upper right click on a tower to see a detailed view of that tower. You can sell or upgrade your towers from here.";
                press.text = "Upgrade a T.O.W.E.R. to continue";
                tutorialStep = 5;
            }
        } else if (tutorialStep == 5) {


            if (Core.player.hasUpgradedTower()) {
                Core.waveSpawner.enabled = true;
                message.text = "Here comes the first wave of asteroids!";
                press.text = "Press space to dismiss";
                tutorialStep = 6;
            } else if (Core.player.scrap < 150) {
                Core.player.addScrap(150);
            }
        } else if (tutorialStep == 6) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                message.text = "";
                press.text = "";
            }
            if (Core.waveNum > 0) {
                Core.waveSpawner.enabled = false;
                message.text = "The scientists are telling me that some of the leftover material from those asteroids contains the rare metals we need to construct and upgrade more towers! Let's deploy a tower on the moon to get some distant coverage.";
                press.text = "Deploy a T.O.W.E.R. on the moon to continue";
                tutorialStep = 7;
            }
        } else if (tutorialStep == 7) {

            if (moon.orbitals[0].towers.Count > 0) {
                Core.waveSpawner.enabled = true;
                message.text = "";
                press.text = "";
            } else if (Core.player.scrap < 150) {
                Core.player.addScrap(150);
            }
            if (Core.waveNum >= Core.waveSpawner.waves.Length) {
                //Core.waveSpawner.enabled = true;
                message.text = "\n\nWhat in tarnation?";
                press.text = "";
                tutorialStep = 8;
            }
        } else if (tutorialStep == 8) {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
                message.text = "\n\nTutorial Complete!";
                press.text = "Press space to return to the main menu";
                tutorialStep = 9;
            }
        } else if (tutorialStep == 9) {
            Core.levelOneUnlocked = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(0);
            }
        }
    }
}
