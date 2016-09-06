using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

namespace UnityPirates
{
    public class UIFormCharacterList : UIForm
    {
        public List<Button>[] UpgradeButtons;
        public List<UICharacterView> CharacterViews;
        public GameObject CharUIFormPrefab;
        public RectTransform FormBodyRoot;

        public void Init(PlayerCharacter[] characters, Action onUpgradeButtonClick, Action onViewCharacterClick)
        {
            CharacterViews = new List<UICharacterView>();
            for (int i = 0; i < characters.Length; i++)
            {
                GameObject newForm = Instantiate(CharUIFormPrefab) as GameObject;
                newForm.GetComponent<RectTransform>().SetParent(FormBodyRoot);
                newForm.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                newForm.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                newForm.GetComponent<RectTransform>().localScale = Vector3.one;
                PlayerCharacter playerChar = characters[i];
                newForm.GetComponent<UICharacterView>().CharacterLevel.gameObject.SetActive(true);
                newForm.GetComponent<UICharacterView>().Character = playerChar;
                newForm.GetComponent<UICharacterView>().ViewUpgradeButton.onClick.AddListener(new UnityAction(() =>
                    {
                        UIController.Instance.SelectedCharacter = playerChar;
                        onUpgradeButtonClick();
                    }));
                newForm.GetComponent<UICharacterView>().ViewCharacterButton.onClick.AddListener(new UnityAction(()
                                                                                                                =>
                    {
                        UIController.Instance.SelectedCharacter = playerChar;
                        onViewCharacterClick();
                    }));
                CharacterViews.Add(newForm.GetComponent<UICharacterView>());
            }
            EventManager.OnGameEvent += HandleGameEvent;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            EventManager.OnGameEvent -= HandleGameEvent;
        }

        private void UpdateCharacterInfo()
        {
            CharacterViews.ForEach(a => a.CharacterLevel.text = a.Character.CharacterLevel.ToString());
        }

        public override void HandleGameEvent(EventManager.GameEvents gameEvent)
        {
            switch (gameEvent)
            {
                case EventManager.GameEvents.ViewCharacterListClicked:
                    gameObject.SetActive(true);
                    UpdateCharacterInfo();
                    break;
                case EventManager.GameEvents.CharacterUpgraded:
                    UpdateCharacterInfo();
                    break;
                case EventManager.GameEvents.UpgradeFailed:
                    gameObject.SetActive(false);
                    break;
                case EventManager.GameEvents.CharacterClicked:
                    gameObject.SetActive(false);
                    break;
                default:
                    if (gameObject.activeSelf) gameObject.SetActive(false);
                    break;
            }
        }
    }
}