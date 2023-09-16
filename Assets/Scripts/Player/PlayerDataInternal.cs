using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerDataInternal
    {
        internal readonly ReactiveProperty<int> currentFloorNumber;
        internal readonly ReactiveProperty<int> currentCurrency;
        internal readonly ReactiveProperty<PlayerUpgrades> currentUpgrades;
        internal readonly ReactiveProperty<PlayerInventory> currentInventory;

        internal PlayerDataInternal(
            int currentFloorNumber,
            int currentCurrency,
            PlayerUpgrades currentUpgrades,
            PlayerInventory currentInventory)
        {
            this.currentFloorNumber = new(currentFloorNumber);
            this.currentCurrency = new(currentCurrency);
            this.currentUpgrades = new(currentUpgrades);
            this.currentInventory = new(currentInventory);
        }

        internal void SetFloor(int newFloorNumber)
        {
            currentFloorNumber.Value = newFloorNumber;
        }

        internal void AddCurrency(int amount)
        {
            currentCurrency.Value += amount;
        }
    }
}