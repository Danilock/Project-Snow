using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Managers;
using Game.Enemy;
using Game.DamageSystem;
using UnityEngine.Events;

namespace Game.Score
{
    public class ScoreManager : SceneSingleton<ScoreManager>
    {
        [SerializeField, ReadOnly] private int _score;

        public UnityAction<ScoreData> OnScore;

        private void Start()
        {
            EnemyQueueManager.Instance.OnChangeEnemy += RegisterToEnemyEvent;

            EnemyQueueManager.Instance.GetCurrentEnemy.OnTakeDamage += UpdateScore;
        }

        private void OnDisable()
        {
            EnemyQueueManager.Instance.OnChangeEnemy -= RegisterToEnemyEvent;
        }

        private void RegisterToEnemyEvent(EnemyHealth health)
        {
            health.OnTakeDamage += UpdateScore;
        }

        private void UpdateScore(DamageInfo info)
        {
            _score += (int)info.Damage;

            OnScore?.Invoke(new ScoreData(_score));
        }
    }

    public struct ScoreData
    {
        public int ScoreAmount;

        public ScoreData(int amount)
        {
            ScoreAmount = amount;
        }
    }
}
