using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

namespace UnityPirates
{
    public class UIFormCharacter : UIForm
    {
        public Button UpgradeButton;
        public Text CharacterName;
        public Text CharacterLevel;
        public Text CharacterHealth;
        public Text CharacterMana;

        public void Init(Action onUpgradeButtonClick)
        {
            UpgradeButton.onClick.AddListener(new UnityAction(onUpgradeButtonClick));
            EventManager.OnGameEvent += HandleGameEvent;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            EventManager.OnGameEvent -= HandleGameEvent;
        }

        private void UpdateCharacterInfo()
        {
            CharacterName.text = UIController.Instance.SelectedCharacter.CharacterName;
            CharacterLevel.text = "Level: " + UIController.Instance.SelectedCharacter.CharacterLevel.ToString();
            CharacterHealth.text = "Health: " + UIController.Instance.SelectedCharacter.CharacterHealth.ToString();
            CharacterMana.text = "Mana: " + UIController.Instance.SelectedCharacter.CharacterMana.ToString();
        }

        public override void HandleGameEvent(EventManager.GameEvents gameEvent)
        {
            switch (gameEvent)
            {
                case EventManager.GameEvents.CharacterClicked:
                    gameObject.SetActive(true);
                    UpdateCharacterInfo();
                    break;
                case EventManager.GameEvents.CharacterUpgraded:
                    UpdateCharacterInfo();
                    break;
                default:
                    if (gameObject.activeSelf) gameObject.SetActive(false);
                    break;
            }
        }
    }
}