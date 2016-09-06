using UnityEngine;
using System.Collections;

namespace UnityPirates
{
    public class EventManager : MonoBehaviour
    {
        public enum GameEvents
        {
            CharacterClicked = 0,
            CharacterUpgraded = 1,
            UpgradeFailed = 2,
            ShopClicked = 3,
            MoneyRaised = 4,
            ViewCharacterListClicked = 5,
            ShowGameView = 6
        }

        public delegate void GameEvent(GameEvents eventID);

        public static event GameEvent OnGameEvent;

        public static void CallGameEvent(GameEvents eventID)
        {
            if (OnGameEvent != null)
            {
                OnGameEvent(eventID);
                Debug.Log("EventManager:" + eventID);
            }
        }

        public delegate void CharacterSelected(PlayerCharacter character);

        public static event CharacterSelected OnCharacterSelected;

        public static void CallCharacterSelected(PlayerCharacter character)
        {
            if (OnCharacterSelected != null)
            {
                OnCharacterSelected(character);
            }
        }
    }
}