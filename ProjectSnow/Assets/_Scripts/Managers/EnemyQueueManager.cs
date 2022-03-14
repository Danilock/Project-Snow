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
    public class EnemyQueueManager : SceneSingleton<EnemyQueueManager>
    {
        #region Private Fields

        [SerializeField] private EnemyProfile[] _enemiesToSpawn;

        [SerializeField] private EnemyController _enemyPrefab;
        
        [SerializeField, ReadOnly] private List<EnemyHealth> _enemies;
        
        private EnemyHealth _currentEnemy;
        
        private int _currentEnemyIndex = 0;

        public int GetCurrentIndex => _currentEnemyIndex;
        public int GetAmountOfEnemies => _enemies.Count;
        #endregion

        #region Public Fields

        public UnityAction<EnemyHealth> OnChangeEnemy;

        public EnemyHealth GetCurrentEnemy => _currentEnemy;

        private EnemyAttack _enemyAttack;

        public EnemyAttack GetEnemyAttack => _enemyAttack;

        #endregion

        #region Events

        public UnityAction<Element> OnElementDecided;
        public UnityAction OnAttack;

        #endregion
        
        protected override void Awake()
        {
            base.Awake();
            
            InstantiateEnemiesInScene();
        }

        private void Start()
        {
            SpawnNextEnemy();
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

                    _enemyAttack = _currentEnemy.GetComponent<EnemyAttack>();
                    
                    GetEnemyAttack.OnAttack += () => OnAttack.Invoke();
                    GetEnemyAttack.OnDecideElement += element => OnElementDecided.Invoke(element);
                    
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
                
                _enemyAttack = _currentEnemy.GetComponent<EnemyAttack>();
                
                GetEnemyAttack.OnAttack += () => OnAttack.Invoke();
                GetEnemyAttack.OnDecideElement += element => OnElementDecided.Invoke(element);
            
                OnChangeEnemy?.Invoke(_currentEnemy);
            }
        }

        private void InstantiateEnemiesInScene()
        {
            GameObject enemiesContainer = new GameObject("Enemies");
            
            foreach (EnemyProfile currentProfile in _enemiesToSpawn)
            {
                EnemyController enemy = Instantiate(_enemyPrefab, Vector3.zero, Quaternion.identity, enemiesContainer.transform);
                EnemyHealth health = enemy.GetComponent<EnemyHealth>();
                
                enemy.transform.localPosition = Vector3.zero;
                enemy.SetProfile(currentProfile);
                enemy.SetupEnemyAttributes();
                health.OnDeath += OnDeath; 
                
                enemy.gameObject.SetActive(false);

                _enemies.Add(enemy.GetComponent<EnemyHealth>());
            }
        }
    }
}