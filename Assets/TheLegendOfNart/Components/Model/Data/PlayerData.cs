using System;
using UnityEngine;

namespace TheLegendsOfNart.Components.Model
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;
        public InventoryData inventory => _inventory;

        public int Hp;

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);            
        }
    }
}
