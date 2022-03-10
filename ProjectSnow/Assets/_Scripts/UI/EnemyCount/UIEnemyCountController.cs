using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enemy;
using UnityEngine;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIEnemyCountController : MonoBehaviour
    {
        [SerializeField] private UIEnemyCountInstance _prefabCount;

        [Header("Instantiated List")] [SerializeField, ReadOnly]
        private List<UIEnemyCountInstance> _countInstances;

        private UIEnemyCountInstance _currentInstance;

        [FoldoutGroup("Tween Settings")] [SerializeField]
        private Vector3 _currentElementSize;

        [FoldoutGroup("Tween Settings")] [SerializeField]
        private Vector3 _otherElementsSize;

        [FoldoutGroup("Tween Settings")] [SerializeField]
        private float _duration;

        private HorizontalLayoutGroup _group;

        private void Awake()
        {
            _group = GetComponent<HorizontalLayoutGroup>();
        }

        private void Start()
        {
            SetupEnemyCount();
        }

        private void OnChangeEnemy(EnemyHealth arg0)
        {
            _currentInstance.SetSize(_otherElementsSize, _duration);

            _currentInstance = _countInstances[EnemyQueueManager.Instance.GetCurrentIndex];
            
            _currentInstance.SetSize(_currentElementSize, _duration);
        }

        private void SetupEnemyCount()
        {
            for (int i = 0; i < EnemyQueueManager.Instance.GetAmountOfEnemies; i++)
            {
                UIEnemyCountInstance instance = Instantiate(_prefabCount, transform);

                _countInstances.Add(instance);
                
                if (i > 0)
                {
                    UIEnemyCountInstance previous = _countInstances[i - 1];
                    previous.NextCount = instance;

                    instance.PreviousCount = previous;
                }

                instance.TMProText.text = string.Format("{0:00}", i + 1);
            }

            _currentInstance = _countInstances[0];
            _currentInstance.SetSize(_currentElementSize, _duration);
            
            EnemyQueueManager.Instance.OnChangeEnemy += OnChangeEnemy;
        }

        private void OnDisable()
        {
            EnemyQueueManager.Instance.OnChangeEnemy -= OnChangeEnemy;
        }
    }
}