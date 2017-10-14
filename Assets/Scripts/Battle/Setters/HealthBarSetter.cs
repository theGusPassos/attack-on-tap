using AttackOnTap.Managers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AttackOnTap.Battle.Setters
{
    public class HealthBarSetter : MonoBehaviour
    {
        public Image characterFace;

        private void Start()
        {
            if (!string.IsNullOrEmpty(CharactersManager.mainCharacter.name))
            {
                SetHealthBarStats(CharactersManager.mainCharacter);
            }
            else
            {
                throw new Exception("There is no selected character");
            }
        }

        private void SetHealthBarStats(CharacterInfo character)
        {
            SetCharacterImage(character.face);
            SetCharacterHealthBar(character.obj.GetComponent<HealthPointSystem>());
        }

        private void SetCharacterImage(Sprite face)
        {
            characterFace.sprite = face;
        }

        private void SetCharacterHealthBar(HealthPointSystem healthPointsSystem)
        {
            healthPointsSystem.SetHealthBar(GetComponent<HealthBar>());
        }
    }
}
