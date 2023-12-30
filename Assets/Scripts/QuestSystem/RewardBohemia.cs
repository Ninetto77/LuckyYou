using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBohemia : QuestReward
{
    public void BohemiaReward(Quest quest)
    {
        switch (quest.QuestLevel)
        {
            case 1:
                UnlockCooking(quest.UnlockElementCount);
                Debug.Log(quest.QuestName);
                break;
            case 2:
                UnlockCooking(quest.UnlockElementCount);
                break;
            case 3:
                UnlockCooking(quest.UnlockElementCount);
                break;
            case 4:
                UnlockCooking(quest.UnlockElementCount);
                break;
            default:
                break;
        }
    }
}
