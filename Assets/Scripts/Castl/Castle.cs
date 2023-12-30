using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour, IUnlocker
{
    [SerializeField] private Transform parent;
    [SerializeField] private Transform castle;
    private GameObject _scriptsHere;
    public StateEnume state;

    private Transform hideCookingPlace;

    public void ForStart()
    {
        _scriptsHere = GameObject.FindGameObjectWithTag("ScriptsHere");
        state = StateEnume.castle;
        hideCookingPlace = parent.GetChild(0);
        HideCookingPlace();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// ћетод, провер€ющий, можно ли сменить знак замка на доллар
    /// </summary>
    public void CheckForChangeIcon()
    {
        if (state == StateEnume.castle)
        {            
            ChangeIconToDollar();            
        }
    }

    /// <summary>
    /// ћетод, мен€ющий знак замка на знак доллара
    /// </summary>
    private void ChangeIconToDollar()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        Destroy(castle.gameObject);
        state = StateEnume.dollar;
    }

    ///// <summary>
    ///// ћетод, показываюший место готовки на сцене
    ///// </summary>
    //private void ShowCookingPlace()
    //{
    //    hideCookingPlace.gameObject.SetActive(true);

    //    hideCookingPlace.parent = null;
    //    Destroy(parent.gameObject);
    //    Destroy(this.gameObject);
    //}

    /// <summary>
    /// ћетод, показывающий только знак замка
    /// </summary>
    private void HideCookingPlace()
    {
        if (_scriptsHere.TryGetComponent(out IAnchorManager anchorManager))
            anchorManager.AnchorBusySwitcher(gameObject.name, true);
        hideCookingPlace.gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        castle.gameObject.SetActive(true);
    }

    public StateEnume CheckUnlockStatus()
    {
        return state;
    }
}
