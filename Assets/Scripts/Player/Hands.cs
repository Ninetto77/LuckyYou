using System;
using UnityEngine;

public class Hands : MonoBehaviour, IGiveOrder
{
    private string _dishInHands = null;
    [SerializeField] private GameObject _tray;


    private void Start()
    {
        _tray.SetActive(false);
    }

    public bool TakeDish(string dishName)
    {        
        if (_dishInHands == null)
        {
            if (TryGetComponent(out IAnimator animator))
                animator.SetBool("Carry", true);
            _tray.SetActive(true);
            _dishInHands = dishName;
            return true;
        }
        else return false;
    }

    public string CheckDishInHands()
    {
        if (_dishInHands != null)
        {
            return _dishInHands;
        }
        else return null;
    }

    public void GiveDish()
    {
        if (_dishInHands != null)
        {
            if (TryGetComponent(out IAnimator animator))
                animator.SetBool("Carry", false);
            _tray.SetActive(false);
            _dishInHands = null;
        }
    }
}
