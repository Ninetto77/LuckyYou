using System;
using UnityEngine;

public class Parcel : Interactable
{
    private GameObject _player;
    private GameObject _scriptsHere;
    public static event Action <Quest> OnQuestDone;
    public static event Action <Quest, string> OnQuestDoneWithName;
    public static event Action<Quest> OnQuestInProgress;
    public static event Action <Quest, string> OnQuestInProgressWithName;

    public override void Start()
    {
        base.Start();
        _player = GameObject.FindGameObjectWithTag("Player");
        _scriptsHere = GameObject.FindGameObjectWithTag("ScriptsHere");
    }

    public override void Interact()
    {        
        base.Interact();
        Quest[] quests;
        int _questStatus = 0;
        if (_player.TryGetComponent(out IGiveOrder giveOrder) 
            && _scriptsHere.TryGetComponent(out IQuestBroker questBroker))
        {
            quests = questBroker.AllQuests();
            bool stopPls = false;
            for (int i = 0; i < quests.Length; i++) 
            {
                for (int j = 0; j < quests[i].QuestDish.Length; j++)
                {
                    if (quests[i].QuestStatus == 1 && quests[i].QuestDish[j] == giveOrder.CheckDishInHands())
                    {
                        _questStatus = quests[i].QuestTargetUpdate(giveOrder.CheckDishInHands());
                        giveOrder.GiveDish();
                        OnQuestInProgress?.Invoke(quests[i]);
                        OnQuestInProgressWithName?.Invoke(quests[i], quests[i].QuestName);
                    }
                    if (_questStatus == 2)
                    {
                        QuestReward(quests[i]);
                        stopPls = true;
                        break;
                    }                    
                }
                if (stopPls) break;
                //if (giveOrder.CheckDishInHands() == quests[i].QuestDish)
                //{
                //    _questStatus = quests[i].QuestTargetUpdate();
                //    if (_questStatus == 2)
                //    {
                //        QuestReward(quests[i]);
                //        questBroker.UpdateUI();
                //        giveOrder.GiveDish();
                //    }
                //    break;
                //}            
            }    
        }
    }
    
    public void QuestReward(Quest quest)
    {
        OnQuestDone?.Invoke(quest);
        OnQuestDoneWithName?.Invoke(quest, quest.QuestName);
    }
}
