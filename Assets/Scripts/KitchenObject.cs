using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KithcenObjectSO kitchenObjectSO;

     private IKitchenObjectParent kitchenObjectParent;


    public KithcenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    { if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        
        this.kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("IKitchenObjectParent already has a kitchenObject!!!");
        }
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroySelf()
    {   kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KithcenObjectSO kitchenObjectSo, IKitchenObjectParent kitchenObjectParent)
    {
        
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>(); 
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }
}