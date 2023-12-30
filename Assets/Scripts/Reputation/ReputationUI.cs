using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReputationUI : MonoBehaviour
{
    [SerializeField] private Image _blue;    //Students
    [SerializeField] private Image _green;   //Family
    [SerializeField] private Image _red;     //Mafia
    [SerializeField] private Image _white;   //Bohemia
    // Start is called before the first frame update
    void Start()
    {
        FractionCustomizer.OnReputationChanged += UpdateUI;
    }
    private void Update()
    {

    }

    private void UpdateUI(FromFraction fromFraction, float value)
    {
        switch (fromFraction)
        {
            case FromFraction.Students:
                _blue.fillAmount = Calculate(value);
                break;
            case FromFraction.Family:
                _green.fillAmount = Calculate(value);
                break;
            case FromFraction.Mafia:
                _red.fillAmount = Calculate(value);
                break;
            case FromFraction.Bohemia:
                _white.fillAmount = Calculate(value);
                break;
            default:
                break;
        }
    }

    private float Calculate(float value)
    {
        if (value <= 5) return value * 0.072f;
        else if (value > 5 && value <= 15) return value * 0.038f;
        else if (value > 15 && value <= 35) return value * 0.022f;
        else if (value > 35 && value <= 75) return value * 0.013f;
        else return value;
    }
}
