using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuFuncs : MonoBehaviour {

   // Written by Addison
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
         endlessModeHighScore.text = "Haungs you do good no matter what the number says :)";

         levelOneButton.interactable = true;
         levelOneEndlessButton.interactable = true;
         levelOneHighscore.text = "99999999999";
         levelTwoButton.interactable = true;
         levelTwoEndlessButton.interactable = true;
         levelTwoHighscore.text = "This actually works in regular mode.";
         levelThreeButton.interactable = true;
         levelThreeEndlessButton.interactable = true;
         levelThreeHighscore.text = "Can we get an A lol?";
      }
      else {
         levelOneButton.interactable = Core.levelOneUnlocked;
         levelOneEndlessButton.interactable = Core.levelOneCompleted;
         levelOneHighscore.text = "Level 1 Endless Highscore:\n" + Core.levelOneEndlessModeHighScore;
         levelTwoButton.interactable = Core.levelTwoUnlocked;
         levelTwoEndlessButton.interactable = Core.levelTwoCompleted;
         levelTwoHighscore.text = "Level 2 Endless Highscore:\n" + Core.levelTwoEndlessModeHighScore;
         levelThreeButton.interactable = Core.levelThreeUnlocked;
         levelThreeEndlessButton.interactable = Core.levelThreeCompleted;
         levelThreeHighscore.text = "Level 3 Endless Highscore:\n" + Core.levelThreeEndlessModeHighScore;
      }
   }

}
