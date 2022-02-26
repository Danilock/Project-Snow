using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        
        [SerializeField, FoldoutGroup("Settings")] private float _secondsBetweenIcons = .5f;

        [SerializeField, FoldoutGroup("Settings")] private Image _currentElementImage;

        [SerializeField, FoldoutGroup("Settings")] private Sprite[] _allPossibleElements;

        #region Tweening

        [SerializeField, FoldoutGroup("Tweening")]
        private Vector3 _scale;

        [SerializeField, FoldoutGroup("Tweening")]
        private float _duration = .3f;

        [SerializeField, FoldoutGroup("Tweening")]
        private int _vibration = 10;

        [SerializeField, FoldoutGroup("Tweening")]
        private float _elasticity = 1f;

        #endregion
        
        private int _currentIndex;

        private EnemyAttack _attack;

        private IEnumerator _enumerator;

        private void Start()
        {
            EnemyQueueManager.Instance.OnChangeEnemy += OnChangeEnemy;
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
            _attack.OnAttack += StartTicking;

            enemy.OnDeath += arg0 => { StopCoroutine(_enumerator); };
            
            StartTicking();
        }

        private void StartTicking()
        {
            _enumerator = ChangeIcon_CO();
            StartCoroutine(_enumerator);
        }

        private void OnDecideElement(Element element)
        {
            StopCoroutine(_enumerator);

            _currentElementImage.sprite = element.Image;

            transform.DOPunchScale(_scale, _duration, _vibration, _elasticity);
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