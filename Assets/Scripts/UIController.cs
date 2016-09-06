using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityPirates
{
    public class UIController : Singleton<UIController>
    {
        public PlayerCharacter SelectedCharacter { get; set; }
        public Stack<EventManager.GameEvents> NavEvents { get; set; }

        private void Awake()
        {
            EventManager.OnGameEvent += EventManager_GenericGameEvent;
            NavEvents = new Stack<EventManager.GameEvents>();
        }

        private void OnDestroy()
        {
            EventManager.OnGameEvent -= EventManager_GenericGameEvent;
        }

        public void Init(GameUIForms uiforms, RectTransform UIRoot)
        {
            foreach (var uiForm in uiforms.Forms)
            {
                GameObject newForm = Instantiate(uiForm.FormPrefab) as GameObject;
                newForm.GetComponent<RectTransform>().SetParent(UIRoot);
                newForm.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                newForm.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                newForm.GetComponent<RectTransform>().localScale = Vector3.one;
                var form = newForm.GetComponent<UIForm>();
                switch (form.FormID)
                {
                    case UIFormIDs.CharacterInfo:
                        ((UIFormCharacter) form).Init(
                            onUpgradeButtonClick: () => GameplayController.Instance.UpgradeCharacter());
                        break;
                    case UIFormIDs.NoMoney:
                        ((UIFormNoMoney) form).Init(
                            onShopButtonClick: () => EventManager.CallGameEvent(EventManager.GameEvents.ShopClicked),
                            onOkButtonClick: GetLastUINavigation);
                        break;
                    case UIFormIDs.Shop:
                        ((UIFormShop) form).Init(onBuyButtonClick: () => GameplayController.Instance.AddMoney());
                        break;
                    case UIFormIDs.Characters:
                        ((UIFormCharacterList) form).Init(GameplayController.Instance.Player.Characters.ToArray(),
                                                          onUpgradeButtonClick:
                                                              () => GameplayController.Instance.UpgradeCharacter(),
                                                          onViewCharacterClick:
                                                              () =>
                                                              EventManager.CallGameEvent(
                                                                  EventManager.GameEvents.CharacterClicked));
                        break;
                    case UIFormIDs.ScreenGUI:
                        ((UIScreenGUI) form).Init(
                            onAllCharactersButtonClick:
                                () => EventManager.CallGameEvent(EventManager.GameEvents.ViewCharacterListClicked),
                            onShopButtonClick: () => EventManager.CallGameEvent(EventManager.GameEvents.ShopClicked));
                        break;
                    case UIFormIDs.Title:
                        ((UIFormTitle) form).Init(onBackButtonClick: OnBackButtonClick);
                        break;
                }
            }
        }

        public void GetLastUINavigation()
        {
            if (NavEvents.Count > 0)
                EventManager.CallGameEvent(NavEvents.Peek());
            else
                EventManager.CallGameEvent(EventManager.GameEvents.ShowGameView);
        }

        public void OnCharacterClicked(object sender, CharacterClickedEventArgs eventArgs)
        {
            if (NavEvents.Count != 0) return;
            Debug.Log(sender);
            SelectedCharacter = (PlayerCharacter) sender;
            EventManager.CallGameEvent(EventManager.GameEvents.CharacterClicked);
        }

        private void OnBackButtonClick()
        {
            if (NavEvents.Count > 0) NavEvents.Pop();
            EventManager.CallGameEvent(NavEvents.Count > 0 ? NavEvents.Peek() : EventManager.GameEvents.ShowGameView);
        }

        private void EventManager_GenericGameEvent(EventManager.GameEvents eventID)
        {
            if (NavEvents.Count > 0 && NavEvents.Peek() == eventID) return;
            switch (eventID)
            {
                case EventManager.GameEvents.CharacterClicked:
                    NavEvents.Push(EventManager.GameEvents.CharacterClicked);
                    break;
                case EventManager.GameEvents.ShopClicked:
                    NavEvents.Push(EventManager.GameEvents.ShopClicked);
                    break;
                case EventManager.GameEvents.ViewCharacterListClicked:
                    NavEvents.Push(EventManager.GameEvents.ViewCharacterListClicked);
                    break;
            }
            DebugStack();
        }

        private void DebugStack()
        {
            foreach (var navEvent in NavEvents)
            {
                Debug.Log("NavEvents Stack:" + navEvent);
            }
        }
    }
}