using System;
using Unity.VisualScripting;
using UnityEngine;

public class Cooking : MonoBehaviour, ICooking
{        
    private CookBook _book;    
    private ProgressImagesUI _progressImagesUI;
    private GameObject _player;
    private bool _cookInProgress = false;
    private float _cookingTime = 0;
    private float _cookingTimeMax = 5;
    private float _cookingTimeOven;
    private float _cookingTimeStove;
    private float _cookingTimeGrill;
    private float _cookingTimeFryer;
    [SerializeField] private float _cookingTimeBase = 5;
    public static event Action<string> OnPressedCooking;
    private CookingPlace[] _places;
    private GameObject _cookingObj;
    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        
    }

    void Start()
    {
        _book = GameObject.FindGameObjectWithTag("ScriptsHere").GetComponent<CookBook>();
        _player = GameObject.FindGameObjectWithTag("Player");
        //_places = _cookingObj.GetComponentsInChildren<CookingPlace>();
        Initialize();


    }
    
    void Update()
    {
        if (_cookingTime >= 0) _cookingTime -= Time.deltaTime;
        if (_cookInProgress && _cookingTime >= 0) _progressImagesUI.FillProgress(_cookingTime / _cookingTimeMax);
    }

    private void Initialize()
    {
        _cookingTimeOven = _cookingTimeBase;
        _cookingTimeStove = _cookingTimeBase;
        _cookingTimeGrill = _cookingTimeBase;
        _cookingTimeFryer = _cookingTimeBase;
    }

    public void ICookingExecute(string DishName, ProgressImagesUI progressImagesUI)
    {
        Dish _dish = _book.DishRecipe(DishName);
        _progressImagesUI = progressImagesUI;
        if (!_cookInProgress && _cookingTime <= 0)
        {
            OnPressedCooking?.Invoke(DishName);
            _cookingTimeMax = CalculateCookingSpeed($"{ _dish.CookingPlace}");
            _cookingTime = _cookingTimeMax;
            _cookInProgress = true;
            ButtonDetector();
        }
        if (_cookingTime <= 0 && _cookInProgress)
        {
            if (_player.TryGetComponent(out IGiveOrder giveOrder))
            {
                if(giveOrder.TakeDish(DishName))
                {                    
                    giveOrder.TakeDish(DishName);
                    _progressImagesUI.ProgressReset(0);
                    CookDone();
                }
            }            
        }
    }

    public void CookDone()
    {
        _cookInProgress = false;
        UnlockButtons();
    }

    private void ButtonDetector() 
    {
        _progressImagesUI.SwitchButton(false);        
    }

    public void UnlockButtons()
    {
        _progressImagesUI.SwitchButton(true);
    }

    private float CalculateCookingSpeed(string cookingPlace)
    {
        switch (cookingPlace)
        {
            case "oven":
                _cookingTimeMax = _cookingTimeOven;
                _audioManager.PlaySound("OvenAndFryer");
                break;
            case "stove":
                _cookingTimeMax = _cookingTimeStove;
                _audioManager.PlaySound("StoveAndGrill");
                break;
            case "grill":
                _cookingTimeMax = _cookingTimeGrill;
                _audioManager.PlaySound("StoveAndGrill");
                break;
            case "fryer":
                _cookingTimeMax = _cookingTimeFryer;
                _audioManager.PlaySound("OvenAndFryer");
                break;
            default:
                _cookingTimeMax = 1;
                break;
        }
        return _cookingTimeMax;
    }

    private void ChangeCookingSpeed(int value, CookingPlaceEnum cookingPlace)
    {
        switch (cookingPlace)
        {
            case CookingPlaceEnum.oven:
                _cookingTimeOven = value;
                break;
            case CookingPlaceEnum.stove:
                _cookingTimeStove = value;
                break;
            case CookingPlaceEnum.grill:
                _cookingTimeGrill = value;
                break;
            case CookingPlaceEnum.fryer:
                _cookingTimeFryer = value;
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        CookingPlace.OnChangeLevel -= ChangeCookingSpeed;
    }
}
