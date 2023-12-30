using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{
    public Sprite DoneQuestSprite;

    [Header("Настройки")]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _questRewardText;
    [SerializeField] private Image _fractionSprite;

    [Header("Икоки фракций")]
    [SerializeField] public Sprite[] FractionSprites;

    [Header("Тексты количества еды")]
    [SerializeField] private TMP_Text[] targetTexts;

    [Header("Икоки еды")]
    [SerializeField] private Image[] targetSprites;

    public string _questNameSlot;
    private GameObject _scriptsHere;
    private FromFraction _fromFraction;
    private Dish[] _dishes;

    void Start()
    {
        Parcel.OnQuestDone += CheckForDone;
        Parcel.OnQuestDoneWithName += GetText;
        Parcel.OnQuestInProgressWithName += GetText;
    }

    private void HideAll(Quest quest)
    {
        HideImages(quest);
        HideTexts(quest);
    }

    public void HideImages(Quest quest)
    {
        for (int i = 2; i >= 2 - quest.QuestDish.Length; i--)
        {
            targetSprites[i].enabled = false;
        }
    }

    public void HideTexts(Quest quest)
    {
        for (int i = 2; i >= 2 - quest.QuestDish.Length; i--)
        {
            targetTexts[i].enabled = false;
        }

    }

    /// <summary>
    /// Метод обновления UI заданий квестов
    /// </summary>
    /// <param name="quest"></param>
    public void UpdateUI(Quest quest)
    {
        _questNameSlot = quest.QuestName;
        GetDishes();
        HideAll(quest);
        ChooseFraction(quest);
        GetIconsAndText(quest);
    }

    private void GetDishes()
    {
        _scriptsHere = GameObject.FindGameObjectWithTag("ScriptsHere");
        _dishes = _scriptsHere.GetComponent<CookBook>().AllMenu();
    }

    /// <summary>
    /// Метод выбора иконки для заданий квестов (фракций)
    /// </summary>
    /// <param name="quest"></param>
    private void ChooseFraction(Quest quest)
    {
        _fromFraction = quest.QuestFromFraction;
        switch (_fromFraction)
        {
            case FromFraction.Students:
                _fractionSprite.sprite = FractionSprites[0];
                break;
            case FromFraction.Family:
                _fractionSprite.sprite = FractionSprites[1];
                break;
            case FromFraction.Mafia:
                _fractionSprite.sprite = FractionSprites[2];
                break;
            case FromFraction.Bohemia:
                _fractionSprite.sprite = FractionSprites[3];
                break;
        }
    }

    /// <summary>
    /// Метод изменения иконки на галочку
    /// </summary>
    /// <param name="quest"></param>
    public void CheckForDone(Quest quest)
    {

        if (quest.QuestStatus == 2 && quest.QuestName == _questNameSlot)
        {
            _fractionSprite.sprite = DoneQuestSprite;
        }
    }

    /// <summary>
    /// Метод извлечения иконок из книги рецептов
    /// </summary>
    /// <param name="quest"></param>
    private void GetIcons(Quest quest)
    {
        for (int j = 0; j < quest.QuestDish.Length; j++)
        {
            foreach (Dish dish in _dishes)
            {
                if (quest.QuestDish[j] == dish.DishName)
                {
                    targetSprites[j].sprite = dish.Icon;
                }
            }
        }
    }

    /// <summary>
    /// Метод изменения текста
    /// </summary>
    /// <param name="quest"></param>
    private void GetText(Quest quest, string name)
    {
        if (quest.QuestName == _questNameSlot)
        {
            _nameText.text = quest.QuestName;
            _questRewardText.text = quest.QuestBody;

            for (int i = 0; i < quest.QuestDish.Length; i++)
            {
                string text = "";
                text += quest.QuestDishesHave[i].ToString();
                text += "/";
                text += quest.QuestDishesNeed[i].ToString();
                targetTexts[i].text = text;
            }
        }
    }

    private void GetIconsAndText(Quest quest)
    {
        GetText(quest, _questNameSlot);
        GetIcons(quest);
    }


}
