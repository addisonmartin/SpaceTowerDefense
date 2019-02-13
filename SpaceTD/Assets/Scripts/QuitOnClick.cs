using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Addison
public class QuitOnClick : MonoBehaviour
{

   // Written by Addison
   public void Quit()
   {
      #if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
      #else
         Application.Quit();
      #endif
   }
}
