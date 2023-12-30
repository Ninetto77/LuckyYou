using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private CookPanel[] cookPanels;
    private CookButton[] cookButtons;
    private ProgressImagesUI[] imagesUIs;
    private CookBookPanel[] cookBookPanels;
    private AnchorsManager anchorsManager;
    private Castle[] castles;

    private void Start()
    {
        anchorsManager = FindObjectOfType<AnchorsManager>(true);
        anchorsManager.ForStart();
        cookPanels = FindObjectsOfType<CookPanel>(true);
        for (int i = 0; i < cookPanels.Length; i++)
        {
            cookPanels[i].ForStart();
        }
        cookButtons = FindObjectsOfType<CookButton>(true);
        for (int i = 0; i < cookButtons.Length; i++)
        {
            cookButtons[i].ForStart();
        }
        cookBookPanels = FindObjectsOfType<CookBookPanel>(true);
        for (int i = 0; i < cookBookPanels.Length; i++)
        {
            cookBookPanels[i].ForStart();
        }
        imagesUIs = FindObjectsOfType<ProgressImagesUI>(true);
        for (int i = 0; i < imagesUIs.Length; i++)
        {
            imagesUIs[i].ForStart();
        }
        castles = FindObjectsOfType<Castle>(true);
        for (int i = 0; i < castles.Length; i++)
        {
            castles[i].ForStart();
        }
        
        

    }
}
