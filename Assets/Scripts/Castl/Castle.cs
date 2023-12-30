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
    /// �����, �����������, ����� �� ������� ���� ����� �� ������
    /// </summary>
    public void CheckForChangeIcon()
    {
        if (state == StateEnume.castle)
        {            
            ChangeIconToDollar();            
        }
    }

    /// <summary>
    /// �����, �������� ���� ����� �� ���� �������
    /// </summary>
    private void ChangeIconToDollar()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        Destroy(castle.gameObject);
        state = StateEnume.dollar;
    }

    ///// <summary>
    ///// �����, ������������ ����� ������� �� �����
    ///// </summary>
    //private void ShowCookingPlace()
    //{
    //    hideCookingPlace.gameObject.SetActive(true);

    //    hideCookingPlace.parent = null;
    //    Destroy(parent.gameObject);
    //    Destroy(this.gameObject);
    //}

    /// <summary>
    /// �����, ������������ ������ ���� �����
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
