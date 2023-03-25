using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSo kitchenObjectSo;

    public event EventHandler OnPlayGrabbedObject;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {// Player is not carrying anything
            KitchenObject.SpawnKitchenObject(kitchenObjectSo, player);
            
            OnPlayGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}