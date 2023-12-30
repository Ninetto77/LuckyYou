using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UIButtons : Interactable
{
    private PlayerController _controller;

    public override void Start()
    {
        if (interactTransform == null)
        {
            interactTransform = transform.parent.parent;
        }
        _controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public override void Interact()
    {
        base.Interact();
        if (transform.parent.parent.TryGetComponent(out ISwitch _switch)) _switch.Execute(transform.name);
        _controller.RemoveFocus();
        _controller.GetComponent<NavMeshAgent>().SetDestination(_controller.transform.position); // переделать
    }

    public void PressButton()
    {
        _controller.SetFocus(this);
    }
}
