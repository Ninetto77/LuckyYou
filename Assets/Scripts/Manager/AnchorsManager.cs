using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorsManager : MonoBehaviour, IAnchorManager
{
    [SerializeField] private Transform _busyAnchorParent;
    private string[] _busyAnchors;

    // Start is called before the first frame update
    public void ForStart()
    {
        AutoFillArray();
    }

    private void AutoFillArray()
    {
        int _totalItems = _busyAnchorParent.childCount;
        _busyAnchors = new string[_totalItems];

        for (int i = 0; i < _totalItems; i++)
        {
            _busyAnchors[i] = $"{_busyAnchorParent.GetChild(i).name}";
        }
    }
    /// <summary>
    /// Метод для проверки занятости якоря. 0 - якорь свободен, 1 - якорь занят, 2 - якорь не найден
    /// </summary>
    /// <param name="anchorName"></param>
    /// <returns></returns>
    public int CheckAnchorBusy(string anchorName)
    {
        int value = 2;  // 0 - якорь свободен, 1 - якорь занят, 2 - якорь не найден
        for (int i = 0; i < _busyAnchors.Length; i++)
        {
            if (_busyAnchors[i] == $"{anchorName}Busy")
                value = 1;
            if (_busyAnchors[i] == $"{anchorName}")
                value = 0;
        }
        return value;
    }

    public void AnchorBusySwitcher(string anchorName, bool value)
    {        
        if (_busyAnchors != null) 
        {
            for (int i = 0; i < _busyAnchors.Length; i++)
            {
                if (value)
                {
                    if (_busyAnchors[i] == anchorName)
                    {
                        _busyAnchors[i] = $"{anchorName}Busy";
                        break;
                    }                                          
                }
                else
                {
                    if (_busyAnchors[i] == $"{anchorName}Busy")
                    {
                        _busyAnchors[i] = anchorName;
                        break;
                    }                        
                }                
            }                
        }
    }

    public string[] FreePlaces(string anchorNamePart)
    {
        int c = 0;
        for (int i = 0; i < _busyAnchors.Length; i++)
        {
            for (int j = 0; j < _busyAnchors.Length; j++)
            {
                if(_busyAnchors[i] == $"{anchorNamePart}{j+1}")
                {
                    c++;
                }
            }            
        }

        int k = 0;
        string[] freePlaces = new string[c];
        for (int i = 0; i < _busyAnchors.Length; i++)
        {
            for (int j = 0; j < _busyAnchors.Length; j++)
            {
                if (_busyAnchors[i] == $"{anchorNamePart}{j + 1}")
                {
                    freePlaces[k] = _busyAnchors[i];
                    k++;
                    
                }                
            }            
        }
        return freePlaces;
    }
}
