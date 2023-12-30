using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorUpdate();
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


}
