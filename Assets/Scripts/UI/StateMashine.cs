using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMashine : MonoBehaviour
{
    [HideInInspector]
    public enum StateType
    {
        game,
        fryerCook,
        grillCook,
        ovenCook,
        stoveCook,
        manager,
        cookBook,
        quest,
        questReward,
        questTasks,
        reputation,
        tutorial
    }
    [Header("Окна для готовки")]
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _ovenCookScreen;
    [SerializeField] private GameObject _stoveCookScreen;
    [SerializeField] private GameObject _grillCookScreen;
    [SerializeField] private GameObject _fryerCookScreen;

    [Header("Окна для персонала")]
    [SerializeField] private GameObject _managerScreen;

    [Header("Окно книги рецептов")]
    [SerializeField] private GameObject _cookBookScreen;

    [Header("Окна квестов")]
    [SerializeField] private GameObject _questScreen;
    [SerializeField] private GameObject _questRewardScreen;
    [SerializeField] private GameObject _questTasksScreen;

    [Header("Окно репутации")]
    [SerializeField] private GameObject _reputationScreen;
    [SerializeField] private GameObject _tutorialScreen;


    private GameObject _currentScreen;
    void Start()
    {
        _stoveCookScreen.SetActive(false);
        _ovenCookScreen.SetActive(false);
        _grillCookScreen.SetActive(false);
        _fryerCookScreen.SetActive(false);
        _managerScreen.SetActive(false);
        _cookBookScreen.SetActive(false);
        _questScreen.SetActive(false);
        _questRewardScreen.SetActive(false);
        _questTasksScreen.SetActive(false);
        _reputationScreen.SetActive(false);
        _tutorialScreen.SetActive(false);

        _currentScreen = _gameScreen;
    }

    public void ChangeStates(StateType state)
    {
        if (_currentScreen == null)
        {
            return;
        }
        _gameScreen.SetActive(true);
        if (_currentScreen != _gameScreen)
            _currentScreen.SetActive(false);

        switch(state)
            {
            case StateType.game:
                _gameScreen.SetActive(true);
                _currentScreen = _gameScreen;
                break;
            case StateType.stoveCook:
                _stoveCookScreen.SetActive(true);
                _currentScreen = _stoveCookScreen;
                break;
            case StateType.ovenCook:
                _ovenCookScreen.SetActive(true);
                _currentScreen = _ovenCookScreen;
                break;
            case StateType.grillCook:
                _grillCookScreen.SetActive(true);
                _currentScreen = _grillCookScreen;
                break;
            case StateType.fryerCook:
                _fryerCookScreen.SetActive(true);
                _currentScreen = _fryerCookScreen;
                break;

            case StateType.manager:
                _managerScreen.SetActive(true);
                _currentScreen = _managerScreen;
                break;
            case StateType.cookBook:
                _cookBookScreen.SetActive(true);
                _currentScreen = _cookBookScreen;
                break;

            case StateType.quest:
                _questScreen.SetActive(true);
                _gameScreen.SetActive(false);
                _currentScreen = _questScreen;
                break;
            case StateType.questReward:
                _questRewardScreen.SetActive(true);
                _gameScreen.SetActive(false);
                _currentScreen = _questRewardScreen;
                break;
            case StateType.questTasks:
                _questTasksScreen.SetActive(true);
                _gameScreen.SetActive(false);
                _currentScreen = _questTasksScreen;
                break;

            case StateType.reputation:
                _reputationScreen.SetActive(true);
                _gameScreen.SetActive(false);
                _currentScreen = _reputationScreen;
                break;   
            
            case StateType.tutorial:
                _tutorialScreen.SetActive(true);
                _currentScreen = _tutorialScreen;
                break;
        }

    }
}
