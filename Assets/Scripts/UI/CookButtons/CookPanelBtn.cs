using UnityEngine;
using UnityEngine.UI;
using static StateMashine;

public class CookPanelBtn : MonoBehaviour
{
    [SerializeField] private StateType type;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        GameManager.Instance.ShowCanvas(type);
    }
}
