using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Motor : MonoBehaviour
{
    protected Transform target;
    protected NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (target != null && agent.isActiveAndEnabled == true)
        {
            MovePlayer(target.position);
            FaceTarget();
        }
    }

    /// <summary>
    /// Метод движения игрока к точке
    /// </summary>
    /// <param name="point"></param>
    public void MovePlayer(Vector3 point)
    {
        agent.SetDestination(point);
    }


    /// <summary>
    /// Метод поворота игрока лицом к цели
    /// </summary>
    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
