using AttackOnTap.UI;
using UnityEngine;

namespace AttackOnTap.Managers
{
    public class EnemyFactory : MonoBehaviour
    {
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

        private void Start()
        {
            currentStage = currentStageStartAt;
            InitNewPhase();
        }

        private void InitNewPhase()
        {
            CharactersManager.canMove = true;

            stage = stages[currentStage];

            nextMinionTime = 0;
            phase = StagePhase.STARTING;
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
                battleText.SetBattleText("Round " + currentStage);
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
            if (timer > stage.timeSummoningMinions)
            {
                //Instantiate(stage.boss, enemySpawn.position, Quaternion.identity);

                minionsSpawned = 0;
                timer = 0;
                phase = StagePhase.BOSS;
            }

            if (timer > nextMinionTime * minionsSpawned)
            {
                Instantiate(GetRandomMinion(), enemySpawn.position, Quaternion.identity);

                minionsSpawned++;
                nextMinionTime = GetNextMinionTime();
            }
        }

        private void BossPhase()
        {
            phase = StagePhase.ENDING;
        }

        private void EndPhase()
        {
            if (!shownText)
            {
                CharactersManager.NotifyVictoryToChar();

                battleText.SetBattleText("Victory");
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

        private float GetNextMinionTime()
        {
            return Random.Range(stage.minTimeToSpawnMinion, stage.maxTimeToSpawnMinion);
        }

        private GameObject GetRandomMinion()
        {
            return stage.minions[Random.Range(0, stage.minions.Length)];
        }

        private void NotifyBossDeath()
        {

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
        public GameObject[] minions;
        public float timeSummoningMinions;

        public float maxTimeToSpawnMinion;
        public float minTimeToSpawnMinion;
    }
}
