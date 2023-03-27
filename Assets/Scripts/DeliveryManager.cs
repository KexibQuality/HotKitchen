using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

        [SerializeField] private RecipeListSO recipeListSo;
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;

    private void Awake()
    {
        Instance = this;
        
        
        
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSo.recipeSoList[Random.Range(0, recipeListSo.recipeSoList.Count)];
                Debug.Log(waitingRecipeSO.name);
                waitingRecipeSOList.Add(waitingRecipeSO);
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSo = waitingRecipeSOList[i];

            if (waitingRecipeSo.KitchenObjectSoList.Count == plateKitchenObject.GetKitchenObjectSoList().Count)
            {
                //Has the same number of ingredients
                bool plateContainsMathcesRecipe = true;

                foreach (KitchenObjectSo recipeKitchenObjectSO in waitingRecipeSo.KitchenObjectSoList)
                {
                    // Cycling through all ingredients in the recipe
                    bool ingredientFound = false;

                    foreach (KitchenObjectSo plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSoList())
                    {
                        // Cycling through all ingredients in the plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            // Ingredients Matches!
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        //The Recipe ingredient was not found on the Plate
                        plateContainsMathcesRecipe = false;
                    }
                }

                if (plateContainsMathcesRecipe)
                {
                    //Player delivered the correct recipe!
                    Debug.Log("Player delivered the correct recipe!");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }
        // No matches found!
        // Player did not deliver a correct recipe!
        Debug.Log("Player did not deliver a correct recipe!");
    }
}