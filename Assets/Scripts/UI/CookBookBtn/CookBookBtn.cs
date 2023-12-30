using UnityEngine;
using UnityEngine.UI;
using static StateMashine;

public class CookBookBtn : MonoBehaviour
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
        CookBookPanel.OnChangeFractionList?.Invoke(type);
    }
}
