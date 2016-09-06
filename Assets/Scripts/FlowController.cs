using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace UnityPirates
{
    public class FlowController : MonoBehaviour
    {
        public GameObject CharacterPrefab;
        public GameUIFormsKeeper UIFormsKeeper;
        public GameSettingsKeeper SettingsKeeper;
        public RectTransform UIRoot;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            PlayerController player = new PlayerController();
            player.PlayerMoney = SettingsKeeper.GameSettings.StartMoney;
            player.Characters = new List<PlayerCharacter>();

            int count = SettingsKeeper.GameSettings.PlayerCharacterCount;
            float startX = (count / 2f - count) * 2f;
            float step = Mathf.Abs(startX * 2f) / count;
            for (int i = 0; i < count; i++)
            {
                GameObject playerChar =
                    Instantiate(CharacterPrefab, new Vector3(startX + step*(i + 1) - step*0.5f, 0f, 0f),
                                Quaternion.identity) as GameObject;
                PlayerCharacter character = playerChar.GetComponent<PlayerCharacter>();
                character.CharacterName = GetRandomName(SettingsKeeper.GameSettings);
                player.Characters.Add(character);
                character.OnClicked += UIController.Instance.OnCharacterClicked;
            }
            GameplayController.Instance.Init(player, SettingsKeeper.GameSettings);
            UIController.Instance.Init(UIFormsKeeper.UIForms, UIRoot);
        }

        private string GetRandomName(GameSettings settings)
        {
            return settings.CharacterNames[Random.Range(0, settings.CharacterNames.Length)];
        }
    }
}
