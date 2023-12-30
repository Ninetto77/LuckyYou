using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBroker : MonoBehaviour
{
    [SerializeField] private RewardStudents _students;
    [SerializeField] private RewardFamily _family;
    [SerializeField] private RewardMafia _mafia;
    [SerializeField] private RewardBohemia _bohemia;


    private void Start()
    {
        Parcel.OnQuestDone += GiveReward;
    }
    private void GiveReward(Quest quest)
    {
        switch (quest.QuestFromFraction)
        {
            case FromFraction.Students:
                Debug.Log("������� �� ���������!!!");
                _students.StudentsReward(quest);
                break;
            case FromFraction.Family:
                Debug.Log("������� �� �����!!!");
                _family.FamilyReward(quest);
                break;
            case FromFraction.Mafia:
                Debug.Log("������� �� �����!!!");
                _mafia.MafiaReward(quest);
                break;
            case FromFraction.Bohemia:
                Debug.Log("������� �� ������!!!");
                _bohemia.BohemiaReward(quest);
                break;
            default:
                break;
        }
    }
}
