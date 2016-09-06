using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

namespace UnityPirates
{
    public class UIFormNoMoney : UIForm
    {
        public Button ShopButton;
        public Button OkButton;

        public void Init(Action onShopButtonClick, Action onOkButtonClick)
        {
            ShopButton.onClick.AddListener(new UnityAction(onShopButtonClick));
            OkButton.onClick.AddListener(new UnityAction(onOkButtonClick));
            EventManager.OnGameEvent += HandleGameEvent;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            EventManager.OnGameEvent -= HandleGameEvent;
        }

        public override void HandleGameEvent(EventManager.GameEvents gameEvent)
        {
            switch (gameEvent)
            {
                case EventManager.GameEvents.UpgradeFailed:
                    gameObject.SetActive(true);
                    break;
                default:
                    if (gameObject.activeSelf) gameObject.SetActive(false);
                    break;
            }
        }
    }
}