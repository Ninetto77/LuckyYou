using System;
using UnityEngine;

public class CookBook : MonoBehaviour, ICookBook
{
    [SerializeField] private Dish[] _dishes;
    [SerializeField] private GameObject[] cookingPlaces;
   
    public void UnlockDishes(CookingPlaceEnum cookingPlaceEnum)
    {
        for (int i = 0; i < _dishes.Length; i++)
        {
            if (_dishes[i].CookingPlace == cookingPlaceEnum)
            {
                _dishes[i].isLocked = false;
            }
        }
    }

    public Dish DishRecipe(string dishName)
    {
        Dish f = Array.Find(_dishes, dish => dish.DishName == dishName);

        if (f == null)
        {
            Debug.LogWarning($"Fraction {dishName} not found");
        }
        return f;
    }

    public Dish[] AllMenu()
    {
        return _dishes;
    }

    public Dish[] AvailableDishes()
    {
        
        int c = 0;
        for (int i = 0; i < _dishes.Length; i++)
        {
            if (_dishes[i].isLocked == false && _dishes[i].Level <= CheckCookingPlace())            
                c++;                          
        }
        Dish[] f = new Dish[c];
        int k = 0;
        for (int i = 0; i < _dishes.Length; i++)
        {
            if (_dishes[i].isLocked == false && _dishes[i].Level <= CheckCookingPlace())
            {
                f[k] = _dishes[i];
                k++;                
            }
        }       
        return f;
    }

    private int CheckCookingPlace()
    {
        int level = 0;
        for (int i = 0; i < cookingPlaces.Length; i++)
        {
            if (cookingPlaces[i].activeSelf) level = i + 1;
        }
        return level;
    }
}
