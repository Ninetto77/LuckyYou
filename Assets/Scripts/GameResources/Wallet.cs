using System;
using UnityEngine;

public class Wallet : MonoBehaviour, Ipay
{
    public float walletBalance = 0;
    public static event Action<float> OnShowBalance;
    public static event Action<string> OnShowError;

    private void Start()
    {
        OnShowBalance?.Invoke(walletBalance);
    }

    public void ChangeBalance(float value)
    {        
        if (walletBalance + value > 0)
        {            
            walletBalance += value;
            Debug.Log(walletBalance);
            OnShowBalance?.Invoke(walletBalance);
        }
        else
        {
            OnShowError?.Invoke("Недостаточно средств!");
        }
    }

    public bool IsBalanceValid(float value) 
    {
        if (walletBalance >= value)
        {
            return true;
        }
        else
        {
            OnShowError?.Invoke("Недостаточно средств!");
            return false;
        }
    }
}
