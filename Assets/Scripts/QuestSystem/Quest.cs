using System;
using UnityEngine;

[Serializable]
public class Quest
{
    public string QuestName;
    public FromFraction QuestFromFraction;
    public string QuestBody;
    public int[] QuestDishesNeed;
    public int[] QuestDishesHave; 
    public string[] QuestDish;
    public int ReputationBarrier;
    public int QuestLevel;
    public int QuestStatus = 0; // 0 - не начат, 1 - начат, 2 - закончен, 3 - в очереди
    public string QuestDone;
    public string QuestRewardText;
    public int MoneyReward;
    public int RepReward;
    public int UnlockLevelReward;
    public int UnlockElementCount;
    public Sprite BossSprite;
    //public TargetToUnlock ToUnlock;
    public CookingPlaceEnum CookingPlace;

    /// <summary>
    /// 0 - не начат, 1 - начат, 2 - закончен
    /// </summary>
    /// <param name="value"></param>
    public void ChangeStatus(int value)
    {
        QuestStatus = value;
    }

    public int QuestTargetUpdate(string questDish)
    {
        for (int i = 0; i < QuestDish.Length; i++)
        {
            if (QuestDish[i] == questDish)
            {
                QuestDishesHave[i]++;
            }
        }

        bool isDone = true;
        for (int i = 0;i < QuestDish.Length; i++)
        {
            if (QuestDishesHave[i] < QuestDishesNeed[i])
                isDone = false;
        }

        if (isDone)
        {
            QuestStatus = 2;
            QuestTasks.OnQuestDone?.Invoke(this);
            //вызов туториала
            ShowTutorial();
        }

        return QuestStatus;
    }

    public void ShowTutorial()
    {
        if (QuestName == "История прогулов")
        {
            GameManager.Instance.ShowTutorial(4);
        }
        if (QuestName == "И ты… Цезарь")
        {
            GameManager.Instance.ShowTutorial(5);
        }
        if (QuestName == "Светская птица")
        {
            GameManager.Instance.ShowTutorial(6);
        }
    }
    //метод вывзова туториала
}
