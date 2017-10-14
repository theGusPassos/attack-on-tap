using AttackOnTap.Characters;
using AttackOnTap.Managers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AttackOnTap.Battle.Setters
{
    public class SetButtons : MonoBehaviour
    {
        public GameObject[] buttonsObj;

        private Button[]    buttons;
        private Image[]     buttonsImage;

        private void Awake()
        {
            buttons         = new Button[buttonsObj.Length];
            buttonsImage    = new Image[buttonsObj.Length];

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i]      = buttonsObj[i].GetComponent<Button>();
                buttonsImage[i] = buttonsObj[i].GetComponent<Image>();
            }
        }

        private void Start()
        {
            CharactersManager.Instance.InstantiateCharacter();

            if (!string.IsNullOrEmpty(CharactersManager.mainCharacter.name))
            {
                SetButtonsForChar(CharactersManager.mainCharacter);
            }
            else
            {
                throw new Exception("There is no selected character");
            }
        }

        public void SetButtonsForChar(CharacterInfo characterInfo)
        {
            SetButtonsMethods(characterInfo.obj.GetComponent<IPlayableCharacter>());
            SetButtonSprites(characterInfo.attackIcons);
        }

        private void SetButtonsMethods(IPlayableCharacter characterScript)
        {
            foreach(Button b in buttons)
                b.onClick.RemoveAllListeners();

            buttons[0].onClick.AddListener( () => characterScript.BasicAttack()   );
            buttons[1].onClick.AddListener( () => characterScript.RangedAttack()  );
            buttons[2].onClick.AddListener( () => characterScript.SpecialAttack() );
        }

        private void SetButtonSprites(AttackIcons sprites)
        {
            buttonsImage[0].sprite = sprites.basicAttack;
            buttonsImage[1].sprite = sprites.rangedAttack;
            buttonsImage[2].sprite = sprites.specialAttack;
        }
    }
}
