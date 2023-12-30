using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationManager : MonoBehaviour, IReputation
{
    [SerializeField] private float _fractionBaseRep = 1;
    private int _multiplier;
    private GameObject _scriptsHere;
    private float _ovenBonusRep = 0;
    private float _stoveBonusRep = 0;
    private float _grillBonusRep = 0;
    private float _fryerBonusRep = 0;

    private void Start()
    {
        ForStart();
    }
    public void ForStart()
    {
        _scriptsHere = GameObject.FindGameObjectWithTag("ScriptsHere");
        MultiplierUpdate();
        CookingPlace.OnChangeReputation += ChangeBonusRep;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void MultiplierUpdate()
    {
        if (_scriptsHere.TryGetComponent(out IChangeBaseRep changeBaseRep))
        {
            _multiplier = changeBaseRep.CheckBaseRepLevel();
        }
        
    }

    private void ChangeBonusRep(int value, CookingPlaceEnum cookingPlace)
    {
        switch (cookingPlace)
        {
            case CookingPlaceEnum.fryer:
                _fryerBonusRep = value;
                break;
            case CookingPlaceEnum.grill:
                _grillBonusRep = value;
                break;
            case CookingPlaceEnum.oven:
                _ovenBonusRep = value;                
                break;
            case CookingPlaceEnum.stove:
                _stoveBonusRep = value;
                break;
            default:
                break;
        }
    }

    public void AddReputation(Dish dish, Fraction fraction)
    {
        float curBonusRep = 0;
        switch (dish.CookingPlace)
        {
            case CookingPlaceEnum.fryer:
                curBonusRep = _fryerBonusRep;
                break;
            case CookingPlaceEnum.grill:
                curBonusRep = _grillBonusRep;
                break;
            case CookingPlaceEnum.oven:
                curBonusRep = _ovenBonusRep;
                break;
            case CookingPlaceEnum.stove:
                curBonusRep = _stoveBonusRep;
                break;
            default:
                break;
        }
        MultiplierUpdate();
        if (_scriptsHere.TryGetComponent(out IChangeInFraction changeInFraction))
            changeInFraction.ChangeReputation(fraction.IsFraction, _fractionBaseRep * _multiplier + curBonusRep);
        Debug.Log($"{fraction.IsFraction} - {fraction.Reputation}");
    }

    private void OnDestroy()
    {
        CookingPlace.OnChangeReputation -= ChangeBonusRep;
    }
}
