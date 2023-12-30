using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Interactable
{
    public override void Interact()
    {
        base.Interact();
        if (player.TryGetComponent(out IGiveOrder giveOrder))
            giveOrder.GiveDish();
    }
}
