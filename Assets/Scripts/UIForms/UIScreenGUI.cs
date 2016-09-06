using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

namespace UnityPirates
{
    public class UIScreenGUI : UIForm
    {
        public Button AllCharactersButton;
        public Button ShopButton;
        public Text CurrentMoney;

        public void Init(Action onAllCharactersButtonClick, Action onShopButtonClick)
        {
            AllCharactersButton.onClick.AddListener(new UnityAction(onAllCharactersButtonClick));
            ShopButton.onClick.AddListener(new UnityAction(onShopButtonClick));
            EventManager.OnGameEvent += HandleGameEvent;
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
                case EventManager.GameEvents.ShowGameView:
                    if (!gameObject.activeSelf) gameObject.SetActive(true);
                    break;
                default:
                    if (gameObject.activeSelf) gameObject.SetActive(false);
                    break;
            }
        }
    }
}
