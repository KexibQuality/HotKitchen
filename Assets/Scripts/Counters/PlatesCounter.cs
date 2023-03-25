using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

  public event EventHandler OnPlateSpawned;
  
  
  public event EventHandler OnPlateRemoved;
  
  [SerializeField] private KitchenObjectSo plateKitchenObjectSo;
  private float spawnPlateTimer;
  private float spawnPlateTimerMax = 4f;
  private int platesSpawnAmount;
  private int platesSpawnAmountMax =4;

  private void Update()
  {
    spawnPlateTimer += Time.deltaTime;
    if (spawnPlateTimer >= spawnPlateTimerMax)
    {
      spawnPlateTimer = 0f;

      if (platesSpawnAmount < platesSpawnAmountMax)
      {
        platesSpawnAmount++;
        
        OnPlateSpawned?.Invoke(this,EventArgs.Empty);
      }
    }
  }

  public override void Interact(Player player)
  {
    if (!player.HasKitchenObject())
    {
      //Player is empty Handed
      if (platesSpawnAmount > 0)
      {
        //There is at least one plate here
        platesSpawnAmount--;

        KitchenObject.SpawnKitchenObject(plateKitchenObjectSo, player);
        
        OnPlateRemoved?.Invoke(this, EventArgs.Empty);
      }
      
    }
  }
}
