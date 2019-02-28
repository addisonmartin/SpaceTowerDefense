using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Written by Addison
public class LoadSceneOnClick : MonoBehaviour
{
   // Written by Addison
   public void LoadSceneByIndex(int sceneIndex)
   {
      SceneManager.LoadScene(sceneIndex);
   }
}
