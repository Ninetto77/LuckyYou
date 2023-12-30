using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dollar : Interactable//, IUnlocker
{
    [Header("÷ена покупки предмета")]
    [SerializeField] private int cost = 50;
    [SerializeField] private Transform parent;
    [SerializeField] private Transform castle;

    public StateEnume state;

    private Transform hideCookingPlace;
    private GameObject _scriptsHere;

    public override void Start()
    {
        base.Start();
        _scriptsHere = GameObject.FindGameObjectWithTag("ScriptsHere");
        state = StateEnume.dollar;
        hideCookingPlace = parent.GetChild(0);
    }



    public override void Interact()
    {
        if (state == StateEnume.castle)
            return;

        if (_scriptsHere.TryGetComponent(out Ipay ipay))
        {
            if (ipay.IsBalanceValid(cost))
            {
                Debug.Log("TakeMyMoney");
                ipay.ChangeBalance(-cost);
                ShowCookingPlace();
                if (_scriptsHere.TryGetComponent(out IAnchorManager anchorManager))
                    anchorManager.AnchorBusySwitcher(gameObject.name, false);
            }

            else
            {
                GameManager.Instance.ShowError();
            }

        }
    }

    ///// <summary>
    ///// ћетод, провер€ющий, можно ли сменить знак замка на доллар
    ///// </summary>
    //public void CheckForChangeIcon()
    //{
    //    if (state == StateEnume.castle)
    //    {
    //        ChangeIconToDollar();
    //        if (_scriptsHere.TryGetComponent(out IAnchorManager anchorManager))
    //            anchorManager.AnchorBusySwitcher(tableName, false);
    //    }
    //}

    ///// <summary>
    ///// ћетод, мен€ющий знак замка на знак доллара
    ///// </summary>
    //private void ChangeIconToDollar()
    //{
    //    transform.gameObject.SetActive(true);
    //    Destroy(castle.gameObject);
    //    state = StateEnume.dollar;        
    //}

    /// <summary>
    /// ћетод, показываюший место готовки на сцене
    /// </summary>
    private void ShowCookingPlace()
    {
        hideCookingPlace.gameObject.SetActive(true);

        hideCookingPlace.parent = null;
        Destroy(parent.gameObject);
        Destroy(this.gameObject);
    }

    ///// <summary>
    ///// ћетод, показывающий только знак замка
    ///// </summary>
    //private void HideCookingPlace()
    //{
    //    if (_scriptsHere.TryGetComponent(out IAnchorManager anchorManager))
    //        anchorManager.AnchorBusySwitcher(tableName, true);
    //    hideCookingPlace.gameObject.SetActive(false);
    //    transform.gameObject.SetActive(false);
    //    castle.gameObject.SetActive(true);

    //}

    //public StateEnume CheckUnlockStatus()
    //{
    //    return state;
    //}
}

public enum StateEnume
{
    castle, 
    dollar
}