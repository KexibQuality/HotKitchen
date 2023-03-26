using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSo kitchenObjectSo;
    }

    [SerializeField] private List<KitchenObjectSo> validKitchenObjectSOList;
    private List<KitchenObjectSo> kitchenObjectSoList;

    private void Awake()
    {
        kitchenObjectSoList = new List<KitchenObjectSo>();
    }

    public bool TryAddIngredient(KitchenObjectSo kitchenObjectSo)
    {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSo))
        {
            //Not a valid ingredient
            return false;
        }

        if (kitchenObjectSoList.Contains(kitchenObjectSo))
        {
            //already has this type 
            return false;
        }

        kitchenObjectSoList.Add(kitchenObjectSo);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
        {
            kitchenObjectSo = kitchenObjectSo
        });
        return true;
    }
}