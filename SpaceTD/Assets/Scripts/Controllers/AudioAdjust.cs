using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAdjust : MonoBehaviour
{
    public void adjustVolume(float newVol) {
        AudioListener.volume = newVol;
    }
}
