using AttackOnTap.Characters;
using UnityEngine;

namespace AttackOnTap.Battle
{
    public class HealthPointSystem : MonoBehaviour
    {
        private static GameObject healthBarPrefab;
        private HealthBar healthBar;

        public float maxHealthPoints;
        private float currentHealthPoints = 0;

        private ICharacter character;

        private void Start()
        {
            if (healthBarPrefab == null)
                healthBarPrefab = Resources.Load("UI/HealthBars/Health Points") as GameObject;

            character = GetComponent<ICharacter>();
            currentHealthPoints = maxHealthPoints;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
                DealDamage(10);
        }

        public void DealDamage(float atack)
        {
            ShowHealthBar();

            currentHealthPoints -= atack;

            healthBar.SetValueToGo(currentHealthPoints);

            CheckDeath();
        }

        private void CheckDeath()
        {
            if (currentHealthPoints <= 0)
            {
                healthBar.DisableHealthBar();
                character.Die();
            }
        }

        /// <summary>
        /// Instantiates the health bar in the game. Can be used for boss fights, since the 
        /// health bar, in this case, will be shown as soon as the boss appears
        /// </summary>
        public void ShowHealthBar()
        {
            if (healthBar == null)
            {
                healthBar = Instantiate(healthBarPrefab, transform).GetComponent<HealthBar>();
                healthBar.name = "HealthBar";
                healthBar.Initialize(maxHealthPoints);
                healthBar.transform.position = new Vector3(0, 1, 0);
            }
        }

        public void SetHealthBar(HealthBar healthBar)
        {
            this.healthBar = healthBar;
            this.healthBar.Initialize(maxHealthPoints);
        }
    }
}
