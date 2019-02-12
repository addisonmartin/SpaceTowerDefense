using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    //Lukas
    public GameObject scrapPrefab;
    public int ScrapAmount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EmitScrap(Transform parentPosition)
    {
        for (int i = 0; i < ScrapAmount; i++)
        {
            Quaternion randRotation = Quaternion.Euler(0, 0, Random.Range(-360.0f, 360.0f));
            GameObject scr = Instantiate(scrapPrefab, parentPosition.position, randRotation);
        }
    }

}
