using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static StateMashine;

public class UIManager : MonoBehaviour
{
    #region Синглтон
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else return;
    }
    #endregion

    [Header("Фритюр")]
    public GameObject FryerCookPanel;
    public GameObject FryerUpgratePanel;

    [Header("Гриль")]
    public GameObject GrillCookPanel;
    public GameObject GrillUpgratePanel;

    [Header("Духовка")]
    public GameObject OvenCookPanel;
    public GameObject OvenUpgratePanel;

    [Header("Плита")]
    public GameObject StoveCookPanel;
    public GameObject StoveUpgratePanel;

    [Header("Текст")]
    public Text ErrorText;

    private StateMashine stateMashine;

    private void Start()
    {
        stateMashine = GetComponent<StateMashine>();
        ErrorText.text = "";
    }

    public void ShowCookPanel(StateType type)
    {
        switch (type)
        {
            case StateType.ovenCook:
                stateMashine.ChangeStates(StateType.ovenCook);
                OvenCookPanel.SetActive(true);
                OvenUpgratePanel.SetActive(false);
                break;
            case StateType.stoveCook:
                stateMashine.ChangeStates(StateType.stoveCook);
                StoveCookPanel.SetActive(true);
                StoveUpgratePanel.SetActive(false);
                break;
            case StateType.grillCook:
                stateMashine.ChangeStates(StateType.grillCook);
                GrillCookPanel.SetActive(true);
                GrillUpgratePanel.SetActive(false);
                break;
            case StateType.fryerCook:
                stateMashine.ChangeStates(StateType.fryerCook);
                FryerCookPanel.SetActive(true);
                FryerUpgratePanel.SetActive(false);
                break;
        }
    }

    public void ShowUpgratePanel(StateType type)
    {
        switch (type)
        {
            case StateType.ovenCook:
                stateMashine.ChangeStates(StateType.ovenCook);
                OvenCookPanel.SetActive(false);
                OvenUpgratePanel.SetActive(true);
                break;
            case StateType.stoveCook:
                stateMashine.ChangeStates(StateType.stoveCook);
                StoveCookPanel.SetActive(false);
                StoveUpgratePanel.SetActive(true);
                break;
            case StateType.grillCook:
                stateMashine.ChangeStates(StateType.grillCook);
                GrillCookPanel.SetActive(false);
                GrillUpgratePanel.SetActive(true);
                break;
            case StateType.fryerCook:
                stateMashine.ChangeStates(StateType.fryerCook);
                FryerCookPanel.SetActive(false);
                FryerUpgratePanel.SetActive(true);
                break;
        }
    }


    public void ShowManagerCanvas()
    {
        stateMashine.ChangeStates(StateType.manager);
    }

    public void ShowCookBookCanvas()
    {
        stateMashine.ChangeStates(StateType.cookBook);
    }

    public void ShowQuestCanvas()
    {
        stateMashine.ChangeStates(StateType.quest);
        Time.timeScale = 0;
    }

    public void ShowQuestRewardanvas()
    {
        stateMashine.ChangeStates(StateType.questReward);
        Time.timeScale = 0;
    }

    public void ShowQuestTasksCanvas()
    {
        stateMashine.ChangeStates(StateType.questTasks);
        Time.timeScale = 0;
    }
      
    public void ShowReputationCanvas()   
    {
        stateMashine.ChangeStates(StateType.reputation);
        Time.timeScale = 0;
    }    
    public void ShowTutorialCanvas()
    {
        stateMashine.ChangeStates(StateType.tutorial);
        Time.timeScale = 0;
    }

    public void ShowErrorText()
    {
        StartCoroutine(ChangeErrorText());
    }

    //public void ShowTutorial()
    //{
    //    StartCoroutine(ChangeErrorText());
    //}

    public IEnumerator ChangeErrorText()
    {
        ErrorText.text = "Недостаточно средств";
        yield return new WaitForSeconds(3f);
        ErrorText.text = "";
    }

    public void HideAllCanvas()
    {
        stateMashine.ChangeStates(StateType.game);
        Time.timeScale = 1;
    }
}
