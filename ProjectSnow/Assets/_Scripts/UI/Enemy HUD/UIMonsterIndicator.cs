using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Game.Enemy;
using Sirenix.OdinInspector;

namespace Game.UI {
    public class UIMonsterIndicator : MonoBehaviour
    {
        [SerializeField] private UIEnemyPortrait _enemyPortraitPrefab;

        [SerializeField, ReadOnly] private List<UIEnemyPortrait> _instantiatedEnemyPortrais;

        [SerializeField] private Transform _OuterTransform;

        [SerializeField, ReadOnly] private UIEnemyPortrait CurrentPortrait;
        private int _currentPortraitIndex = 0;

        private void Start()
        {
            for(int i = 0; i < EnemyQueueManager.Instance.GetEnemiesToSpawn.Length; i++)
            {
                UIEnemyPortrait obj = Instantiate(_enemyPortraitPrefab, this.transform);
                EnemyProfile profile = EnemyQueueManager.Instance.GetEnemiesToSpawn[i];
                
                obj.Init(profile.EnemySprite);
                _instantiatedEnemyPortrais.Add(obj);

                if(i > 0)
                {
                    obj.Previous = _instantiatedEnemyPortrais[i - 1];
                }
                if(i > 0 && i < EnemyQueueManager.Instance.GetAmountOfEnemies)
                {
                    obj.Previous.Next = obj;
                }
            }

            CurrentPortrait = _instantiatedEnemyPortrais[_currentPortraitIndex];

            EnemyQueueManager.Instance.OnChangeEnemy += OnEnemyDead;
        }

        private void OnDisable()
        {
            EnemyQueueManager.Instance.OnChangeEnemy -= OnEnemyDead;
        }

        private void OnEnemyDead(EnemyHealth enemy)
        {
            MovePortraits();
        }

        [Button("Move Portraits")]
        private void MovePortraits()
        {
            CurrentPortrait.Move(_OuterTransform.position);

            _currentPortraitIndex = (_currentPortraitIndex + 1) % _instantiatedEnemyPortrais.Count;

            CurrentPortrait = _instantiatedEnemyPortrais[_currentPortraitIndex];
        }
    }
}