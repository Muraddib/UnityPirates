using System;
using UnityEngine;

namespace UnityPirates
{
    public class CharacterClickedEventArgs : EventArgs
    {
    }

    public class PlayerCharacter : MonoBehaviour
    {
        public event EventHandler<CharacterClickedEventArgs> OnClicked = (sender, e) => { };

        public string CharacterName { get; set; }
        public int CharacterLevel { get; set; }
        public int CharacterHealth { get; set; }
        public int CharacterMana { get; set; }

        public void Upgrade()
        {
            CharacterLevel += 1;
            CharacterHealth += 20;
            CharacterMana += 5;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                {
                    var eventArgs = new CharacterClickedEventArgs();
                    OnClicked(this, eventArgs);
                    //EventManager.CallCharacterSelected(this);
                }
            }
        }
    }
}