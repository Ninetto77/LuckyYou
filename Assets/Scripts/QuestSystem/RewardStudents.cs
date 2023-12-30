using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardStudents : QuestReward
{
    public void StudentsReward(Quest quest)
    {
        switch (quest.QuestLevel)
        {
            case 1:
                AddExp(FromFraction.Students, quest.RepReward);
                UnlockUpgradesAppliances(1);
                break;
            case 2:
                UnlockUpgradesAppliances(quest.UnlockLevelReward);
                break;
            case 3:
                UnlockUpgradesAppliances(quest.UnlockLevelReward);
                break;
            case 4:
                UnlockUpgradesAppliances(quest.UnlockLevelReward);
                break;
            default:
                break;
        }
    }
}
