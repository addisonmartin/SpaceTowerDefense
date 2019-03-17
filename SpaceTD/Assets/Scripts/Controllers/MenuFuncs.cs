using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuFuncs : MonoBehaviour {

   // Written by Addison
   public Button endlessModeButton;
   public Text endlessModeHighScore;
   public Button levelOneButton;
   public Button levelOneEndlessButton;
   public Text levelOneHighscore;
   public Button levelTwoButton;
   public Button levelTwoEndlessButton;
   public Text levelTwoHighscore;
   public Button levelThreeButton;
   public Button levelThreeEndlessButton;
   public Text levelThreeHighscore;

    public void Start() {
        Cursor.visible = true;
        enableDisableLevelButtons();
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
        SceneManager.LoadScene(sceneIndex);
    }

    public void setEndless(bool endless) {
        Core.endlessMode = endless;
    }

    public void setVolume(float vol) {
        AudioListener.volume = vol;
    }

    public void enableDisableLevelButtons() {

      if (Core.isHaungsMode) {
         endlessModeHighScore.text = "Haungs you do good no matter\nwhat the number says :)";
         endlessModeButton.interactable = true;
         levelOneButton.interactable = true;
         levelOneEndlessButton.interactable = true;
         levelOneHighscore.text = "Highscore:\n99999999999";
         levelTwoButton.interactable = true;
         levelTwoEndlessButton.interactable = true;
         levelTwoHighscore.text = "This actually works\nin regular mode.";
         levelThreeButton.interactable = true;
         levelThreeEndlessButton.interactable = true;
         levelThreeHighscore.text = "Can we get an A lol?";
      }
      else {
         endlessModeHighScore.text = "Endless Mode Highscore:\n" + Core.endlessModeHighScore;
         //endlessModeButton.interactable = Core.endlessModeUnlocked;
         endlessModeButton.interactable = true;
         //levelOneButton.interactable = Core.levelOneUnlocked;
         levelOneButton.interactable = true;
         levelOneEndlessButton.interactable = Core.levelOneCompleted;
         levelOneHighscore.text = "Level 1 Endless Highscore:\n" + Core.levelOneEndlessModeHighScore;
         //levelTwoButton.interactable = Core.levelTwoUnlocked;
         levelTwoButton.interactable = true;
         levelTwoEndlessButton.interactable = Core.levelTwoCompleted;
         levelTwoHighscore.text = "Level 2 Endless Highscore:\n" + Core.levelTwoEndlessModeHighScore;
         //levelThreeButton.interactable = Core.levelThreeUnlocked;
         levelThreeButton.interactable = true;
         levelThreeEndlessButton.interactable = Core.levelThreeCompleted;
         levelThreeHighscore.text = "Level 3 Endless Highscore:\n" + Core.levelThreeEndlessModeHighScore;
      }
   }

}
