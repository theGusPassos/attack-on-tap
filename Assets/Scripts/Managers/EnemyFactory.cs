using AttackOnTap.Battle;
using AttackOnTap.Characters;
using AttackOnTap.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AttackOnTap.Managers
{
    public class EnemyFactory : MonoBehaviour
    {
        public GameObject bossHealtBar;
        public Image bossFace;

        public StageInfo[] stages;
        private StageInfo stage;

        public Transform enemySpawn;

        private StagePhase phase;

        public BattleText battleText;
        private bool shownText = false;

        public float    timeToStartRound;
        public int      currentStageStartAt;
        private int     currentStage;

        private float timer = 0;
        private int minionsSpawned = 0;
        private float nextMinionTime;

        private List<GameObject> minionsAlive;

        private void Start()
        {
            minionsAlive = new List<GameObject>();
            currentStage = currentStageStartAt;
            InitNewPhase();
        }

        private void InitNewPhase()
        {
            CharactersManager.canMove = true;

            if (currentStage > stages.Length-1)
            {
                EndGame();
            }
            else
            {
                stage = stages[currentStage];

                nextMinionTime = 0;
                phase = StagePhase.STARTING;
            }
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if (phase == StagePhase.STARTING)
            {
                StartingPhase();
            }

            if (phase == StagePhase.SPAWNING)
            {
                SpawningPhase();
            }

            if (phase == StagePhase.BOSS)
            {
                BossPhase();
            }

            if (phase == StagePhase.ENDING)
            {
                EndPhase();
            }
        }

        private void StartingPhase()
        {
            if (!shownText)
            {
                battleText.SetBattleText("Round " + (currentStage+1));
                shownText = true;
            }

            if (timer > timeToStartRound)
            {
                shownText = false;
                phase = StagePhase.SPAWNING;
            }
        }

        private void SpawningPhase()
        {
            if (timer > stage.timeSummoningMinions && !MinionsAlive())
            {
                if (stage.boss != null)
                {
                    GameObject boss = Instantiate(stage.boss, enemySpawn.position, Quaternion.identity);
                    SetBossHealthBar(boss);
                    boss.GetComponent<IBoss>().SetEnemyFactory(this);

                    minionsSpawned = 0;
                    timer = 0;
                    phase = StagePhase.BOSS;
                }
                else
                {
                    EndPhase();
                }
            }
            else if (timer < stage.timeSummoningMinions && timer > nextMinionTime * minionsSpawned)
            {
                minionsAlive.Add(Instantiate(GetRandomMinion(), enemySpawn.position, Quaternion.identity));

                minionsSpawned++;
                nextMinionTime = GetNextMinionTime();
            }
        }

        private bool MinionsAlive()
        {
            foreach (GameObject minion in minionsAlive)
            {
                if (minion != null)
                    return true;
            }

            return false;
        }

        private void SetBossHealthBar(GameObject boss)
        {
            bossHealtBar.SetActive(true);

            bossFace.sprite = stage.bossFace;

            boss.GetComponent<HealthPointSystem>()
                .SetHealthBar(bossHealtBar.GetComponent<HealthBar>());
        }

        private void BossPhase()
        {
            
        }

        private void EndPhase()
        {
            if (!shownText)
            {
                if (currentStage < stages.Length -1)
                {
                    CharactersManager.NotifyVictoryToChar(0);

                    battleText.SetBattleText("Victory");
                }

                shownText = true;
            }

            if (timer >= timeToStartRound)
            {
                shownText = false;
                timer = 0;
                currentStage++;
                InitNewPhase();
            }
        }

        private void EndGame()
        {
            CharactersManager.NotifyVictoryToChar(1);

            battleText.SetBattleText("Nice Work!!!");
        }

        private float GetNextMinionTime()
        {
            return Random.Range(stage.minTimeToSpawnMinion, stage.maxTimeToSpawnMinion);
        }

        private GameObject GetRandomMinion()
        {
            return stage.minions[Random.Range(0, stage.minions.Length)];
        }

        public void NotifyBossDeath()
        {
            EndPhase();
        }

        private enum StagePhase
        {
            STARTING,
            SPAWNING,
            BOSS,
            ENDING
        }
    }

    [System.Serializable]
    public struct StageInfo
    {
        public GameObject boss;
        public Sprite bossFace;
        public GameObject[] minions;
        public float timeSummoningMinions;

        public float maxTimeToSpawnMinion;
        public float minTimeToSpawnMinion;
    }
}
