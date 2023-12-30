using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightTumbler : MonoBehaviour, ICashLight
{
    private Light _light;
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        LightOn(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightOn(bool value)
    {
        _light.enabled = value;
    }
}
