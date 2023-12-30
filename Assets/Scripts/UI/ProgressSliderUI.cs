using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressSliderUI : MonoBehaviour
{
    [SerializeField] private Slider _progressSlider;

    public void FillProgress(float value)
    {
        if (_progressSlider != null && value >= 0) 
        {
            _progressSlider.value =value;
        }
        
    }
}
