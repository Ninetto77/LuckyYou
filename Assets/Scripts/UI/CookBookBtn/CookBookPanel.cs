using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookBookPanel : MonoBehaviour
{
    public static Action<TypeOfCustomerEnum> OnChangeFractionList;
    public Sprite[] icons = new Sprite[4];

    private CookBook _cookBook;
    private GameObject _scriptsHere;
    private CookBookSlot[] CookingBookSlots;
    private Dish[] _dishes;
    private string[] _nameText = new string[4];


    public void ForStart()
    {
        _scriptsHere = GameObject.FindGameObjectWithTag("ScriptsHere");
        _cookBook = _scriptsHere.GetComponent<CookBook>();
        _dishes = _cookBook.AllMenu();
        CookingBookSlots = GetComponentsInChildren<CookBookSlot>();
        OnChangeFractionList += GetIconsAndText;

        Initialize();
    }

    public void Initialize()
    {
        GetIconsAndText(TypeOfCustomerEnum.student);

        SetIconAndTxt();
    }

    /// <summary>
    /// Метод установления иконок и названия блюд
    /// </summary>
    public void SetIconAndTxt()
    {
        for (int i = 0; i < CookingBookSlots.Length; i++)
        {
            Button btn = CookingBookSlots[i].GetComponentInChildren<Button>();
            Image spr = btn.transform.GetChild(0).GetComponent<Image>();
            spr.sprite = icons[i];

            Text txt = btn.gameObject.GetComponentInChildren<Text>();
            txt.text = _nameText[i];
        }
    }

    /// <summary>
    /// Метод вывода иконок и названия блюда
    /// </summary>
    /// <param name="type"></param>
    public void GetIconsAndText(TypeOfCustomerEnum type)
    {
        switch (type)
        {
            case TypeOfCustomerEnum.student:
                ChooseIconAndText(0, 4);
                break;
            case TypeOfCustomerEnum.family:
                ChooseIconAndText(4, 8);
                break;
            case TypeOfCustomerEnum.mafia:
                ChooseIconAndText(8, 12);
                break;
            case TypeOfCustomerEnum.bogema:
                ChooseIconAndText(12, 16);
                break;
        }
        SetIconAndTxt();

    }

    /// <summary>
    /// Метод выбора иконок и текста блюд
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    private void ChooseIconAndText(int min, int max)
    {
        int j = 0;
        for (int i = min; i < max; i++)
        {
            if (_dishes[i].isLocked)
            {
                icons[j] = _dishes[i].HideIcon;
                _nameText[j] = "";
            }
            else
            {
                icons[j] = _dishes[i].Icon;
                _nameText[j] = _dishes[i].DishName;
            }
            j++;
        }
    }
}
public enum TypeOfCustomerEnum
{
    student,
    family,
    mafia,
    bogema
}