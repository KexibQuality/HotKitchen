using UnityEngine;

public class ClearCounter : BaseCounter 
{
    [SerializeField] private KithcenObjectSO kithcenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no KitchenObject here
            if (player.HasKitchenObject())
            {
                //Player is carrying here
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
            }
            else
            {
                // Player is not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}