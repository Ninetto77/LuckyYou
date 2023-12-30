using System;
using UnityEngine;
using Random = System.Random;
[RequireComponent(typeof(CookBook))]
public class DishRandomizer : MonoBehaviour, IChoose
{
    private CookBook _cookBook;
    //public static event Action<string> OnHaveOrder;
    [SerializeField] private int _fractionDishChance;

    private void Start()
    {
        ForStart();
    }
    private void Update()
    {
       
    }
    private void ForStart()
    {
        _cookBook = GetComponent<CookBook>();
    }

    /// <summary>
    /// Метод для случайного выбора доступного блюда
    /// </summary>
    /// <param name="fractionName">Имя фракции посетителя</param>
    /// <returns></returns>
    public Dish SelectDish(FromFraction fractionName)
    {
        Dish selectedDish = null;
        Dish[] availableDishes = _cookBook.AvailableDishes();        
        // Проверяем, есть ли топовое блюдо фракции и определяем totalChance
        int totalChance = 0;
        int maxLevelFavoriteDish = 0;
        int hasFavoriteDish = 0;
        for (int i = 0; i < availableDishes.Length; i++)
        {
            if (availableDishes[i].FractionName == fractionName && availableDishes[i].Level > maxLevelFavoriteDish)
            {
                if (totalChance == 0) totalChance += _fractionDishChance;
                if (hasFavoriteDish < 1) hasFavoriteDish = 1;
                maxLevelFavoriteDish = availableDishes[i].Level;
            }
        }

        for (int i = 0; i < availableDishes.Length - hasFavoriteDish; i++)
        {
            if (maxLevelFavoriteDish > 0)
                totalChance += Mathf.CeilToInt((100 - _fractionDishChance) / ((float)availableDishes.Length - 1));
            else
                totalChance += Mathf.CeilToInt(100 / ((float)availableDishes.Length));
        }        
        
        // Делаем выбор
        int limit;
        int chanceValue = Randomizer(1, totalChance);
        for (int i = 0; i < availableDishes.Length; i++)
        {
            if (availableDishes[i].FractionName == fractionName && availableDishes[i].Level == maxLevelFavoriteDish)
            {
                limit = 60;
            }
            else if (maxLevelFavoriteDish > 0)
                limit = Mathf.CeilToInt((100 - _fractionDishChance) / ((float)availableDishes.Length - 1));
            else 
                limit = Mathf.CeilToInt(100 / ((float)availableDishes.Length));

            if (chanceValue >= (totalChance - limit) && chanceValue < totalChance)
            {
                selectedDish = availableDishes[i];
            }
            totalChance -= limit;
        }
        //OnHaveOrder?.Invoke(selectedDish.DishName);
        Debug.Log(selectedDish.DishName);
        return selectedDish;
    }

    private int Randomizer(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
}
