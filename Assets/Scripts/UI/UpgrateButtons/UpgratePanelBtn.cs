using UnityEngine;
using UnityEngine.UI;
using static StateMashine;

public class UpgratePanelBtn : MonoBehaviour
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
        GameManager.Instance.ShowUpgrateCanvas(type);
    }
}
