using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardFamily : QuestReward
{
    public void FamilyReward(Quest quest)
    {
        switch (quest.QuestLevel)
        {
            case 1:
                UnlockDishes(quest.CookingPlace);
                break;
            case 2:
                UnlockDishes(quest.CookingPlace);
                break;
            case 3:
                UnlockDishes(quest.CookingPlace);
                break;
            case 4:
                AddExp(FromFraction.Family, quest.RepReward);
                break;
            default:
                break;
        }
    }
}
