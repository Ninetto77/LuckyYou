using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPls : MonoBehaviour, IWantToOrder
{
    public bool _CashRegisterBusy = false;
    public bool CashRegisterBusy()
    {
        return _CashRegisterBusy;
    }

    public void CashRegisterSwitch(bool value)
    {
        _CashRegisterBusy = value;
    }
}
