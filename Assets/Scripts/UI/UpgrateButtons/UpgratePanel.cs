using UnityEngine;
using UnityEngine.UI;

public class UpgratePanel : MonoBehaviour
{

    [Header("Данные про улучшения")]
    public UpgrateDescription[] upgrateDescriptions;
    public UpgrateBonus[] upgrateBonuses;
    public UpgrateReputation[] upgrateReputations;
    [Header("Текст цены")]
    public Text[] UpgrateText;
    [Header("Место готовки")]
    [SerializeField] private CookingPlaceEnum CookPlace;

    private UpgrateSlot[] UpgrateSlots;


    public int levelBarrier;

    public int baseLevel;
    public int bonusLevel;
    public int reputationLevel;

    private void Awake()
    {
        UpgrateSlots = GetComponentsInChildren<UpgrateSlot>();
        Initialize();
        CookingPlace.OnShowCookPlaceChange += ChangeTxt;
    }

    public void Initialize()
    {

        string string1 = upgrateDescriptions[1].Cost + "$";
        string string2 = upgrateBonuses[1].Cost + "$";
        string string3 = upgrateReputations[1].Cost + "$";

        UpgrateText[0].text = string1;
        UpgrateText[1].text = string2;
        UpgrateText[2].text = string3;
        
        baseLevel = int.Parse(upgrateDescriptions[0].Level);
        bonusLevel = int.Parse(upgrateBonuses[0].Level);
        reputationLevel = int.Parse(upgrateReputations[0].Level);

        CheckForIntaractableBtn(baseLevel, bonusLevel, reputationLevel);

    }

    public void ChangeTxt(int level, int levelBonus, int levelRep, CookingPlaceEnum CookingPlace)
    {
        if (CookPlace != CookingPlace)
        {
            return;
        }
        if (level != 3)
        {
            string string1 = upgrateDescriptions[level].Cost + "$";
            UpgrateText[0].text = string1;
        }
        if (levelBonus != 3)
        {
            string string2 = upgrateBonuses[levelBonus+1].Cost + "$";
            UpgrateText[1].text = string2;
        }
        if (levelRep != 3)
        {
            string string3 = upgrateReputations[levelRep + 1].Cost + "$";
            UpgrateText[2].text = string3;
        }

        CheckForIntaractableBtn(level, levelBonus, levelRep);
        ChangeLevels(level, levelBonus, levelRep);

    }

    public void ChangeLevels(int level, int levelBonus, int levelRep)
    {
        baseLevel = level;
        bonusLevel = levelBonus;
        reputationLevel= levelRep;
    }

    public void ChangeBarrierLevel(int level)
    {
        levelBarrier = level;

        CheckForIntaractableBtn(baseLevel, bonusLevel, reputationLevel);
    }

    public void CheckForIntaractableBtn(int level, int levelBonus, int levelRep)
    {
        for (int i = 0; i < UpgrateSlots.Length; i++)
        {
            Button btn = UpgrateSlots[i].GetComponentInChildren<Button>();
            if (i == 0 )
            {
                if (level >= levelBarrier)
                    btn.interactable = false;
                else
                    btn.interactable = true;
            }
            if (i == 1)
            {
                if (levelBonus >= levelBarrier)
                    btn.interactable = false;
                else
                    btn.interactable = true;
            }
            if (i == 2)
            {
                if (levelRep >= levelBarrier)
                    btn.interactable = false;
                else
                    btn.interactable = true;
            }
        }
    }
}
