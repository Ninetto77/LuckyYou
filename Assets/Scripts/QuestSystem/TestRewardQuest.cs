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
                Debug.Log("������� �� ���������!!!");
                // �����, ����������� ���-�� � ���������. ����� ���������� ������� ������  - quest.QuestLevel
                break;
            case FromFraction.Family:
                Debug.Log("������� �� �����!!!");
                // �����, ����������� ���-�� � �����. ����� ���������� ������� ������  - quest.QuestLevel
                break;
            case FromFraction.Mafia:
                Debug.Log("������� �� �����!!!");
                // �����, ����������� ���-�� � �����. ����� ���������� ������� ������  - quest.QuestLevel
                break;
            case FromFraction.Bohemia:
                Debug.Log("������� �� ������!!!");
                // �����, ����������� ���-�� � ������. ����� ���������� ������� ������  - quest.QuestLevel
                break;
            default:
                break;
        }        
    }
}
