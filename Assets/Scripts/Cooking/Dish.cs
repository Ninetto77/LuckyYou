using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Dish
{
    public string DishName;            // Название блюда
    public int Level;                  // Требуемый уровень готовки
    public bool isLocked;              // Открыто ли блюдо к заказу?
    public FromFraction FractionName;  // У какой фракции это блюдо любимое?
    public float CookingTime;          // Время приготовления блюда
    public float Cost;                 // Базовая стоимость блюда
    public float Reputation;           // Базовая репутация за приготовленное блюдо
    public Sprite Icon;                // Иконка блюда
    public Sprite HideIcon;            // Иконка не открытого блюда
    public Sprite WishIcon;            // Иконка желаемого блюда у клиента
    public CookingPlaceEnum CookingPlace; // Место приготовления
}
