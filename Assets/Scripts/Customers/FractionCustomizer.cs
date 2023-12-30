using System;
using UnityEngine;

public class FractionCustomizer : MonoBehaviour, IChangeInFraction
{
    [SerializeField] protected Fraction[] _fraction;
    public static event Action<FromFraction, float> OnReputationChanged;

    private void Start()
    {

    }
    public Fraction FractionSettings(FromFraction fromFraction)
    {
        Fraction f = Array.Find(_fraction, fraction => fraction.IsFraction == fromFraction);
        
        if (f == null) 
        {
            Debug.LogWarning($"Fraction {fromFraction} not found");
        }
        return f;
    }

    public Fraction[] AllFractions()
    {
        return _fraction;
    }

    public void ChangeReputation(FromFraction fromFraction, float value)
    {
        Fraction f = Array.Find(_fraction, fraction => fraction.IsFraction == fromFraction);
        if (f == null)
        {
            Debug.LogWarning($"Fraction {fromFraction} not found");
        }
        f.Reputation += value;
        if (f.Reputation < 0) f.Reputation = 0;
        Debug.Log($"{f.FractionName} + {value} = {f.Reputation}");
        OnReputationChanged?.Invoke(f.IsFraction, f.Reputation);
    }

}
