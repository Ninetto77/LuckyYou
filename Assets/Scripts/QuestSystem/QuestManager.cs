using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

[RequireComponent(typeof(FractionCustomizer))]
public class QuestManager : MonoBehaviour, IQuestBroker
{
    public Quest[] quests;
    [SerializeField] private GameObject _questCanvas;
    private GameObject _scriptsHere;
    private QuestUI _questUI;
    private Button acceptButton;
    private Quest _curQuest;
    public static event Action<bool> OnQuestGiven;
    public float activeQuestsCount;
    private GameObject CashLightParent;

    private void Start()
    {
        _scriptsHere = GameObject.FindGameObjectWithTag("ScriptsHere");
        acceptButton = _questCanvas.transform.GetChild(0).GetChild(2).GetComponent<Button>();
        acceptButton.onClick.AddListener(AcceptButton);
        FractionCustomizer.OnReputationChanged += QuestActivator;
        BossInteractable.OnBossHere += StartQuest;
        Parcel.OnQuestDone += QuestDone;
        _questUI = GetComponent<QuestUI>();
        CashLightParent = GameObject.FindGameObjectWithTag("ParcelLight");
    }

    private void QuestDone(Quest quest)
    {
        activeQuestsCount--;
        if (activeQuestsCount <= 0) 
        {
            if (CashLightParent.TryGetComponent(out ICashLight _cashLight))
                _cashLight.LightOn(false);
            Debug.Log(CashLightParent);
        }
    }

    public void QuestActivator(FromFraction fromFraction, float repValue)
    {
        int minLevel = quests.Length;
        int j = 99;

        for (int i = 0; i < quests.Length; i++)
        {
            // Проверяем на наличие квестов в очереди
            if (quests[i].QuestStatus == 3)
            {
                j = 99;
                break;
            }
            // Проверяем, нет ли активных квестов
            if (quests[i].QuestStatus == 1 && quests[i].QuestFromFraction == fromFraction)
            {
                j = 99;
                break;
            }
            // Отсеиваем выполненные квесты
            if (quests[i].QuestStatus < 1)
            {
                // Отсеиваем квесты других фракций и выявляем следующий квест по уровню
                if (quests[i].QuestFromFraction == fromFraction && quests[i].ReputationBarrier <= repValue)
                {
                    if (quests[i].QuestLevel < minLevel)
                    {
                        minLevel = quests[i].QuestLevel;
                        j = i;
                    }                    
                }
            }            
        }
        if (j != 99)
        {
            _curQuest = quests[j];
            quests[j].ChangeStatus(3);
            SendBoss(_curQuest);
        }
    }

    public void SendBoss(Quest quest)
    {
        if (_scriptsHere.TryGetComponent(out IManageQueue manageQueue) && manageQueue.IsStopCheck() < 2)
            manageQueue.StopCreate(true);        
    }    

    public Quest CurrentQuest()
    {
        return _curQuest;
    }

    public void StartQuest()
    {
        GameManager.Instance.ShowQuestCanvas();
        _questUI.UpdateUI(_curQuest);
    }

    public void AcceptButton()
    {
        if(_curQuest != null)
        {
            _curQuest.ChangeStatus(1);            
            UpdateUI();
            GameManager.Instance.HideCanvas();
            OnQuestGiven?.Invoke(true);
            _questUI.HideImages();
            QuestTasks.OnQuestAppear?.Invoke(_curQuest);
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i].QuestStatus == 1) activeQuestsCount++;
        }
        if (CashLightParent.TryGetComponent(out ICashLight _cashLight))
            _cashLight.LightOn(true);
    }

    public Quest[] AllQuests()
    {
        return quests;
    }

    public void CheckQuestsInQueue()
    {
        bool check = false;
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i].QuestStatus == 3)
            {
                SendBoss(quests[i]);
                check = true;
                break;
            }
        }
        if (!check)
        {
            if (_scriptsHere.TryGetComponent(out IManageQueue manageQueue))
                manageQueue.StopCreate(false);
        }
    }

    private void OnDestroy()
    {
        FractionCustomizer.OnReputationChanged -= QuestActivator;
        BossInteractable.OnBossHere -= StartQuest;
        Parcel.OnQuestDone -= QuestDone;
    }
}
