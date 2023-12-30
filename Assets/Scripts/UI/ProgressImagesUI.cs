using UnityEngine;
using UnityEngine.UI;

public class ProgressImagesUI : MonoBehaviour
{
    private GameObject _scriptsHere;
    public CookingPlaceEnum CookingPlace;
    private Image[] _images = new Image[4];
    private Button[] _buttons = new Button[4];
    private string[] _dishName = new string[4];
    private int _dishIndex;
    private Dish[] _allMenu;

    private Canvas _uiCanvas;
    private Image _progressBar;
    private Transform _uiCanvasTransform;
    private Transform _cameraTransform;
    private bool _cookInProgress = false;

    public void ForStart()
    {
        _scriptsHere = GameObject.FindGameObjectWithTag("ScriptsHere");
        AutoFillButtons();
        FillVariables();        
        if (_scriptsHere.TryGetComponent(out ICookBook cookBook))
            _allMenu = cookBook.AllMenu();
        Cooking.OnPressedCooking += DetectButton;

        if (_uiCanvas != null)
        {
            _uiCanvasTransform = _uiCanvas.transform;
            _cameraTransform = Camera.main.transform;
        }

        _progressBar.fillAmount = 0;
        QuestReward.OnUnlockedDishes += InteractableButtons;
        InteractableButtons();
    }

    private void Update()
    {
        if (_uiCanvas != null)
        {
            _uiCanvasTransform.forward = _cameraTransform.forward;
        }
    }

    private void FillVariables()
    {
        switch (CookingPlace)
        {
            case CookingPlaceEnum.fryer:
                _uiCanvas = GameObject.FindGameObjectWithTag("Fryer").GetComponentInChildren<Canvas>(true);
                _progressBar = _uiCanvas.GetComponentInChildren<Image>(true);
                break;
            case CookingPlaceEnum.grill:
                _uiCanvas = GameObject.FindGameObjectWithTag("Grill").GetComponentInChildren<Canvas>(true);
                _progressBar = _uiCanvas.GetComponentInChildren<Image>(true);
                break;
            case CookingPlaceEnum.oven:
                _uiCanvas = GameObject.FindGameObjectWithTag("Oven").GetComponentInChildren<Canvas>(true);
                _progressBar = _uiCanvas.GetComponentInChildren<Image>(true);
                break;
            case CookingPlaceEnum.stove:
                _uiCanvas = GameObject.FindGameObjectWithTag("Stove").GetComponentInChildren<Canvas>(true);
                _progressBar = _uiCanvas.GetComponentInChildren<Image>(true);
                break;
            default:
                break;
        }
    }

    private void AutoFillButtons()
    {
        CookSlot[] cookSlots = new CookSlot[4];
        cookSlots = GetComponentsInChildren<CookSlot>();
        for (int i = 0; i < cookSlots.Length; i++)
        {
            _buttons[i] = cookSlots[i].GetComponentInChildren<Button>();
            _images[i] = _buttons[i].transform.GetChild(1).GetComponent<Image>();
            _dishName[i] = _buttons[i].GetComponentInChildren<Text>().text;
        }
    }

    public void InteractableButtons()
    {
        if (!_cookInProgress)
        {
            for (int i = 0; i < _allMenu.Length; i++)
            {
                for (int j = 0; j < _dishName.Length; j++)
                {
                    if (_allMenu[i].DishName == _dishName[j] && _allMenu[i].isLocked)
                        _buttons[j].interactable = false;
                    else if (_allMenu[i].DishName == _dishName[j] && !_allMenu[i].isLocked)
                        _buttons[j].interactable = true;
                }
            }
        }        
    }

    public void FillProgress(float value)
    {
        _images[_dishIndex].fillAmount = 1 - value;
        _progressBar.fillAmount = 1 - value;
    }

    public void SwitchButton(bool value)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            if (i != _dishIndex)
            {
                _buttons[i].interactable = value;
                _cookInProgress = true;
            }
        }
        
    }

    public void ProgressReset(float value)
    {
        //_images[_dishIndex].fillAmount = value;
        _progressBar.fillAmount = value;
        _cookInProgress = false;
    }

    public void DetectButton(string dishName)
    {
        for (int i = 0; i < _images.Length; i++)
        {
            if (_dishName[i] == dishName)
                _dishIndex = i;
        }
    }
}
