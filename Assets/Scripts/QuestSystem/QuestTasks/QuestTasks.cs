using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestTasks : MonoBehaviour
{
    public static Action<Quest> OnQuestAppear;
    public static Action<Quest> OnQuestDone;

    [SerializeField] private GameObject slot;
    [SerializeField] private Transform _parent;
    private List<QuestSlot> QuestSlots = new List<QuestSlot>();

    private GameObject curObject ;

    private void Start()
    {
        //QuestSlots = GetComponentsInChildren<QuestSlot>();
        OnQuestAppear += AddQuest;
    }

    /// <summary>
    /// Метод добавления квеста в список квестов
    /// </summary>
    /// <param name="quest"></param>
    public void AddQuest(Quest quest)
    {
        curObject = Instantiate(slot, _parent);
        QuestSlot questSlot = curObject.GetComponent<QuestSlot>();

        QuestSlots.Add(questSlot);
        questSlot.UpdateUI(quest);

    }
}
