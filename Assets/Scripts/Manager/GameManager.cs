using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StateMashine;

public class GameManager : MonoBehaviour
{
    #region Синглтон
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else return;
    }
    #endregion
    [SerializeField] UIManager uiManager;
    [SerializeField] private Tutorial tutorial;
    public void ShowCanvas(StateType type)
    {
        uiManager.ShowCookPanel(type);
    }

    public void ShowUpgrateCanvas(StateType type)
    {
        uiManager.ShowUpgratePanel(type);
    }

    public void ShowCookBookCanvas()
    {
        uiManager.ShowCookBookCanvas();
    }
    public void ShowQuestCanvas()
    {
        uiManager.ShowQuestCanvas();
    }

    public void ShowQuestRewardCanvas()
    {
        uiManager.ShowQuestRewardanvas();
    }

    public void ShowError()
    {
        uiManager.ShowErrorText();
    }  
    public void ShowTutorial(int i)
    {
        if (tutorial.CanShowTutorial(i))
            uiManager.ShowTutorialCanvas();
    }

    public void HideCanvas()
    {
        uiManager.HideAllCanvas();
    }
}
