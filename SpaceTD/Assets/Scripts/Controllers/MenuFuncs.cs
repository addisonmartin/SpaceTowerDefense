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
    public Text highscoreText;
    public bool isLevelSelect;
    public GameObject pauseMenu;

    public void Start() {
        Cursor.visible = true;

        if (isLevelSelect) {
            lockUnlockLevels();
        }
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
        if (isLevelSelect) {
            Core.endlessMode = endlessToggle.isOn;
            Core.difficulty = difficultyDropdown.value;
        }
        SceneManager.LoadScene(sceneIndex);
    }

    public void setVolume(float vol) {
        AudioListener.volume = vol;
    }

    public void resumeGame() {
        Core.freeze = false;
        pauseMenu.SetActive(false);
    }

    public void lockUnlockLevels() {

        if (levelNum == 1) {
            startButton.interactable = Core.levelOneUnlocked;
            endlessToggle.interactable = Core.levelOneCompleted;
            highscoreText.text = "Endless Highscore: " + Core.levelOneEndlessModeHighScore;
        } else if (levelNum == 2) {
            startButton.interactable = Core.levelTwoUnlocked;
            endlessToggle.interactable = Core.levelTwoCompleted;
            highscoreText.text = "Endless Highscore: " + Core.levelTwoEndlessModeHighScore;
        } else if (levelNum == 3) {
            startButton.interactable = Core.levelThreeUnlocked;
            endlessToggle.interactable = Core.levelThreeCompleted;
            highscoreText.text = "Endless Highscore: " + Core.levelThreeEndlessModeHighScore;
        } else if (levelNum == 4) {
            startButton.interactable = Core.levelFourUnlocked;
            endlessToggle.interactable = Core.levelFourCompleted;
            highscoreText.text = "Endless Highscore: " + Core.levelFourEndlessModeHighScore;
        } else if (levelNum == 5) {
            startButton.interactable = Core.levelFiveUnlocked;
            endlessToggle.interactable = Core.levelFiveCompleted;
            highscoreText.text = "Endless Highscore: " + Core.levelFiveEndlessModeHighScore;
        } else if (levelNum == 6) {
            startButton.interactable = Core.levelSixUnlocked;
            endlessToggle.interactable = Core.levelSixCompleted;
            highscoreText.text = "Endless Highscore: " + Core.levelSixEndlessModeHighScore;
        }
    }

}
