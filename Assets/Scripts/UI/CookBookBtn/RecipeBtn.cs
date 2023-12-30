using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBtn : MonoBehaviour
{
    [SerializeField] private TypeOfCustomerEnum type;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        GameManager.Instance.ShowCookBookCanvas();
    }
}
