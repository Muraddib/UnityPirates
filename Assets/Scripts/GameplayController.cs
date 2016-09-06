using UnityEngine;
using System.Collections;

namespace UnityPirates
{
    public class GameplayController : Singleton<GameplayController>
    {
        public PlayerController Player;
        public GameSettings Settings;

        public void Init(PlayerController player, GameSettings settings)
        {
            Player = player;
            Settings = settings;
        }

        public void AddMoney()
        {
            Player.PlayerMoney += Settings.AddMoneyAmount;
            EventManager.CallGameEvent(EventManager.GameEvents.MoneyRaised);
        }

        public void UpgradeCharacter()
        {
            if (Player.PlayerMoney - Settings.CharacterUpgradeCost > 0)
            {
                Player.PlayerMoney -= Settings.CharacterUpgradeCost;
                UIController.Instance.SelectedCharacter.Upgrade();
                EventManager.CallGameEvent(EventManager.GameEvents.CharacterUpgraded);
            }
            else
            {
                EventManager.CallGameEvent(EventManager.GameEvents.UpgradeFailed);
            }
        }
    }
}
