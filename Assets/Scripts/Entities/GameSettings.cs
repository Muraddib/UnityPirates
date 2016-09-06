using System;
using UnityEngine;
using UnityEngine.Serialization;
    [Serializable]
    public class GameSettings
    {
        [Header("Player Defaults")]
        public int StartMoney;
        public int PlayerCharacterCount;
        public int CharacterUpgradeCost;
        public int AddMoneyAmount;
        public string[] CharacterNames;
    }
