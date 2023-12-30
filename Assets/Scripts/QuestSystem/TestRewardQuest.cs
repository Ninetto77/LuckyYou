using UnityEngine;

public class TestRewardQuest : MonoBehaviour
{
    void Start()
    {
        Parcel.OnQuestDone += GiveReward;
    }

    private void GiveReward(Quest quest)
    {
        switch (quest.QuestFromFraction)
        {
            case FromFraction.Students:
                Debug.Log("Награда от студентов!!!");
                // Метод, открывающтй что-то у студентов. Можно определить уровень квеста  - quest.QuestLevel
                break;
            case FromFraction.Family:
                Debug.Log("Награда от семей!!!");
                // Метод, открывающтй что-то у семей. Можно определить уровень квеста  - quest.QuestLevel
                break;
            case FromFraction.Mafia:
                Debug.Log("Награда от мафии!!!");
                // Метод, открывающтй что-то у мафии. Можно определить уровень квеста  - quest.QuestLevel
                break;
            case FromFraction.Bohemia:
                Debug.Log("Награда от богемы!!!");
                // Метод, открывающтй что-то у богемы. Можно определить уровень квеста  - quest.QuestLevel
                break;
            default:
                break;
        }        
    }
}
