using UnityEngine;
using UnityEngine.UI;

public class AcceptBtn : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        GameManager.Instance.HideCanvas();
    }
}
