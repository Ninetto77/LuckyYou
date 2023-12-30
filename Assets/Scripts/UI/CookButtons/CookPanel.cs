using UnityEngine;
using UnityEngine.UI;

public class CookPanel : MonoBehaviour
{
    public Sprite[] icons = new Sprite[4];
    public CookingPlaceEnum CookingPlace;

    private CookBook _cookBook;
    private GameObject _scriptsHere;
    private CookSlot[] CookingSlots;
    private Dish[] _dishes;
    private Dish[] _curDishes = new Dish[4];
    private string[] _nameText = new string[4];


    public void ForStart()
    {
        _scriptsHere = GameObject.FindGameObjectWithTag("ScriptsHere");
        _cookBook = _scriptsHere.GetComponent<CookBook>();
        _dishes = _cookBook.AllMenu();
        CookingSlots = GetComponentsInChildren<CookSlot>();

        Initialize();
    }

    public void Initialize()
    {
        GetIconsAndText();

        for (int i = 0; i < CookingSlots.Length; i++)
        {
            Button btn = CookingSlots[i].GetComponentInChildren<Button>();
            Image spr = btn.transform.GetChild(1).GetComponent<Image>();
            spr.sprite =  icons[i];

            Text txt= btn.gameObject.GetComponentInChildren<Text>();
            txt.text = _nameText[i];
        }
    }

    private void GetIconsAndText()
    {
        int i = 0;
        foreach (Dish dish in _dishes)
        {
            if (dish.CookingPlace == CookingPlace)
            {
                _curDishes[i] = dish;
                _nameText[i] = dish.DishName;
                icons[i] = dish.Icon;
                i++;
            }
        }
    }
}
