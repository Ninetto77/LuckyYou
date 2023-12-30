using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;


public class EmojiController : MonoBehaviour
{
    private Canvas _emojiCanvas;
    private Transform _emojiTransform;
    private Transform _cameraTransform;
    

    // Эмодзи
    //[SerializeField] private GameObject _wantToOrder;
    private Image _cloudImg;
    private Image _wishIcon;

    void Start()
    {
        _emojiCanvas = transform.GetChild(1).GetComponentInChildren<Canvas>();
        _cloudImg = _emojiCanvas.transform.GetChild(0).GetComponentInChildren<Image>();
        _wishIcon = _cloudImg.transform.GetChild(0).GetComponentInChildren<Image>();
        _emojiCanvas.worldCamera = Camera.main;
        _emojiTransform = _emojiCanvas.transform;
        _cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        _emojiTransform.forward = _cameraTransform.forward;
    }

    /// <summary>
    /// Метод изменения иконки желаемого блюда
    /// </summary>
    /// <param name="value"></param>
    /// <param name="dish"></param>
    public void WaitOrder(bool value, Dish dish)
    {
        _cloudImg.enabled = value;

        if(value) _wishIcon.sprite = dish.WishIcon;
        _wishIcon.enabled = value;

    }

    public void QuestIcon(bool value)
    {
        _cloudImg.enabled = value;
    }
}
