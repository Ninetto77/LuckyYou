using UnityEngine;

public class CheckPls : MonoBehaviour, ICheck
{    
    private float _ovenBonus = 0;
    private float _stoveBonus = 0;
    private float _grillBonus = 0;
    private float _fryerBonus = 0;

    private void Start()
    {
        ForStart();
    }
    public void ForStart()
    {
        CookingPlace.OnChangeBonus += ChangeBonus;
    }

    private void ChangeBonus(int value, CookingPlaceEnum cookingPlace)
    {
        switch (cookingPlace)
        {
            case CookingPlaceEnum.oven:
                _ovenBonus = value;
                break;
            case CookingPlaceEnum.stove:
                _stoveBonus = value;
                break;
            case CookingPlaceEnum.grill:
                _grillBonus = value;
                break;
            case CookingPlaceEnum.fryer:
                _fryerBonus = value;
                break;
            default:
                break;
        }
    }

    public void PayForOrder(Dish dish)
    {
        float totalPrice = 0;
        switch (dish.CookingPlace)
        {
            case CookingPlaceEnum.oven:
                totalPrice = dish.Cost + _ovenBonus;
                break; 
            case CookingPlaceEnum.stove:
                totalPrice = dish.Cost + _stoveBonus;
                break;
            case CookingPlaceEnum.grill:
                totalPrice = dish.Cost + _grillBonus;
                break;
            case CookingPlaceEnum.fryer:
                totalPrice = dish.Cost + _fryerBonus;
                break;
        }
        Debug.Log($"TotalPrice - {totalPrice}, Bonus - {totalPrice - dish.Cost}");
        if (gameObject.TryGetComponent(out Ipay ipay)) ipay.ChangeBalance(totalPrice);       
    }

    private void OnDestroy()
    {
        CookingPlace.OnChangeBonus -= ChangeBonus;
    }
}
