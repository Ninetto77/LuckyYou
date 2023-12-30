using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

[RequireComponent(typeof(CustomerPath))]
[RequireComponent(typeof(Timer))]
[RequireComponent(typeof(DishReceiver))]
[RequireComponent(typeof(EmojiController))]
public class Customer : MonoBehaviour, ISwitch
{
    [Header("Фракция")]
    [SerializeField] private FromFraction _fromFraction;

    [Header("Пол")]
    [SerializeField] private GenderEnum _gender;

    private EmojiController _emoji;
    private GameObject _scriptsHere;
    private CustomerPath _customerPath;
    private Fraction _fraction;
    private Timer _timer;
    private DishReceiver _dishReceiver;
    private NavMeshAgent _agent;
    private SkinnedMeshRenderer _meshRenderer;
    private AudioManager _audioManager;
    private GameObject CashLightParent;

    //private Cooking _cooking;
    public int _desire = -1;             // 0 - идти к стойке, 1 - ждать принятия заказа, 2 - идти к столу,
                                          // 3 - ждать заказа, 4 - есть, 5 - идти к уборной, 6 - делать дела в уборной,
                                          // 7 - уходить

    private int _feedback = 0;           // Для начисления репутации. -1 = плохо, 1 = хорошо    
    string selectedPlace = null;

    // Start is called before the first frame update
    public void ForStart()
    {
        _scriptsHere = GameObject.FindGameObjectWithTag("ScriptsHere");
        _fraction = _scriptsHere.GetComponent<FractionCustomizer>().FractionSettings(_fromFraction);
        _timer = GetComponent<Timer>();
        _customerPath = GetComponent<CustomerPath>();
        _dishReceiver = GetComponent<DishReceiver>();
        _emoji = GetComponent<EmojiController>();
        _agent = GetComponent<NavMeshAgent>();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _audioManager = FindObjectOfType<AudioManager>();
        CashLightParent = GameObject.FindGameObjectWithTag("CashLight");


        _customerPath.InPosition += SwitchDesire;
        _timer.OnAlarm += SwitchDesire;
        Register.OnOrderAccepted += MyOrderAccepted;

        _desire = -1;
        SwitchDesire(false);
    }

    private void Update()
    {
        AnimatorUpdate();
    }

    private void DesireTumbler(bool imLeaving)
    {
        if (imLeaving && _desire < 4)
        {
            SittingOnTable(false);
            if (_scriptsHere.TryGetComponent(out IWantToOrder _wantToOrder) && _desire == 1)
                _wantToOrder.CashRegisterSwitch(false);
            if (TryGetComponent(out IAnimator animator))
            {
                animator.SetBool("SitWait", false);
                animator.SetBool("Sad", true);
            }                     
            if (_scriptsHere.TryGetComponent(out IManageQueue manageQueue))
                manageQueue.ChangeCustomerQueue(-1);
            _desire = 7;
            _feedback = -1;
            PlaySoundForGender(_feedback);
        }
        else if (_desire == 4 && Randomizer(0, 101) > 1)
        {
            if (TryGetComponent(out IAnimator animator)) animator.SetBool("Sit", false);
            SittingOnTable(false);
            _feedback = 1;
            _desire++;
            if (_scriptsHere.TryGetComponent(out IAnchorManager anchorManager))
                anchorManager.AnchorBusySwitcher(selectedPlace, false);
            if (_scriptsHere.TryGetComponent(out ISelectPlace _selectPlace))
            {
                selectedPlace = _selectPlace.Select("WC");
                if (selectedPlace != null)
                {
                    _customerPath.MoveTo(selectedPlace);
                    anchorManager.AnchorBusySwitcher(selectedPlace, true); 
                    PlaySoundForGender(_feedback);
                }
                else
                {
                    _desire = 6;
                    _feedback = 1;
                    SwitchDesire(false);
                }
                
            }
        }
        else if (_desire == 4 && Randomizer(0, 101) <= 1)
        {
            if (TryGetComponent(out IAnimator animator)) animator.SetBool("Sit", false);
            SittingOnTable(false);
            if (_scriptsHere.TryGetComponent(out IAnchorManager anchorManager))
                anchorManager.AnchorBusySwitcher(selectedPlace, false);            
            _feedback = 1;
            _desire = 7;
            PlaySoundForGender(_feedback);

        }
        else if (_desire == 7)
        {
            if (_scriptsHere.TryGetComponent(out ICallNextOne callNextOne))
                callNextOne.NextCustomer();
            _desire++;
            Destroy(gameObject);
        }
        else _desire++;
    }

    /// <summary>
    /// Метод проигрывания звуков в зависимости от пола клиента
    /// </summary>
    /// <param name="feedback"></param>
    private void PlaySoundForGender(int feedback)
    {
        string gender = _gender.ToString();

        //клиент ждет у кассы
        if (_desire == 1)
            _audioManager.PlaySound($"Wait {gender}");

        //клиент доволен заказом
        if (feedback == 1 && _desire == 7)
            _audioManager.PlaySound($"Pleased {gender}");

        //клиент недоволен заказом
        if (feedback == -1 && _desire == 7)
            _audioManager.PlaySound($"Unpleased {gender}");
    }

    /// <summary>
    /// Метод переключения состояний
    /// </summary>
    /// <param name="imLeaving">true, если посетитель должен уйти</param>
    public void SwitchDesire(bool imLeaving)
    {
        DesireTumbler(imLeaving);

        switch (_desire)
        {
            case 0:
                if (_scriptsHere.TryGetComponent(out IWantToOrder wantToOrder))
                    wantToOrder.CashRegisterSwitch(true);
                _customerPath.MoveTo("Bar");
                break;
            case 1:                
                _timer.SetTimer(_fraction.WaitingTimeToOrder);
                if (CashLightParent.TryGetComponent(out ICashLight cashLight))
                    cashLight.LightOn(true);
                _audioManager.PlaySound("Order");
                //_emoji.WantToOrder(true);
                break;
            case 2:
                _timer.TimerReset();
                if (_scriptsHere.TryGetComponent(out IWantToOrder _wantToOrder))
                    _wantToOrder.CashRegisterSwitch(false);
                if (CashLightParent.TryGetComponent(out ICashLight _cashLight))
                    _cashLight.LightOn(false);
                IWillWaitOrder();
                break;
            case 3:                               
                _timer.SetTimer(_fraction.OrderWaitingTime);
                if (TryGetComponent(out IAnimator animator)) animator.SetBool("SitWait", true);
                SittingOnTable(true);
                break;
            case 4:
                _timer.TimerReset();
                MyOrderIsReady();                               
                _timer.SetTimer(_fraction.EatingTime);
                break;
            case 5:
                _audioManager.PlaySound("Payment");
                break;
            case 6:
                _meshRenderer.enabled = false;
                _timer.SetTimer(_fraction.DoingTime);
                _audioManager.PlaySound("Flush");
                break;
            case 7:
                _meshRenderer.enabled = true;
                _timer.TimerReset();
                //_emoji.WaitOrder(true, "angry");
                if (_scriptsHere.TryGetComponent(out IChangeBaseRep _changeBaseRep))
                    _changeBaseRep.ChangeBaseRep(1 * _feedback);
                if (_scriptsHere.TryGetComponent(out IAnchorManager anchorManager))
                    anchorManager.AnchorBusySwitcher(selectedPlace, false);
                _customerPath.MoveTo("Create");
                break;
            default:
                break;
        };        
    } 
    
    private void IWillWaitOrder()
    {        
        if (_scriptsHere.TryGetComponent(out IManageQueue manageQueue))
            manageQueue.ChangeCustomerQueue(0);
        _dishReceiver.IWillWaitOrder(_fraction);
        //if (_scriptsHere.TryGetComponent(out IChoose choose))
        //    _choosedDish = choose.SelectDish(_fraction.FractionName);
        if (_scriptsHere.TryGetComponent(out ISelectPlace _selectPlace))
        {
            selectedPlace = _selectPlace.Select("Table");
            if (selectedPlace != null) 
            {
                _customerPath.MoveTo(selectedPlace);
            }
            else
            {
                SwitchDesire(true);
            }
            
        }
        if (_scriptsHere.TryGetComponent(out IAnchorManager anchorManager))
            anchorManager.AnchorBusySwitcher(selectedPlace, true);
    }

    private void MyOrderIsReady()  
    {
        _emoji.WaitOrder(false, null);
        if (_scriptsHere.TryGetComponent(out IManageQueue manageQueue))
            manageQueue.ChangeCustomerQueue(-1);
        if (TryGetComponent(out IAnimator animator))
        {
            animator.SetBool("SitWait", false);
            animator.SetBool("Sit", true);
        }               
    }

    public void Execute(string value)
    {        
        SwitchDesire(false);
    }

    public void MyOrderAccepted()
    {        
        if (_desire == 1) 
            SwitchDesire(false);        
    }

    private int Randomizer(int min, int max)
    {
        Random rnd = new Random();
        return rnd.Next(min, max);
    }

    public float CheckDesire()
    {
        return _desire;
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

    private void SittingOnTable(bool value)
    {
        if (_agent != null)
        {
            _agent.enabled = !value;
            if (value)
            {
                _customerPath.SitOnTable();
            }            
        }
    }

    private void OnDestroy()
    {
        //Cooking.OnCookDone += DishReadiness;
        _audioManager.PlaySound("Leaving");
        _customerPath.InPosition -= SwitchDesire;
        _timer.OnAlarm -= SwitchDesire;
        Register.OnOrderAccepted -= MyOrderAccepted;
    }
}

public enum FromFraction
{
    Students,
    Family,
    Mafia,
    Bohemia
}
public enum GenderEnum
{
    female,
    male
}

