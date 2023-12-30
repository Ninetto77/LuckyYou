using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Sprite[] tutorialSprites;
    [SerializeField]private Image tutorial;

    private bool[] HaveShown;

    private void Awake()
    {
        HaveShown = new bool[tutorialSprites.Length];
        for (int i = 0; i < tutorialSprites.Length; i++)
        {
            HaveShown[i] = false;
            Debug.Log(HaveShown[i]);
        }
    }

    //public bool CanShowTutorial(int i)
    //{
    //    if (i > 1 && HaveShown[i - 1])
    //    {
    //        Debug.Log(1);
    //        tutorial.sprite = tutorialSprites[i - 1];
    //        HaveShown[i - 1] = true;
    //        return true;
    //    }
    //    else if( i == 1) return true;

    //    else return false;
    //}

    public bool CanShowTutorial(int i)
    {
        if (HaveShown[i - 1] == false)
        {
            Debug.Log(1);
            tutorial.sprite = tutorialSprites[i - 1];
            HaveShown[i - 1] = true;
            return true;
        }
        else return false;
    }

    
}
