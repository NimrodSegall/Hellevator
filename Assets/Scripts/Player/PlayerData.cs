using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerData
    {
        private ReactiveProperty<PlayerDataInternal> _data;
        public PlayerData(
            int currentFloorNumber = 0,
            int currentCurrency = 0,
            PlayerUpgrades currentUpgrades = null,
            PlayerInventory currentInventory = null)
        {
            var playerData = new PlayerDataInternal(currentFloorNumber, currentCurrency, currentUpgrades, currentInventory);
            _data = new ReactiveProperty<PlayerDataInternal>(playerData);
        }

        public ReactiveProperty<int> CurrentFloorNumber => _data.Value.currentFloorNumber;
        public ReactiveProperty<int> CurrentCurrency => _data.Value.currentCurrency;

        public void SetFloor(int newFloorNumber)
        {
            _data.Value.SetFloor(newFloorNumber);
        }

        public void AddCurrency(int amount)
        {
            _data.Value.AddCurrency(amount);
        }

        public void SubtractCurrency(int amount)
        {
            AddCurrency(-amount);
        }
    }
}