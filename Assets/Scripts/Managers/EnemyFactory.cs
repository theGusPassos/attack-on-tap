using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackOnTap.Managers
{
    public class EnemyFactory : MonoBehaviour
    {
        public StageInfo[] stages;
        private StageInfo stage;

        public Transform enemySpawn;

        private StagePhase phase;
        private float timeToStart;

        public int startAt;
        private int currentStage;

        private float timer = 0;
        private float nextMinionTime;

        private void Start()
        {
            currentStage = startAt;
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
        }

        private void StartingPhase()
        {
            // do something in a text script right here
        }

        private void SpawningPhase()
        {
            if (timer > stage.timeSummoningMinions)
            {
                Instantiate(stage.boss, enemySpawn.position, Quaternion.identity);

                timer = 0;
                phase = StagePhase.BOSS;
            }

            if (timer > nextMinionTime)
            {
                Instantiate(GetRandomMinion(), enemySpawn.position, Quaternion.identity);

                timer = 0;
                nextMinionTime = GetNextMinionTime();
            }
        }

        private void BossPhase()
        {

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
            BOSS
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
