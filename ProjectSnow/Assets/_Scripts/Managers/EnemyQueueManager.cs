using System;
using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using Game.Enemy;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine.Events;

namespace Managers
{
    public class EnemyQueueManager : SimpleSingleton<EnemyQueueManager>
    {
        #region Private Fields
        [SerializeField] private List<EnemyHealth> _enemies;
        
        private EnemyHealth _currentEnemy;
        
        private int _currentEnemyIndex = 0;
        #endregion

        #region Public Fields

        public UnityAction<EnemyHealth> OnChangeEnemy;

        public EnemyHealth GetCurrentEnemy => _currentEnemy;

        #endregion
        
        private void Start()
        {   
            SpawnNextEnemy();
        }

        private void OnEnable()
        {
            foreach (EnemyHealth currentEnemy in _enemies)
            {
                currentEnemy.OnDeath += OnDeath;
                currentEnemy.gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            foreach (EnemyHealth currentEnemy in _enemies)
            {
                currentEnemy.OnDeath -= OnDeath;
            }
        }

        private void OnDeath(DamageInfo arg0)
        {
            SpawnNextEnemy();
        }

        [ContextMenu("Spawn Next Enemy")]
        public void SpawnNextEnemy()
        {
            SpriteRenderer enemyRenderer;
            
            //If we have a previous enemy
            if (_currentEnemy != null)
            {
                Sequence seq = DOTween.Sequence();
                
                enemyRenderer = _currentEnemy.GetComponentInChildren<SpriteRenderer>();

                seq.Append(enemyRenderer.DOFade(0f, 1f).OnComplete(() =>
                {
                    _currentEnemy.gameObject.SetActive(false);

                    //Checking if it's the last enemy
                    if (_currentEnemyIndex == _enemies.Count - 1)
                        return;
                    
                    _currentEnemyIndex = (_currentEnemyIndex + 1) % _enemies.Count;

                    _currentEnemy = _enemies[_currentEnemyIndex];

                    _currentEnemy.gameObject.SetActive(true);
                    
                    enemyRenderer = _currentEnemy.GetComponentInChildren<SpriteRenderer>();
            
                    OnChangeEnemy?.Invoke(_currentEnemy);
                }));
                seq.Append(enemyRenderer.DOFade(1f, 1.5f));

                seq.Play();
            }
            else
            {
                _currentEnemy = _enemies[_currentEnemyIndex];
                
                _currentEnemy.gameObject.SetActive(true);

                enemyRenderer = _currentEnemy.GetComponentInChildren<SpriteRenderer>();
                enemyRenderer.DOFade(1f, 0f);
            
                OnChangeEnemy?.Invoke(_currentEnemy);
            }
        }

        [Button("Find Enemies"), GUIColor(0.4f, 0.8f, 1)]
        private void FindEnemies()
        {
            _enemies = FindObjectsOfType<EnemyHealth>().ToList();
        }
    }
}