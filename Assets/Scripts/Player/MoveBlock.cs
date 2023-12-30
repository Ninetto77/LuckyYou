using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveBlock : MonoBehaviour, IStopMoving
{
    private NavMeshAgent _agent;
    private Transform _transform;
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _transform = transform;
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// Метод для остановки NavMeshAgent. Параметр отвечает за время блока
    /// </summary>
    /// <param name="stopTime"></param>
    public void StopMoving(float stopTime)
    {
        _agent.SetDestination(_transform.position);
        _agent.isStopped = true;
        StartCoroutine(CancelBlock(stopTime));
    }

    IEnumerator CancelBlock(float stopTime)
    {
        yield return new WaitForSeconds(stopTime);
        _agent.isStopped = false;
        _agent.SetDestination(_transform.position);
    }
}
