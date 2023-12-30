using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CustomerPath))]
[RequireComponent(typeof(BossInteractable))]
public class Boss : MonoBehaviour
{
    private GameObject _scriptsHere;
    private CustomerPath _customerPath;
    private NavMeshAgent _agent;
    private BossInteractable _interactable;
    private EmojiController _emojiController;
    private int _desire = 0; // 0 - идти к бару, 1 - выдать задание, 2 - уйти

    void Start()
    {
        _emojiController = GetComponent<EmojiController>();
        _scriptsHere = GameObject.FindGameObjectWithTag("ScriptsHere");
        _agent = GetComponent<NavMeshAgent>();
        _customerPath = GetComponent<CustomerPath>();
        _interactable = GetComponent<BossInteractable>();
        _customerPath.InPosition += SwitchDesire;
        QuestManager.OnQuestGiven += SwitchDesire;
        MoveAndDo();
        _interactable.enabled = false;
    }

    private void Update()
    {
        AnimatorUpdate();
    }

    private void SwitchDesire(bool value)
    {
        if (value) 
        { 
            // Действия, связанные с принятием квеста        
        }
        else
        {
            // Действия, связанные с отменой квеста  
        }
        _desire++;
        MoveAndDo();
        if (_desire > 2)
        {
            if (_scriptsHere.TryGetComponent(out IQuestBroker questBroker))
                questBroker.CheckQuestsInQueue();
            Destroy(gameObject);
        }
            

        
    }

    private void MoveAndDo()
    {
        switch (_desire)
        {
            case 0:
                if (gameObject.TryGetComponent(out IWantToOrder wantToOrder))
                    wantToOrder.CashRegisterSwitch(true);
                _customerPath.MoveTo("Bar");
                break;
            case 1:
                _interactable.enabled = true;
                _emojiController.QuestIcon(true);
                break;
            case 2:
                _emojiController.QuestIcon(false);
                if (gameObject.TryGetComponent(out IWantToOrder _wantToOrder))
                    _wantToOrder.CashRegisterSwitch(true);
                _customerPath.MoveTo("Create");
                break;
            default:
                break;
        }
    }

    private void AnimatorUpdate()
    {
        if (_agent != null)
        {
            if (TryGetComponent(out IAnimator animator))
            {
                animator.SetFloat("Move", _agent.velocity.magnitude);
            }
        }
    }

    private void OnDestroy()
    {
        _customerPath.InPosition -= SwitchDesire;
        QuestManager.OnQuestGiven -= SwitchDesire;
    }
}
