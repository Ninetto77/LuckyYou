using System.Collections;
using UnityEngine;
using Random = System.Random;

public class CustomerCreator : MonoBehaviour, ICallNextOne, IManageQueue
{
    [SerializeField] private GameObject[] _studentCustomers;
    [SerializeField] private GameObject[] _familyCustomers;
    [SerializeField] private GameObject[] _mafiaCustomers;
    [SerializeField] private GameObject[] _bohemiaCustomers;
    [SerializeField] private Transform _createPosition;
    private FractionCustomizer _customizer;
    public int isStoped = 0; // 0 - свободен, 1 - надо вызвать босса, 2 - босс вызван
    public int _customersInQueue = 0;

    private bool IsFirst;

    // Start is called before the first frame update
    void Start()
    {
        _customizer = GameObject.FindGameObjectWithTag("ScriptsHere").GetComponent<FractionCustomizer>();
        CreateCustomer();
        IsFirst = false;
    }

    public void CreateCustomer()
    {
        FromFraction fractionName;
        if (isStoped < 1) fractionName = SelectFraction();
        else
        {
            fractionName = WhichBoss().QuestFromFraction;
        }
        GameObject newCustomer = null;
        switch (fractionName)
        {
            case FromFraction.Students:
                if (isStoped == 1 && gameObject.TryGetComponent(out IBossTaxi bossTaxiS))
                {
                    bossTaxiS.CreateBoss(FromFraction.Students);
                    isStoped++;
                }
                if (isStoped < 1) newCustomer = _studentCustomers[Randomizer(0, _studentCustomers.Length)];
                break;
            case FromFraction.Family:
                if (isStoped == 1 && gameObject.TryGetComponent(out IBossTaxi bossTaxiF))
                {
                    bossTaxiF.CreateBoss(FromFraction.Family);
                    isStoped++;
                }
                if (isStoped < 1) newCustomer = _familyCustomers[Randomizer(0, _familyCustomers.Length)];
                break;
            case FromFraction.Mafia:
                if (isStoped == 1 && gameObject.TryGetComponent(out IBossTaxi bossTaxiM))
                {
                    bossTaxiM.CreateBoss(FromFraction.Mafia);
                    isStoped++;
                }
                if (isStoped < 1) newCustomer = _mafiaCustomers[Randomizer(0, _mafiaCustomers.Length)];
                break;
            case FromFraction.Bohemia:
                if (isStoped == 1 && gameObject.TryGetComponent(out IBossTaxi bossTaxiB))
                {
                    bossTaxiB.CreateBoss(FromFraction.Bohemia);
                    isStoped++;
                }
                if (isStoped < 1) newCustomer = _bohemiaCustomers[Randomizer(0, _bohemiaCustomers.Length)];
                break;
            default:
                break;
        }
        if (newCustomer != null)
        {
            if (IsFirst == false)
            {
                StartCoroutine(Delay());
                IsFirst = true;
            }
            GameObject customerClone = Instantiate(newCustomer, _createPosition.position, Quaternion.identity);
            _customersInQueue++;
            FindObjectOfType<AudioManager>().PlaySound("Enter client");
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.ShowTutorial(1);
    }    

    private Quest WhichBoss()
    {
        if (gameObject.TryGetComponent(out IQuestBroker questBroker))
            return questBroker.CurrentQuest();
        else return null;
    }

    private FromFraction SelectFraction()
    {
        Fraction[] tempFractions = _customizer.AllFractions();
        int totalChance = 0;
        FromFraction targetFraction = FromFraction.Students;
        //Определяем общее количество процентов в вероятности (защита от неправильного ввода)
        for (int i = 0; i < tempFractions.Length; i++)
        {
            totalChance += tempFractions[i].CreateChance;
        }
        // Создаем случайное число и проверяем в какой диапазон оно попало
        int chanceValue = Randomizer(1, totalChance);
        for (int i = 0; i < tempFractions.Length; i++)
        {
            if (chanceValue >= totalChance - tempFractions[i].CreateChance && chanceValue < totalChance)
            {
                targetFraction = tempFractions[i].IsFraction;
            }
            totalChance -= tempFractions[i].CreateChance;
        }
        // Возвращаем имя фракции согласно диапазону        
        return targetFraction;
    }

    public void ChangeCustomerQueue(int value)
    {
        _customersInQueue += value;
        if (gameObject.TryGetComponent(out IWantToOrder wantToOrder) && !wantToOrder.CashRegisterBusy())
            NextCustomer();
    }

    public void NextCustomer()
    {
        if (_customersInQueue < 3)
            StartCoroutine(NextCustomerDelay());
    }

    IEnumerator NextCustomerDelay()
    {
        yield return new WaitForSeconds((float)Randomizer(1, 4));
        if (gameObject.TryGetComponent(out IWantToOrder wantToOrder) && !wantToOrder.CashRegisterBusy())
        {
            if (_customersInQueue < 3)
                CreateCustomer();
        }
    }

    public void StopCreate(bool value)
    {
        if (value) isStoped = 1;
        else isStoped = 0;
        if (isStoped == 0) NextCustomer();
    }

    public int IsStopCheck()
    {
        return isStoped;
    }

    private int Randomizer(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
}
