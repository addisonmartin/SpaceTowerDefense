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

    public void Start() {
        Cursor.visible = true;
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

}
