using System;
using UnityEngine;
using UnityEngine.XR;


public class CookingPlace : Interactable
{
    public static Action<int, CookingPlaceEnum> OnChangeLevel;
    public static Action<int, CookingPlaceEnum> OnChangeBonus;
    public static Action<int, CookingPlaceEnum> OnChangeReputation;

    public static event Action<int, int, int, CookingPlaceEnum> OnShowCookPlaceChange;

    // сделать прайвет  - оставлю для слежения
    [SerializeField] private UpgrateDescription[] upgrateDescriptions;
    [SerializeField] private UpgrateBonus[] upgrateBonuses;
    [SerializeField] private UpgrateReputation[] upgrateReputations;

    private int curUpLevel;
    private int curBonusesLevel;
    private int curReputationLevel;

    [Header("Место готовки")]
    [SerializeField] private CookingPlaceEnum CookPlace;

    private GameObject _scriptsHere;
    private CookBook _cookBook;
    private Dish[] _dishes;
    private bool isFirst = false;

    public override void Start()
    {
        base.Start();
        _scriptsHere = GameObject.FindGameObjectWithTag("ScriptsHere");
        _cookBook = _scriptsHere.GetComponent<CookBook>();
        _dishes = _cookBook.AllMenu();
        Initialized();
    }

    private void Initialized()
    {
        curUpLevel = int.Parse( upgrateDescriptions[0].Level);
        curBonusesLevel = int.Parse(upgrateBonuses[0].Level);
        curReputationLevel = int.Parse(upgrateReputations[0].Level);
    }

    public override void Interact()
    {
        base.Interact();

        switch (CookPlace)
        {
            case CookingPlaceEnum.fryer:
                GameManager.Instance.ShowCanvas(StateMashine.StateType.fryerCook);
                break;
            case CookingPlaceEnum.grill:
                GameManager.Instance.ShowCanvas(StateMashine.StateType.grillCook);
                break;
            case CookingPlaceEnum.oven:
                if (isFirst == false)
                {
                    GameManager.Instance.ShowTutorial(3);
                    isFirst = true;
                }
                else GameManager.Instance.ShowCanvas(StateMashine.StateType.ovenCook);
                break;
            case CookingPlaceEnum.stove:
                GameManager.Instance.ShowCanvas(StateMashine.StateType.stoveCook);
                break;

        }
        
    }
    /// <summary>
    /// Метод апгрейта общего уровня - скорости готовки
    /// </summary>
    /// <param name="levelUp"></param>
    public void UpgradeLevel()
    {
        if (curUpLevel == 3)
            return;
        int cost = int.Parse(upgrateDescriptions[curUpLevel].Cost);
        if (_scriptsHere.TryGetComponent(out Ipay ipay))
        {
            if (ipay.IsBalanceValid(cost))
            {
                ipay.ChangeBalance(-cost);                
                ChangeLevel();
                curUpLevel++;
                OnShowCookPlaceChange?.Invoke(curUpLevel, curBonusesLevel, curReputationLevel, CookPlace);
            }
        }
    }
    /// <summary>
    /// Метод апгрейта бонуса
    /// </summary>
    /// <param name="levelUp"></param>
    public void UpgradeBonus()
    {
        if (curBonusesLevel == 3)
            return;
        int cost = int.Parse(upgrateBonuses[curBonusesLevel + 1].Cost);
        if (_scriptsHere.TryGetComponent(out Ipay ipay))
        {
            if (ipay.IsBalanceValid(cost))
            {
                ipay.ChangeBalance(-cost);
                curBonusesLevel++;
                ChangeBonus();
                OnShowCookPlaceChange?.Invoke(curUpLevel, curBonusesLevel, curReputationLevel, CookPlace);
            }
        }
    }
    /// <summary>
    /// Метод апгрейта репутации
    /// </summary>
    /// <param name="levelUp"></param>
    public void UpgradeReputationl()
    {
        if (curReputationLevel == 3)
            return;
        int cost = int.Parse(upgrateReputations[curReputationLevel + 1].Cost);
        if (_scriptsHere.TryGetComponent(out Ipay ipay))
        {
            if (ipay.IsBalanceValid(cost))
            {
                ipay.ChangeBalance(-cost);
                curReputationLevel++;
                ChangeReputation();
                OnShowCookPlaceChange?.Invoke(curUpLevel, curBonusesLevel, curReputationLevel, CookPlace);
            }
        }
    }

    /// <summary>
    /// Метод изменения уровня
    /// </summary>
    private void ChangeLevel()
    {        
        OnChangeLevel?.Invoke(int.Parse(upgrateDescriptions[curUpLevel].Speed), CookPlace);
        //foreach (Dish dish in _dishes)
        //{
        //    if (dish.CookingPlace == CookPlace)
        //    {
        //        dish.CookingTime--;
        //    }
        //}
    }

    /// <summary>
    /// Метод изменения бонуса
    /// </summary>
    public void ChangeBonus()
    {
        OnChangeBonus?.Invoke(int.Parse(upgrateBonuses[curBonusesLevel].Bonus), CookPlace);
    }

    /// <summary>
    /// Метод изменения репутации
    /// </summary>
    public void ChangeReputation()
    {
        OnChangeReputation?.Invoke(int.Parse(upgrateReputations[curReputationLevel].Reputation), CookPlace);
    }
}

public enum CookingPlaceEnum
{
    fryer,
    grill,
    oven,
    stove
}
