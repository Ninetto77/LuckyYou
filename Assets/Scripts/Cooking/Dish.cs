using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Dish
{
    public string DishName;            // �������� �����
    public int Level;                  // ��������� ������� �������
    public bool isLocked;              // ������� �� ����� � ������?
    public FromFraction FractionName;  // � ����� ������� ��� ����� �������?
    public float CookingTime;          // ����� ������������� �����
    public float Cost;                 // ������� ��������� �����
    public float Reputation;           // ������� ��������� �� �������������� �����
    public Sprite Icon;                // ������ �����
    public Sprite HideIcon;            // ������ �� ��������� �����
    public Sprite WishIcon;            // ������ ��������� ����� � �������
    public CookingPlaceEnum CookingPlace; // ����� �������������
}
