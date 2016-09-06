using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

namespace UnityPirates
{
    public class UIFormShop : UIForm
    {
        public Button BuyButton;
        public Text CurrentMoney;

        public void Init(Action onBuyButtonClick)
        {
            BuyButton.onClick.AddListener(new UnityAction(onBuyButtonClick));
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
            CurrentMoney.text = "Money: " + GameplayController.Instance.Player.PlayerMoney.ToString();
        }

        public override void HandleGameEvent(EventManager.GameEvents gameEvent)
        {
            switch (gameEvent)
            {
                case EventManager.GameEvents.ShopClicked:
                    gameObject.SetActive(true);
                    UpdateMoneyInfo();
                    break;
                case EventManager.GameEvents.MoneyRaised:
                    UpdateMoneyInfo();
                    break;
                default:
                    if (gameObject.activeSelf) gameObject.SetActive(false);
                    break;
            }
        }
    }
}
