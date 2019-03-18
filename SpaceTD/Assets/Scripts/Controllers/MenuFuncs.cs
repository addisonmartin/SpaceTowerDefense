using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuFuncs : MonoBehaviour {

   // Written by Addison
   public Text levelTitle;
   public Button previousLevelButton;
   public Button nextLevelButton;
   public Image levelImage;
   public Toggle endlessToggle;
   public Dropdown difficultyDropdown;
   public Button startButton;
   public int levelNum;

    public void Start() {
        Cursor.visible = true;

        lockUnlockLevels();
    }

    // Written by Addison
    public void Quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    // Written by Addison
    public void LoadSceneByIndex(int sceneIndex) {
        Core.endlessMode = endlessToggle.isOn;
        if (difficultyDropdown.itemText.text == "Haungs") {
           Core.isHaungsMode = true;
        }
        else {
           Core.isHaungsMode = false;
        }
        SceneManager.LoadScene(sceneIndex);
    }

    public void setVolume(float vol) {
        AudioListener.volume = vol;
    }

    public void lockUnlockLevels() {

      if (levelNum == 1) {
         startButton.interactable = Core.levelOneUnlocked;
         endlessToggle.interactable = Core.levelOneCompleted;
      }
      else if (levelNum == 2) {
         startButton.interactable = Core.levelTwoUnlocked;
         endlessToggle.interactable = Core.levelTwoCompleted;
      }
      else if (levelNum == 3) {
         startButton.interactable = Core.levelThreeUnlocked;
         endlessToggle.interactable = Core.levelThreeCompleted;
      }
      else if (levelNum == 4) {
         startButton.interactable = Core.levelFourUnlocked;
         endlessToggle.interactable = Core.levelFourCompleted;
      }
      else if (levelNum == 5) {
         startButton.interactable = Core.levelFiveUnlocked;
         endlessToggle.interactable = Core.levelFiveCompleted;
      }
      else if (levelNum == 6) {
         startButton.interactable = Core.levelSixUnlocked;
         endlessToggle.interactable = Core.levelSixCompleted;
      }
   }

}
