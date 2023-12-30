using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseReputation : MonoBehaviour, IChangeBaseRep
{
    [SerializeField] private ProgressSliderUI _progressUI;
    [SerializeField] private float _baseReputationMax;
    [SerializeField] private TMP_Text _levelText;
    public float baseReputation = 10;
    public int baseReputationLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        ForStart();
    }

    private void ForStart()
    {
        if (_progressUI != null) _progressUI.FillProgress(baseReputation / _baseReputationMax);
    }

    public void ChangeBaseRep(float value)
    {
        if (value > 0)
            baseReputation = baseReputation + value * baseReputationLevel;
        else if (value < 0 && baseReputationLevel > 1)
            baseReputation = baseReputation + value * baseReputationLevel * 1.25f;
        else baseReputation += value;

        if (baseReputation < 0 && baseReputationLevel > 1)
        {
            baseReputation = _baseReputationMax;
            baseReputationLevel--;
            _levelText.text = baseReputationLevel.ToString();
        }
        else if (baseReputation < 0 && baseReputationLevel == 1)
        {
            baseReputation = 0;
        }

        if (baseReputation >= _baseReputationMax)
        {
            baseReputationLevel++;
            _levelText.text = baseReputationLevel.ToString();
            baseReputation = 0;
        }

        if (baseReputationLevel > 3)
            baseReputationLevel = 3;



        if (_progressUI != null) _progressUI.FillProgress(baseReputation / _baseReputationMax);
    }

    public int CheckBaseRepLevel()
    {
        return baseReputationLevel;
    }
}
