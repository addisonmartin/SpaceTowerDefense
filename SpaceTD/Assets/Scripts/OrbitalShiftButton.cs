using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Addison
public class OrbitalShiftButton : MonoBehaviour
{
    public int towerIndex = -1;
    public int shiftFactor = 0;

    public void onClick()
    {
      Debug.Log(towerIndex);
    }
}
