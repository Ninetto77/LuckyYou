using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSettings : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("SetStartValues"))
        {
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetInt("SpeedOfCooking", 5);
            PlayerPrefs.SetInt("Bonus", 1);
            PlayerPrefs.SetInt("ReputationBonus", 1);
            PlayerPrefs.SetInt("Cost", 50);

            PlayerPrefs.SetInt("SetStartValues", 1);
        }
    }


}
