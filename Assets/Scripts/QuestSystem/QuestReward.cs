using System;
using UnityEngine;

public class QuestReward : MonoBehaviour
{
    private GameObject _scriptsHere;
    private Transform _tablesParent;
    private Transform _cookingParent;
    private Transform[] _tablesArray;
    private Transform[] _cookingArray;
    public static event Action<int> OnUpgradesAppliances;
    public static event Action OnUnlockedDishes;
    // Start is called before the first frame update
    public virtual void Start()
    {
        _scriptsHere = GameObject.FindGameObjectWithTag("ScriptsHere");
        _tablesParent = GameObject.FindGameObjectWithTag("Tables").transform;
        _cookingParent = GameObject.FindGameObjectWithTag("Cooking").transform;
        AutoFill(TargetToUnlock.tables, _tablesParent);
        AutoFill(TargetToUnlock.cooking, _cookingParent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AutoFill(TargetToUnlock targetToUnlock, Transform arrayParrent)
    {
        Transform[] tempArray;
        int _totalItems = arrayParrent.childCount;
        tempArray = new Transform[_totalItems];

        for (int i = 0; i < _totalItems; i++)
        {
            tempArray[i] = arrayParrent.GetChild(i);
            
        }
        switch (targetToUnlock)
        {
            case TargetToUnlock.tables:
                _tablesArray = tempArray;
                break;
            case TargetToUnlock.cooking:
                _cookingArray = tempArray;
                break;
            default:
                break;
        }
    }    
    /// <summary>
    /// Метод, открывающий улучшения техники
    /// </summary>
    /// <param name="UpgradeLevel">уровень, который открывается</param>
    public void UnlockUpgradesAppliances(int UpgradeLevel)
    {
        OnUpgradesAppliances?.Invoke(UpgradeLevel);
    }

    /// <summary>
    /// Метод для добавления опыта за выполнение квеста
    /// </summary>
    /// <param name="value">количество опыта</param>
    public void AddExp(FromFraction fromFraction, int value)
    {
        if (_scriptsHere.TryGetComponent(out IChangeInFraction changeInFraction))
            changeInFraction.ChangeReputation(fromFraction, value);
    }

    /// <summary>
    /// Метод для добавления денег за выполнение квеста
    /// </summary>
    /// <param name="value">количество денег</param>
    public void AddMoney(int value)
    {
        if (_scriptsHere.TryGetComponent(out Ipay ipay))
            ipay.ChangeBalance(value);
    }


    /// <summary>
    /// Метод, открывающий блюда
    /// </summary>
    /// <param name="cookingPlace">блюда какой техники</param>
    public void UnlockDishes(CookingPlaceEnum cookingPlace)
    {
        if (_scriptsHere.TryGetComponent(out ICookBook cookBook))
            cookBook.UnlockDishes(cookingPlace);
        OnUnlockedDishes?.Invoke();
    }

    /// <summary>
    /// Метод для открывания возможности покупки столов
    /// </summary>
    /// <param name="value">количество открываемых элементов</param>
    public void UnlockTables(int value)
    {        
        int count = value;
        for (int i = 0; i < _tablesArray.Length; i++)
        {
            if (count > 0)
            {
                if (_tablesArray[i].TryGetComponent(out IUnlocker unlocker)
                    && unlocker.CheckUnlockStatus() == StateEnume.castle)
                {
                    unlocker.CheckForChangeIcon();
                    count--;
                }                
            }
        }
    }

    /// <summary>
    /// Метод для открывания возможности покупки техники
    /// </summary>
    /// <param name="value">порядковый номер объекта в иерархии</param>
    public void UnlockCooking(int value)
    {
        if (_cookingArray[value].TryGetComponent(out IUnlocker unlocker))
        {
            unlocker.CheckForChangeIcon();
        }
    }

}

public enum TargetToUnlock
{
    tables,
    cooking
}
