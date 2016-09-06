using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

namespace UnityPirates
{
    public class UIFormTitle : UIForm
    {
        public Button BackButton;
        public Text CurrentMoney;

        public void Init(Action onBackButtonClick)
        {
            BackButton.onClick.AddListener(new UnityAction(onBackButtonClick));
            EventManager.OnGameEvent += HandleGameEvent;
            gameObject.SetActive(false);
            UpdateMoneyInfo();
        }

        private void OnDestroy()
        {
            EventManager.OnGameEvent -= HandleGameEvent;
        }

        private void UpdateMoneyInfo()
        {
            CurrentMoney.text = GameplayController.Instance.Player.PlayerMoney.ToString();
        }

        public override void HandleGameEvent(EventManager.GameEvents gameEvent)
        {
            UpdateMoneyInfo();
            switch (gameEvent)
            {
                case EventManager.GameEvents.CharacterClicked:
                    CurrentMoney.gameObject.SetActive(true);
                    gameObject.SetActive(true);
                    break;
                case EventManager.GameEvents.ShopClicked:
                    gameObject.SetActive(true);
                    CurrentMoney.gameObject.SetActive(false);
                    break;
                case EventManager.GameEvents.UpgradeFailed:
                    gameObject.SetActive(false);
                    break;
                case EventManager.GameEvents.ViewCharacterListClicked:
                    CurrentMoney.gameObject.SetActive(true);
                    gameObject.SetActive(true);
                    break;
                case EventManager.GameEvents.ShowGameView:
                    if (gameObject.activeSelf) gameObject.SetActive(false);
                    break;
            }
        }
    }
}
