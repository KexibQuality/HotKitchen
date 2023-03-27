using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter,IHasProgress
{
    public static event EventHandler OnAnyCut;
    public event EventHandler OnCut;
    
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged; 
    

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttinProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no KitchenObject here
            if (player.HasKitchenObject())
            {
                //Player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Player carrying something that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    cuttinProgress = 0;
                }
            }
            else
            {
                // Player carrying anything
            }
        }
        else
        {
            //There is KitchenObject here
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                // Player is not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttinProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this,EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());


            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormolized = (float)cuttinProgress / cuttingRecipeSo.cuttingProgressMax
            });

            if (cuttinProgress >= cuttingRecipeSo.cuttingProgressMax)
            {
                //There is a kitchen object here and it cab be cut
                KitchenObjectSo outputKitchenObjectSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSo, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSo inputKitchenObjectSo)
    {
        CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSoWithInput(inputKitchenObjectSo);
        return cuttingRecipeSo != null;
    }

    private KitchenObjectSo GetOutputForInput(KitchenObjectSo inputKitchenObjectSo)
    {
        CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSoWithInput(inputKitchenObjectSo);
        if (cuttingRecipeSo != null)
        {
            return cuttingRecipeSo.output;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSoWithInput(KitchenObjectSo inputKitchenObjectSo)
    {
        foreach (CuttingRecipeSO cuttingRecipeSo in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSo.input == inputKitchenObjectSo)
            {
                return cuttingRecipeSo;
            }
        }

        return null;
    }
}