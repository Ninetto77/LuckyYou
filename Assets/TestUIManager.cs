using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUIManager : MonoBehaviour
{
    [SerializeField] private Text _moneyText;

    // Start is called before the first frame update
    void Start()
    {
        Wallet.OnShowBalance += ShowBalance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowBalance(float value)
    {
        _moneyText.text = value.ToString();
    }


}
