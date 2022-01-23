using System;
using System.Collections;
using System.Collections.Generic;
using Game.DamageSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyElementChange : MonoBehaviour
    {
        #region Private Fields
        [SerializeField] private List<EnemyHealthBar> _healthBars;
        [SerializeField, ReadOnly] private EnemyHealthBar _currentHealthBar;
        [SerializeField] private int _currentBarIndex = 0;
        private Damageable _damageable;
        #endregion

        #region Public Fields

        public EnemyHealthBar GetCurrentHealthBar => _currentHealthBar;

        public List<EnemyHealthBar> GetHealthBars => _healthBars;

        #endregion

        #region Properties

        /// <summary>
        /// If the current health of the enemy is lower than the Value specified in the current bar then we return true.
        /// </summary>
        private bool CurrentBarIsGreaterThanHealthValue
        {
            get
            {
                if (_damageable.CurrentHealth < _damageable.StartHealth * (_currentHealthBar.Value / 100))
                {
                    return true;
                }

                return false;
            }
        }
        #endregion

        private void Awake()
        {
            _damageable = GetComponent<Damageable>();
        }

        private void Start()
        {
            SetBar(0);
        }

        private void OnEnable()
        {
            _damageable.OnTakeDamage += OnTakeDamageListener;
        }

        private void OnDisable()
        {
            _damageable.OnTakeDamage -= OnTakeDamageListener;
        }

        private void OnTakeDamageListener(DamageInfo info)
        {
            if (CurrentBarIsGreaterThanHealthValue)
            {
                _currentBarIndex = (_currentBarIndex + 1) % _healthBars.Count;
                
                SetBar(_currentBarIndex);
            }
        }

        private void SetBar(int index)
        {
            _currentHealthBar = _healthBars[index];
            
            _damageable.ChangeElement(_currentHealthBar.Element);
        }
    }
}