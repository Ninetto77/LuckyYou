using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerInteract : Interactable
{
    public override void Interact()
    {
        base.Interact();
        if (TryGetComponent(out ISwitch _switch)) _switch.Execute(transform.name);
    }
}
