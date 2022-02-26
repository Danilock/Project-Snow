using System;
using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using Game.Enemy;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// Shows in the UI the current enemy attack element
    /// </summary>
    public class UIEnemyAttackElement : MonoBehaviour
    {
        [SerializeField] private float _secondsBetweenIcons = .5f;

        [SerializeField] private Image _currentElementImage;

        [SerializeField] private Sprite[] _allPossibleElements;

        private int _currentIndex;

        private EnemyAttack _attack;

        private IEnumerator _enumerator;

        [SerializeField] private bool _canTick;
        
        private void Start()
        {
            EnemyQueueManager.Instance.OnChangeEnemy += OnChangeEnemy;

            _enumerator = ChangeIcon_CO();

            StartCoroutine(_enumerator);
        }

        private void OnDisable()
        {
            EnemyQueueManager.Instance.OnChangeEnemy -= OnChangeEnemy;
        }

        private void OnChangeEnemy(EnemyHealth enemy)
        { 
            _attack = enemy.GetComponent<EnemyAttack>();
            
            if(_attack == null)
                return;
            
            _attack.OnDecideElement += OnDecideElement;
            _attack.OnAttack += () =>
            {
                _enumerator = ChangeIcon_CO();
                StartCoroutine(_enumerator);
            };
        }

        private void OnDecideElement(Element element)
        {
            StopCoroutine(_enumerator);

            _currentElementImage.sprite = element.Image;
        }

        private IEnumerator ChangeIcon_CO()
        {
            while (true)
            {
                yield return new WaitForSeconds(_secondsBetweenIcons);

                _currentElementImage.sprite = _allPossibleElements[_currentIndex];

                _currentIndex = (_currentIndex + 1) % _allPossibleElements.Length;
            }
        }
    }
}