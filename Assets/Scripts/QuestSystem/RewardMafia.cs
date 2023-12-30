using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardMafia : QuestReward
{
    public void MafiaReward(Quest quest)
    {
        switch (quest.QuestLevel)
        {
            case 1:
                AddMoney(quest.MoneyReward);
                break;
            case 2:
                UnlockTables(2);
                break;
            case 3:
                UnlockTables(2);
                break;
            case 4:
                AddMoney(quest.MoneyReward);
                break;
            default:
                break;
        }
    }
}
