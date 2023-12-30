using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CustomerMotor))]
public class CustomerPath : MonoBehaviour
{
    private Transform _anchorsParent;
    private Transform[] _anchors;
    private CustomerMotor _motor;
    //private Customer _customer;
    private Transform _transform;
    private bool _block = false;
    public event Action<bool> InPosition;
    int j = 0;
    private string currentPlace;

    private void AutoFillArray()
    {
        int _totalItems = _anchorsParent.childCount;
        _anchors = new Transform[_totalItems];

        for (int i = 0; i < _totalItems; i++)
        {
            _anchors[i] = _anchorsParent.GetChild(i);
        }
    }
    // Start is called before the first frame update
    public void Start()
    {
        _motor = GetComponent<CustomerMotor>();
        _transform = GetComponent<Transform>();
        _anchorsParent = GameObject.FindGameObjectWithTag("Anchors").GetComponent<Transform>();
        AutoFillArray();
        if (TryGetComponent(out Customer _customer))
        {
            _customer.ForStart();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetReached();        
    }

    /// <summary>
    /// Метод для движения посетителя к Трансформу массива Anchors
    /// </summary>
    /// <param name="value">имя целевого Anchor</param>
    public void MoveTo(string value)
    {
        
        for (int i = 0; i < _anchors.Length; i++)
        {
            if (value == _anchors[i].name)
            {
                currentPlace = value;
                j = i; 
                break;
            }
        }
        _motor.SetTarget(_anchors[j]);
        StartCoroutine(BlockDelay());
    }    
        
    private void TargetReached()
    {
        if (!_block && Vector3.Distance(_transform.position, _anchors[j].position) < 2f)
        {
            InPosition?.Invoke(false);
            _block = true;
        }
    }

    public void SitOnTable()
    {
        for (int i = 0; i < _anchors.Length; i++)
        {
            if (currentPlace  == _anchors[i].name)
            {
                _transform.position = _anchors[i].position;
                _transform.forward = _anchors[i].forward;
            }                
        }               
    }

    IEnumerator BlockDelay()  //Для Устранения бага срабатывания TargetReached на предыдущий якорь
    {
        yield return new WaitForSeconds(0.5f);
        _block = false;
    }
}
